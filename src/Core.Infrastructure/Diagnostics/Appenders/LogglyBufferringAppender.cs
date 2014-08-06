using System;
using System.Diagnostics;

namespace Lucas.Solutions.Diagnostics.Appenders
{
    using Lucas.Solutions.Diagnostics.Responses;
    using log4net.Appender;
    using log4net.Core;
    
    public class LogglyBufferringAppender : BufferingAppenderSkeleton, IRequestContext
    {
        public static readonly string InputKeyProperty = "LogglyInputKey";

        private readonly ILogglyAppenderConfig _config;
        private readonly ILogglyFormatter _formatter;

        public LogglyBufferringAppender()
        {
            _formatter = new LogglyFormatter();
            _config = new LogglyAppenderConfig();
            _config.RootUrl = "inputs/";
        }

        string IRequestContext.Url
        {
            get { return _config.RootUrl; }
        }

        public LogglyBufferringAppender RootUrl(string value)
        {
            _config.RootUrl = value;
            return this;
        }

        public LogglyBufferringAppender InputKey(string value)
        {
            _config.InputKey = value;
            return this;
        }

        public LogglyBufferringAppender UserAgent(string value)
        {
            _config.UserAgent = value;
            return this;
        }

        public LogglyBufferringAppender TimeoutInSeconds(int value)
        {
            _config.TimeoutInSeconds = value;
            return this;
        }

        protected override void Append(LoggingEvent loggingEvent)
        {
            _formatter.AppendAdditionalLoggingInformation(_config, loggingEvent);
            base.Append(loggingEvent);
        }

        protected override void SendBuffer(LoggingEvent[] loggingEvents)
        {
            Action<Response> callbackWrapper;
            callbackWrapper = (Response r) =>
            {
                if (!r.Success)
                {
                    Trace.WriteLine("Loggly communicator failed on LogglyApplender.");
                }
            };

            var communicator = new LogglyHttpCommunicator(this);
            var message = _formatter.ToJson(loggingEvents);

            communicator.SendPayload(LogglyHttpCommunicator.POST, string.Concat(_config.RootUrl, _config.InputKey), message, true, callbackWrapper);
        }
    }
}