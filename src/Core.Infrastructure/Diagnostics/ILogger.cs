using System;
using System.Collections.Generic;

namespace Lucas.Solutions.Diagnostics
{
    using Lucas.Solutions.Diagnostics.Responses;

    public interface ILogger
    {
        TLog Log<TLog>() where TLog : Log<TLog>;

        void Log<TLog>(TLog log) where TLog : Log<TLog>;
        void LogInfo(string message);
        void LogInfo(string message, IDictionary<string, object> data);
        void LogVerbose(string message);
        void LogVerbose(string message, IDictionary<string, object> data);
        void LogWarning(string message);
        void LogWarning(string message, IDictionary<string, object> data);
        void LogError(string message, Exception ex);
        void LogError(string message, Exception ex, IDictionary<string, object> data);
    }
}