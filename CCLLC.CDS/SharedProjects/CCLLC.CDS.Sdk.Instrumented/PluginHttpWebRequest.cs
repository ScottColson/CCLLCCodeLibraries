﻿using System;
using System.Globalization;
using System.IO;
using System.Net;



namespace CCLLC.CDS.Sdk
{
    using CCLLC.Core.Net;
    using CCLLC.Telemetry;

    public class PluginHttpWebRequest : IHttpWebRequest
    {        
        internal const string ContentEncodingHeader = "Content-Encoding";

        private Uri _address = null;
        private ITelemetryFactory _telemetryFactory = null;
        private ITelemetryClient _telemetryClient = null;
        private string _dependencyName = null;

        
        public ICredentials Credentials { get; set; }
        
      
        public WebHeaderCollection Headers { get; set; }
        

        public TimeSpan Timeout { get; set; }
        

        internal PluginHttpWebRequest(Uri address, string dependencyName = null, ITelemetryFactory telemetryFactory = null, ITelemetryClient telementryClient = null)
        {
            if (address == null) { throw new ArgumentNullException("address cannot be null."); }

            _dependencyName = dependencyName;
            _telemetryClient = telementryClient;
            _telemetryFactory = telemetryFactory;
            _address = address;

            this.Headers = new WebHeaderCollection();
            this.Timeout = new TimeSpan(0, 0, 30); //Default timeout is 30 seconds.
        }
            
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            _address = null;
            _dependencyName = null;
            _telemetryClient = null;
            _telemetryFactory = null;
            this.Credentials = null;
            this.Headers = null;
        }

        public virtual IPluginWebResponse Get()
        {
            IDependencyTelemetry dependencyTelemetry = null;
            IOperationalTelemetryClient<IDependencyTelemetry> dependencyClient = null;

            if (_telemetryClient != null && _telemetryFactory != null)
            {
                dependencyTelemetry = _telemetryFactory.BuildDependencyTelemetry(
                    "Web",
                    _address.ToString(),
                    _dependencyName != null ? _dependencyName : "PluginWebRequest",
                    null);
                dependencyClient = _telemetryClient.StartOperation<IDependencyTelemetry>(dependencyTelemetry);
            }

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_address);
                request.Method = "GET";
                if (this.Credentials != null)
                {
                    request.Credentials = this.Credentials;
                }
                request.Headers = this.Headers;                
                request.Timeout = (int)this.Timeout.TotalMilliseconds;

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    var pluginWebResponse = new PluginHttpWebResponse(response);
                    if (dependencyTelemetry != null)
                    {
                        dependencyTelemetry.ResultCode = pluginWebResponse.StatusCode.ToString();                        
                    }
                    
                    //signals completion of the request operation for telemetry tracking.
                    if (dependencyClient != null)
                    {
                        dependencyClient.CompleteOperation(pluginWebResponse.Success);                   
                    }

                    return pluginWebResponse;
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
                    throw new Exception("Plugin Web Request Timeout occurred.", ex);
                }
                throw new Exception(String.Format(CultureInfo.InvariantCulture,
                    "A Web exception occurred while attempting to issue the request. {0}: {1}",
                    ex.Message, str), ex);
            }
            finally
            {
                if (dependencyClient != null) { dependencyClient.Dispose(); }
            }

        }
      

        public virtual IPluginWebResponse Post(byte[] data, string contentType = null, string contentEncoding = null)             
        {
            IDependencyTelemetry dependencyTelemetry = null;
            IOperationalTelemetryClient<IDependencyTelemetry> dependencyClient = null;            
            
            if (_telemetryClient != null && _telemetryFactory != null)
            {
                dependencyTelemetry = _telemetryFactory.BuildDependencyTelemetry(
                    "Web",
                    _address.ToString(),
                    _dependencyName != null ? _dependencyName : "PluginWebRequest",
                    null);
                dependencyClient = _telemetryClient.StartOperation<IDependencyTelemetry>(dependencyTelemetry);
            }

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_address);
                request.Method = "POST";
                if(this.Credentials != null)
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
                    var pluginWebResponse = new PluginHttpWebResponse(response);
                    if (dependencyTelemetry != null)
                    {
                        dependencyTelemetry.ResultCode = pluginWebResponse.StatusCode.ToString();                       
                    }
                 
                    //signals completion of the request operation for telemetry tracking.
                    if (dependencyClient != null)
                    {
                        dependencyClient.CompleteOperation(pluginWebResponse.Success);                       
                    }

                    return pluginWebResponse;
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
                    throw new Exception("Plugin Web Request Timeout occurred.", ex);
                }
                throw new Exception(String.Format(CultureInfo.InvariantCulture,
                    "A Web exception occurred while attempting to issue the request. {0}: {1}",
                    ex.Message, str), ex);
            }
            finally
            {
                if (dependencyClient != null) { dependencyClient.Dispose(); }
            }

        }

        public virtual IPluginWebResponse Put(string data, string contentType = null)
        {
            IDependencyTelemetry dependencyTelemetry = null;
            IOperationalTelemetryClient<IDependencyTelemetry> dependencyClient = null;

            if (_telemetryClient != null && _telemetryFactory != null)
            {
                dependencyTelemetry = _telemetryFactory.BuildDependencyTelemetry(
                    "Web",
                    _address.ToString(),
                    _dependencyName != null ? _dependencyName : "PluginWebRequest",
                    null);
                dependencyClient = _telemetryClient.StartOperation<IDependencyTelemetry>(dependencyTelemetry);
            }

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_address);
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
                    var pluginWebResponse = new PluginHttpWebResponse(response);
                    if (dependencyTelemetry != null)
                    {
                        dependencyTelemetry.ResultCode = pluginWebResponse.StatusCode.ToString();
                    }

                    //signals completion of the request operation for telemetry tracking.
                    if (dependencyClient != null)
                    {
                        dependencyClient.CompleteOperation(pluginWebResponse.Success);
                    }

                    return pluginWebResponse;
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
                    throw new Exception("Plugin Web Request Timeout occurred.", ex);
                }
                throw new Exception(String.Format(CultureInfo.InvariantCulture,
                    "A Web exception occurred while attempting to issue the request. {0}: {1}",
                    ex.Message, str), ex);
            }
            finally
            {
                if (dependencyClient != null) { dependencyClient.Dispose(); }
            }

        }

    }
}
