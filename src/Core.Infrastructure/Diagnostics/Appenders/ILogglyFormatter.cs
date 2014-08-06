using System.Collections.Generic;

namespace Lucas.Solutions.Diagnostics.Appenders
{
    using log4net.Core;

    public interface ILogglyFormatter
    {
        void AppendAdditionalLoggingInformation(ILogglyAppenderConfig unknown, LoggingEvent loggingEvent);
        string ToJson(LoggingEvent loggingEvent);
        string ToJson(IEnumerable<LoggingEvent> loggingEvents);
    }
}