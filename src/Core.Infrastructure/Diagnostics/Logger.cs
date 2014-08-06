using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Lucas.Solutions.Diagnostics
{
    using Lucas.Solutions.Diagnostics.Responses;

    public abstract partial class Logger<TLogger> : ILogger, IDisposable
        where TLogger : Logger<TLogger>
    {
        private ConcurrentDictionary<int, Task> _tasks;

        protected Logger()
        {
            _tasks = new ConcurrentDictionary<int, Task>();
        }

        void IDisposable.Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            Flush();
        }

        public void Flush()
        {
            Task.WaitAll(_tasks.Values.ToArray<Task>());
        }

        void ILogger.Log<TLog>(TLog log)
        {
            Log(log);
        }

        protected abstract void Log<TLog>(TLog log) where TLog : Log<TLog>;

        public virtual TLog Log<TLog>() where TLog : Log<TLog>
        {
            return (TLog)Activator.CreateInstance(typeof(TLog), (ILogger)this);
        }

        public virtual TLog Log<TLog>(Action<LogResponse> callback) where TLog : Log<TLog>
        {
            return (TLog)Activator.CreateInstance(typeof(TLog), (ILogger)this, callback);
        }

        public virtual TLog Log<TLog>(IDictionary<string, object> data) where TLog : Log<TLog>
        {
            return (TLog)Activator.CreateInstance(typeof(TLog), (ILogger)this, data);
        }

        public virtual TLog Log<TLog>(IDictionary<string, object> data, Action<LogResponse> callback) where TLog : Log<TLog>
        {
            return (TLog)Activator.CreateInstance(typeof(TLog), (ILogger)this, data, callback);
        }

        public void Log(string message, LogCategories category, IDictionary<string, object> data)
        {
            var log = new Log(this, data).Message(message).Category(category);
            log.Send();
        }

        public void LogError(string message, Exception ex)
        {
            LogError(message, ex, null);
        }

        public void LogError(string message, Exception ex, IDictionary<string, object> data)
        {
            var log = new Log(this, data).Message(message).Category(LogCategories.Error).Error(ex);
            log.Send();
        }

        public void LogInfo(string message)
        {
            LogInfo(message, null);
        }

        public void LogInfo(string message, IDictionary<string, object> data)
        {
            Log(message, LogCategories.Info, data);
        }

        public void LogVerbose(string message)
        {
            LogVerbose(message, null);
        }

        public void LogVerbose(string message, IDictionary<string, object> data)
        {
            Log(message, LogCategories.Verbose, data);
        }

        public void LogWarning(string message)
        {
            LogWarning(message, null);
        }

        public void LogWarning(string message, IDictionary<string, object> data)
        {
            Log(message, LogCategories.Warning, data);
        }
    }
}