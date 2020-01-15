﻿using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using CCLLC.Telemetry.Implementation;

namespace CCLLC.Telemetry.Sink
{
    /// <summary>
    /// Helper generated by <see cref="TelemetryTransmitter"/> to post a single batch of serialized
    /// telemetry data to the telemetry system endpoint.
    /// </summary>
    public class Transmission : ITransmission
    {
        protected const string ContentEncodingHeader = "Content-Encoding";

        protected static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(30);

        private int _isSending;
        private Uri _endPointAddress;
        private byte[] _content;
        private string _contentType;
        private string _contentEncoding;
              
        internal protected Transmission(Uri address, byte[] content, string contentType, string contentEncoding)
        {
            if (address == null)
            {
                throw new ArgumentNullException("address");
            }

            if (content == null)
            {
                throw new ArgumentNullException("content");
            }

            if (contentType == null)
            {
                throw new ArgumentNullException("contentType");
            }

            _endPointAddress = address;
            _content = content;
            _contentType = contentType;
            _contentEncoding = contentEncoding;                                 
        }

        /// <summary>
        /// Send the content synchronously.
        /// </summary>
        /// <param name="timeout">The maximum allowed transmission time. If omitted, defaults to 30 seconds.</param>
        /// <returns><see cref="IHttpWebResponseWrapper"/> with status and any returned content or headers.</returns>
        public virtual IHttpWebResponseWrapper Send(TimeSpan timeout = default(TimeSpan))
        {
            if (Interlocked.CompareExchange(ref _isSending, 1, 0) != 0)
            {
                throw new InvalidOperationException("Transmission is already in progress.");
            }
            try
            {
                try
                {
                    timeout = (timeout == default(TimeSpan)) ? DefaultTimeout : timeout;

                    WebRequest request = this.CreateRequest(_endPointAddress);
                    request.Timeout = (int)timeout.TotalMilliseconds;
                    return this.GetResponse(request);
                }
                catch (WebException ex)
                {
                    string str = string.Empty;
                    if (ex.Response != null)
                    {
                        using (StreamReader reader = new StreamReader(ex.Response.GetResponseStream()))
                        {
                            str = reader.ReadToEnd();
                        }
                        ex.Response.Close();
                    }
                    if (ex.Status == WebExceptionStatus.Timeout)
                    {
                        throw new Exception(
                                "The timeout elapsed while attempting to transmit telemetry.", ex);

                    }
                    throw new Exception(String.Format(CultureInfo.InvariantCulture,
                        "A Web exception occurred while attempting to issue the request. {0}: {1}",
                        ex.Message, str), ex);
                }
            }
            finally
            {
                Interlocked.Exchange(ref _isSending, 0);
            }
        }


        /// <summary>
        /// Send the content asynchronously.
        /// </summary>
        /// <param name="timeout">The maximum allowed transmission time. If omitted, defaults to 30 seconds.</param>
        /// <returns><see cref="IHttpWebResponseWrapper"/> with status and any returned content or headers.</returns>
        public virtual async Task<IHttpWebResponseWrapper> SendAsync(TimeSpan timeout = default(TimeSpan))
        {
            if (Interlocked.CompareExchange(ref _isSending, 1, 0) != 0)
            {
                throw new InvalidOperationException("Transmission is already in progress.");
            }

            try
            {
                timeout = (timeout == default(TimeSpan)) ? DefaultTimeout : timeout;

                WebRequest request = this.CreateRequest(_endPointAddress);
                Task<HttpWebResponseWrapper> sendTask = this.GetResponseAsync(request);

                Task timeoutTask = Task.Delay(timeout).ContinueWith(task =>
                {
                    if (!sendTask.IsCompleted)
                    {
                        request.Abort(); // And force the sendTask to throw WebException.
                    }
                });

                Task completedTask = await Task.WhenAny(timeoutTask, sendTask).ConfigureAwait(false);

                // Observe any exceptions the sendTask may have thrown and propagate them to the caller.
                HttpWebResponseWrapper responseContent = await sendTask.ConfigureAwait(false);
                return responseContent;

            }            
            finally
            {
                Interlocked.Exchange(ref _isSending, 0);
            }
        }
               
      
        protected virtual WebRequest CreateRequest(Uri address)
        {
            var request = WebRequest.Create(address);

            request.Method = "POST";
            request.ContentType = _contentType;
            
            if (!string.IsNullOrEmpty(_contentEncoding))
            {
                request.Headers[ContentEncodingHeader] = _contentEncoding;
            }

            return request;
        }
        
        protected virtual HttpWebResponseWrapper GetResponse(WebRequest request)
        {
            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(_content, 0, _content.Length);
            }

            using (WebResponse response = request.GetResponse())
            {
                return this.CheckResponse(response);
            }
        }
        
        protected virtual async Task<HttpWebResponseWrapper> GetResponseAsync(WebRequest request)
        {
            using (Stream requestStream = await request.GetRequestStreamAsync().ConfigureAwait(false))
            {
                await requestStream.WriteAsync(_content, 0, _content.Length).ConfigureAwait(false);
            }

            using (WebResponse response = await request.GetResponseAsync().ConfigureAwait(false))
            {
                return this.CheckResponse(response);
            }
        }

        protected virtual HttpWebResponseWrapper CheckResponse(WebResponse response)
        {
            HttpWebResponseWrapper wrapper = null;

            var httpResponse = response as HttpWebResponse;
            if (httpResponse != null)
            {
                // Return content only for 206 for performance reasons
                // Currently we do not need it in other cases
                if (httpResponse.StatusCode == HttpStatusCode.PartialContent)
                {
                    wrapper = new HttpWebResponseWrapper
                    {
                        StatusCode = (int)httpResponse.StatusCode,
                        StatusDescription = httpResponse.StatusDescription
                    };

                    if (httpResponse.Headers != null)
                    {
                        wrapper.RetryAfterHeader = httpResponse.Headers["Retry-After"];
                    }

                    using (StreamReader content = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        wrapper.Content = content.ReadToEnd();
                    }
                }
            }

            return wrapper;
        }
    }
}