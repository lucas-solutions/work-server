using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;

namespace Lucas.Solutions.Diagnostics
{
    using Lucas.Solutions.Diagnostics.Responses;
    
    /// <summary>
    /// Log builder class.
    /// </summary>
    public sealed class Log : Log<Log>
    {
        public Log(ILogger logger)
            : this(logger, new Dictionary<string, object>())
        {
        }

        public Log(ILogger logger, IDictionary<string, object> data)
            : base(logger, data)
        {
        }
    }

    /// <summary>
    /// Abstract log builder class
    /// </summary>
    /// <typeparam name="TLog">Concrete class for the log builder</typeparam>
    public abstract class Log<TLog>
        where TLog : Log<TLog>
    {
        public static implicit operator Dictionary<string, object>(Log<TLog> log)
        {
            return new Dictionary<string,object>(log.Context.Data);
        }

        public static implicit operator LogCategories(Log<TLog> log)
        {
            return log.Context.Category;
        }

        public static implicit operator LogContext<TLog>(Log<TLog> log)
        {
            return log.Context;
        }

        private readonly LogContext<TLog> _context;
        private StackFrame _caller;
        private StackTrace _trace;
        
        protected Log(LogContext<TLog> context)
        {
            _context = context;
        }

        protected Log(ILogger logger, IDictionary<string, object> data)
            : this(new LogContext<TLog>(logger, data))
        { 
        }

        public LogResponse Response
        {
            get { return Context.Response; }
        }

        public bool Completed
        {
            get { return Context.Completed; }
        }

        protected LogContext<TLog> Context
        {
            get { return _context; }
        }

        public bool Sent
        {
            get { return Context.Sending || Context.Completed; }
        }

        public bool Synchronous
        {
            get;
            set;
        }

        public TLog Application(string value)
        {
            Context["Application"] = value;
            return (TLog)this;
        }

        public StackFrame Caller
        {
            get { return _caller ?? (_caller = new StackFrame(1)); }
        }

        protected StackTrace Trace
        {
            get { return _trace ?? (_trace = new StackTrace()); }
        }

        public TLog Blame(string text)
        {
            Context["Blame"] = text;
            return (TLog)this;
        }

        public TLog Callback(Action<LogContext<TLog>> callback)
        {
            Context.Callback = callback;
            return (TLog)this;
        }

        public virtual TLog Category(LogCategories value)
        {
            Context.Category = value;
            Context["Category"] = value.ToString();
            return (TLog)this;
        }

        public TLog Category(EventLogEntryType value)
        {
            switch (value)
            {
                case EventLogEntryType.Error:
                    return Category(LogCategories.Error);

                case EventLogEntryType.FailureAudit:
                    return Category(LogCategories.FailureAudit);

                case EventLogEntryType.Information:
                    return Category(LogCategories.Info);

                case EventLogEntryType.SuccessAudit:
                    return Category(LogCategories.SuccessAudit);

                case EventLogEntryType.Warning:
                    return Category(LogCategories.Warning);
            }

            return Category(LogCategories.Info);
        }

        public virtual TLog Category(TraceEventType value)
        {
            switch (value)
            {
                case TraceEventType.Critical:
                    return Category(LogCategories.Critical);

                case TraceEventType.Error:
                    return Category(LogCategories.Error);

                case TraceEventType.Information:
                    return Category(LogCategories.Info);

                case TraceEventType.Resume:
                    return Category(LogCategories.Resume);

                case TraceEventType.Start:
                    return Category(LogCategories.Start);

                case TraceEventType.Stop:
                    return Category(LogCategories.Stop);

                case TraceEventType.Suspend:
                    return Category(LogCategories.Suspend);

                case TraceEventType.Transfer:
                    return Category(LogCategories.Transfer);

                case TraceEventType.Verbose:
                    return Category(LogCategories.Verbose);

                case TraceEventType.Warning:
                    return Category(LogCategories.Warning);
            }

            return Category(LogCategories.Info);
        }

        public T Cast<T>()
            where T : TLog
        {
            T log = null;
            try
            {
                log = (T)Activator.CreateInstance(typeof(T), Context);
            }
            catch
            {
            }
            return log;
        }

        public TLog Class()
        {
            Class(Caller.GetType());
            return (TLog)this;
        }

        public TLog Class(string text)
        {
            Context["Class"] = text;
            return (TLog)this;
        }

        public TLog Class(Type type)
        {
            var validType = type ?? Caller.GetType();
            Class(validType.Name);
            Namespace(validType.Namespace);
            return (TLog)this;
        }

        /// <summary>
        /// System.Web.Hosting.HostingEnvironment.IsDevelopmentEnvironment
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public TLog Development(bool value)
        {
            Context["Development"] = value;
            return (TLog)this;
        }

        public TLog Detail(string text)
        {
            Context["Detail"] = text;
            return (TLog)this;
        }

        public TLog Duration(TimeSpan value)
        {
            Context["Duration"] = value.ToString();
            return (TLog)this;
        }

        /// <summary>
        /// Set log category to Error
        /// </summary>
        /// <returns>this</returns>
        public TLog Error()
        {
            return Category(LogCategories.Error);
        }

        /// <summary>
        /// Set log category to Error and log exception
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="recursive"></param>
        /// <returns></returns>
        public TLog Error(Exception ex, bool recursive = false)
        {
            Context["Exception"] = ExceptionData(ex, recursive);
            return Category(LogCategories.Error);
        }

        private static Dictionary<string, object> ExceptionData(Exception e, bool recursive)
        {
            var data = new Dictionary<string, object>
            {
                { "Type", e.GetType().Name },
                { "Message", e.Message }
            };

            if (recursive && e.InnerException != null)
            {
                data["InnerException"] = ExceptionData(e.InnerException, true);
            }

            return data;
        }

        public TLog File()
        {
            Context["File"] = Caller.GetFileName();
            return (TLog)this;
        }

        public TLog File(string text)
        {
            Context["File"] = text;
            return (TLog)this;
        }

        public TLog Hostname(string text)
        {
            Context["Hostname"] = text;
            return (TLog)this;
        }

        public TLog ID(int value)
        {
            Context["ID"] = value;
            return (TLog)this;
        }

        /// <summary>
        /// Set log category to Info
        /// </summary>
        /// <returns>this</returns>
        public TLog Info()
        {
            return Category(LogCategories.Info);
        }

        /// <summary>
        /// Set log category to Info and set message
        /// </summary>
        /// <returns>this</returns>
        public TLog Info(string message)
        {
            return Category(LogCategories.Info).Message(message);
        }

        public TLog LocalAddr(string value)
        {
            Context["LocalAddr"] = value;
            return (TLog)this;
        }

        public TLog LocalAddr(IPAddress value)
        {
            Context["LocalAddr"] = value.ToString();
            return (TLog)this;
        }

        public TLog Location()
        {
            return Class().Method().File();
        }

        public TLog Method()
        {
            Context["Method"] = Caller.GetMethod().Name;
            return (TLog)this;
        }

        public TLog Method(string text)
        {
            Context["Method"] = text;
            return (TLog)this;
        }

        public TLog Message(string text)
        {
            Context["Message"] = text;
            return (TLog)this;
        }

        public TLog Namespace(string text)
        {
            Context["Namespace"] = text;
            return (TLog)this;
        }

        public TLog Priority(int value)
        {
            Context["Priority"] = value;
            return (TLog)this;
        }

        public void Send()
        {
            Category(Context.Category);
            Context.Logger.Log((TLog)this);
        }

        public TLog Severity(int value)
        {
            Context["Severity"] = value;
            return (TLog)this;
        }

        public TLog Status(int value)
        {
            Context["Status"] = value;
            return (TLog)this;
        }

        public TLog Timestamp()
        {
            return Timestamp(DateTime.Now);
        }

        public TLog Timestamp(DateTime name)
        {
            Context["Timestamp"] = name.ToString();
            return (TLog)this;
        }

        public TLog Token(Guid value)
        {
            Context["Token"] = value.ToString();
            return (TLog)this;
        }

        /// <summary>
        /// Set version
        /// </summary>
        /// <param name="value">version</param>
        /// <returns>this</returns>
        public TLog Version(int value)
        {
            Context["Version"] = value;
            return (TLog)this;
        }

        /// <summary>
        /// Set version
        /// </summary>
        /// <param name="value">version</param>
        /// <returns>this</returns>
        public TLog Version(Version value)
        {
            Context["Version"] = value.ToString();
            return (TLog)this;
        }

        /// <summary>
        /// Set log category to Watning
        /// </summary>
        /// <returns>this</returns>
        public TLog Warning()
        {
            return Category(LogCategories.Warning);
        }
    }
}