using System;
using System.Collections.Generic;
using System.Threading;

namespace Lucas.Solutions.Diagnostics
{
    using Lucas.Solutions.Diagnostics.Responses;
    using Newtonsoft.Json;

    /// <summary>
    /// http://loggly.com/support/sending-data/logging-from/application-logs/net/
    /// Customary
    /// MVC4 + RavenDB + ExtJS
    /// Consumer Key
    /// mGybFDd8cPR8RGP36X
    /// Consumer Secret
    /// G5e7n5cBX685q4LeFrfMhP5cX9apJBDV
    /// Request token URL
    /// http://customary.loggly.com/api/oauth/request_token/
    /// Access token URL
    /// http://customary.loggly.com/api/oauth/access_token/
    /// Authorize URL
    /// http://customary.loggly.com/api/oauth/authorize/
    /// </summary>
    public class LogglyLogger : Logger<LogglyLogger>, IRequestContext
    {
        private string _url;
        private readonly string _inputKey;

        public LogglyLogger(string inputKey, string url = "logs.loggly.com/")
        {
            _url = url;
            _inputKey = inputKey;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        public string Url
        {
            get { return _url; }
        }

        protected override void Log<TLog>(TLog log)
        {
            var context = (LogContext<TLog>)log;
            var communicator = new LogglyHttpCommunicator(this);

            LogResponse response;
            Action<Response> callbackWrapper;
            
            var synchronizer = log.Synchronous ? new AutoResetEvent(false) : null;

            callbackWrapper = (Response r) =>
            {
                if (r.Success)
                {
                    response = JsonConvert.DeserializeObject<LogResponse>(r.Raw);
                    response.Success = true;
                }
                else
                {
                    response = new LogResponse { Success = false };
                }

                if (synchronizer != null)
                    synchronizer.Set();

                var callback = (Action<LogResponse>)context;
                if (callback != null)
                    callback(response);
            };

            if (synchronizer != null)
                synchronizer.WaitOne();

            communicator.SendPayload(LogglyHttpCommunicator.POST, string.Concat("inputs/", _inputKey), context.ToString(), true, callbackWrapper);
        }
    }
}