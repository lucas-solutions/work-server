using System;
using System.Diagnostics;

namespace Lucas.Solutions.Diagnostics.Appenders
{
    using Lucas.Solutions.Diagnostics.Responses;
    using log4net.Appender;
    using log4net.Core;
    
    public class LogglyAppender : AppenderSkeleton, IRequestContext
    {
        public static readonly string InputKeyProperty = "LogglyInputKey";

        private ILogglyAppenderConfig _config;

        public LogglyAppender()
        {
            _config = new LogglyAppenderConfig();
            _config.RootUrl = "inputs/";
        }

        string IRequestContext.Url
        {
            get { return _config.RootUrl; }
        }

        public LogglyAppender RootUrl(string value)
        { 
            _config.RootUrl = value;
            return this;
        }
        
        public LogglyAppender InputKey(string value)
        {
            _config.InputKey = value;
            return this;
        }
        
        public LogglyAppender UserAgent(string value)
        {
            _config.UserAgent = value;
            return this;
        }
        
        public LogglyAppender TimeoutInSeconds(int value)
        {   
            _config.TimeoutInSeconds = value;
            return this;
        }

        protected override void Append(LoggingEvent loggingEvent)
        {
            Action<Response> callbackWrapper;
            callbackWrapper = (Response r) =>
            {
                if (!r.Success)
                {
                    Trace.WriteLine("Loggly communicator failed on LogglyApplender.");
                }
            };

            var formatter = new LogglyFormatter();
            formatter.AppendAdditionalLoggingInformation(_config, loggingEvent);
            
            var communicator = new LogglyHttpCommunicator(this);
            var message = formatter.ToJson(loggingEvent);

            communicator.SendPayload(LogglyHttpCommunicator.POST, string.Concat(_config.RootUrl, _config.InputKey), message, true, callbackWrapper);
        }
    }
}