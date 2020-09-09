using System;
using System.Globalization;
using System.IO;
using System.Net;

namespace CCLLC.Core.Net
{   
    public class HttpWebRequestWrapper : IWebRequest
    {
        private const string ContentEncodingHeader = "Content-Encoding";
        private const int DefaultTimeoutInSeconds = 30;

        protected Uri Address { get; private set; }
       
        public ICredentials Credentials { get; set; }

        public WebHeaderCollection Headers { get; set; }

        public TimeSpan Timeout { get; set; }

        public HttpWebRequestWrapper(IAPIEndpoint endpoint) : this(endpoint.ToUri()) { }

        public HttpWebRequestWrapper(Uri address) : base()
        {
            Address = address ?? throw new ArgumentNullException("address cannot be null.");

            this.Headers = new WebHeaderCollection();
            this.Timeout = TimeSpan.FromSeconds(DefaultTimeoutInSeconds);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            Address = null;
            this.Credentials = null;
            this.Headers = null;
        }

        public virtual IWebResponse Get()
        {  
            try
            {
                var request = (System.Net.HttpWebRequest)WebRequest.Create(Address);
                request.Method = "GET";
                if (this.Credentials != null)
                {
                    request.Credentials = this.Credentials;
                }
                request.Headers = this.Headers;
                request.Timeout = (int)this.Timeout.TotalMilliseconds;

                using (var response = request.GetResponse() as HttpWebResponse)
                {
                   return new HttpWebResponseWrapper(response);     
                    
                }

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
                    throw new Exception("Web Request Timeout occurred.", ex);
                }

                throw new Exception(String.Format(CultureInfo.InvariantCulture,
                    "A Web exception occurred while attempting to issue the request. {0}: {1}",
                    ex.Message, str), ex);
            }            
        }

        public virtual IWebResponse Delete()
        {
            try
            {
                var request = (System.Net.HttpWebRequest)WebRequest.Create(Address);
                request.Method = "DELETE";
                if (this.Credentials != null)
                {
                    request.Credentials = this.Credentials;
                }
                request.Headers = this.Headers;
                request.Timeout = (int)this.Timeout.TotalMilliseconds;

                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    return new HttpWebResponseWrapper(response);

                }

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
                    throw new Exception("Web Request Timeout occurred.", ex);
                }

                throw new Exception(String.Format(CultureInfo.InvariantCulture,
                    "A Web exception occurred while attempting to issue the request. {0}: {1}",
                    ex.Message, str), ex);
            }
        }

        public virtual IWebResponse Post(byte[] data, string contentType = null, string contentEncoding = null)
        { 
            try
            {
                var request = (System.Net.HttpWebRequest)WebRequest.Create(Address);
                request.Method = "POST";
                if (this.Credentials != null)
                {
                    request.Credentials = this.Credentials;
                }
                request.Headers = this.Headers;
                if (contentType != null)
                {
                    request.ContentType = contentType;
                }

                if (!string.IsNullOrEmpty(contentEncoding))
                {
                    request.Headers[ContentEncodingHeader] = contentEncoding;
                }

                request.Timeout = (int)this.Timeout.TotalMilliseconds;

                if (data.Length > 0)
                {
                    using (Stream requestStream = request.GetRequestStream())
                    {
                        requestStream.Write(data, 0, data.Length);
                    }
                }

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    var webResponse = new HttpWebResponseWrapper(response); 
                    return webResponse;
                }

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
                    throw new Exception("Web Request Timeout occurred.", ex);
                }
                throw new Exception(String.Format(CultureInfo.InvariantCulture,
                    "A Web exception occurred while attempting to issue the request. {0}: {1}",
                    ex.Message, str), ex);
            }            

        }

        public virtual IWebResponse Put(string data, string contentType = null)
        {      
            try
            {
                var request = (System.Net.HttpWebRequest)WebRequest.Create(Address);
                request.Method = "PUT";
                if (this.Credentials != null)
                {
                    request.Credentials = this.Credentials;
                }
                request.Headers = this.Headers;
                if (contentType != null)
                {
                    request.ContentType = contentType;
                }

                request.Timeout = (int)this.Timeout.TotalMilliseconds;

                if (!string.IsNullOrEmpty(data) && data.Length > 0)
                {
                    using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                    {
                        streamWriter.Write(data);
                    }
                }

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    var webResponse = new HttpWebResponseWrapper(response);    
                    return webResponse;
                }

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
                    throw new Exception("Web Request Timeout occurred.", ex);
                }
                throw new Exception(String.Format(CultureInfo.InvariantCulture,
                    "A Web exception occurred while attempting to issue the request. {0}: {1}",
                    ex.Message, str), ex);
            }          

        }
    }
}
