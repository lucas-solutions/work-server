using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Lucas.Solutions.Diagnostics
{
    using Lucas.Solutions.Diagnostics.Responses;
    using log4net;
    using System.Diagnostics;
    
    public class Log4NetLogger : Logger<Log4NetLogger>
    {
        private ILog _log4netLogger;
        private ConcurrentDictionary<int, Task> _tasks;

        public Log4NetLogger()
            : this(Log4NetHelper.LogInstance)
        {
        }

        public Log4NetLogger(ILog log4netLogger)
        {
            _log4netLogger = log4netLogger;
            _tasks = new ConcurrentDictionary<int, Task>();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        protected override void Log<TLog>(TLog log)
        {
            var context = (LogContext<TLog>)log;
            var handler = ToHandler(log);

            Task idc;
            Task task = Task.Factory.StartNew(() => handler);
            if (!_tasks.TryAdd(task.Id, task))
            {
                Trace.WriteLine("could not add log task to pending tasks");
            }
            task.ContinueWith((Task at) =>
            {
                _tasks.TryRemove(at.Id, out idc);

                var response = new LogResponse { Success = true };
                var callback = (Action<LogResponse>)context;
                if (callback != null)
                    callback(response);
            });
        }

        private Action<string> ToHandler(LogCategories value)
        {
            switch (value)
            {
                case LogCategories.Critical:
                    return message => _log4netLogger.Error(message);

                case LogCategories.Debug:
                    return message => _log4netLogger.Debug(message);

                case LogCategories.Error:
                    return message => _log4netLogger.Error(message);

                case LogCategories.FailureAudit:
                    return message => _log4netLogger.Error(message);

                case LogCategories.Fatal:
                    return message => _log4netLogger.Fatal(message);

                case LogCategories.Info:
                    return message => _log4netLogger.Info(message);

                case LogCategories.Resume:
                    return message => _log4netLogger.Info(message);

                case LogCategories.Start:
                    return message => _log4netLogger.Info(message);

                case LogCategories.Stop:
                    return message => _log4netLogger.Info(message);

                case LogCategories.SuccessAudit:
                    return message => _log4netLogger.Info(message);

                case LogCategories.Suspend:
                    return message => _log4netLogger.Info(message);

                case LogCategories.Transfer:
                    return message => _log4netLogger.Info(message);

                case LogCategories.Verbose:
                    return message => _log4netLogger.Info(message);

                case LogCategories.Warning:
                    return message => _log4netLogger.Warn(message);

                default:
                    throw new NotImplementedException(string.Format("Log4NetLogger does not implement handler for Log Category {0}.", value));
            }
        }
    }
}