using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Lucas.Solutions.Diagnostics
{
    using Lucas.Solutions.Configuration;
    using log4net;
    using log4net.Appender;
    using log4net.Config;
    using log4net.Layout;
    
    internal class Log4NetHelper
    {
        public const string APPLICATION_ID_CONFIG_NAME = "ApplicationIdForLogging";
        public const string LOG4NET_APPLICATION_ID_PARAM_NAME = "ApplicationId";
        private const string LOG4NET_APPLICATION_ID_PARAM_UNCONFIGURED = "UNCONFIGURED";
        private const string DEFAULT_APPENDER = "AdoNetAppender";
        private const string DEFAULT_FILE_APPENDER = "RollingFileAppender";
        private const string DEFAULT_FILENAME_LOG = "log-file.txt";
        public const string DEFAULT_LOG4NET_CS = "log4netConnectionString";

        private static bool _asyncConfigured = false;
        private static object _asyncConfiguredDecoy = new object();
        private static bool _fileLogInitiated = false;
        private static object _key = new object();
        private static bool _logInitiated = false;
        
        private static Dictionary<System.Type, ILog> _logs = new Dictionary<System.Type, ILog>();
        private static object _lock = new object();
        private const int STACK_FRAME = 3;
        private static System.Type _thisType = typeof(Log4NetHelper);

        static Log4NetHelper()
        {
            AsynchConfigure();
        }

        public static ILog LogInstance
        {
            get
            {
                return HetLogInstance(3);
            }
        }

        private static void _Configure()
        {
            if (!_asyncConfigured)
            {
                lock (_asyncConfiguredDecoy)
                {
                    _asyncConfigured = true;
                    try
                    {
                        XmlConfigurator.Configure();
                        Configure();
                    }
                    catch (System.Exception exception)
                    {
                        Trace.WriteLine(exception.Message + " " + exception.StackTrace);
                    }
                }
            }
        }

        public static void AsynchConfigure()
        {
            System.Threading.ThreadStart start = new System.Threading.ThreadStart(Log4NetHelper._Configure);
            new System.Threading.Thread(start).Start();
        }

        public static void Configure()
        {
            try
            {
                string defaultConnectionString = ApplicationSettings.GetConnectionString(DEFAULT_LOG4NET_CS);
                string applicationId = ApplicationSettings.GetApplicationSetting(APPLICATION_ID_CONFIG_NAME);
                Configure(defaultConnectionString, applicationId);
            }
            catch (System.Exception exception)
            {
                Trace.WriteLine(exception.Message + " " + exception.StackTrace);
            }
        }

        public static void Configure(string defaultConnectionString, string applicationId)
        {
            if (!_logInitiated)
            {
                lock (_lock)
                {
                    if (!_logInitiated)
                    {
                        try
                        {
                            XmlConfigurator.Configure();
                            log4net.Repository.Hierarchy.Hierarchy repository = LogManager.GetRepository() as log4net.Repository.Hierarchy.Hierarchy;
                            if (repository != null)
                            {
                                AdoNetAppender appender = (AdoNetAppender)repository.Root.GetAppender(DEFAULT_APPENDER);
                                if (appender != null)
                                {
                                    string str = defaultConnectionString;
                                    if (string.IsNullOrWhiteSpace(str))
                                    {
                                        str = ApplicationSettings.GetConnectionString(DEFAULT_LOG4NET_CS);
                                    }
                                    if (str != null)
                                    {
                                        appender.ConnectionString = str;
                                        appender.ActivateOptions();
                                    }
                                    else
                                    {
                                        return;
                                    }
                                }
                                GlobalContext.Properties["ApplicationId"] = string.IsNullOrWhiteSpace(applicationId) ? ((object)"UNCONFIGURED") : ((object)applicationId);
                                _logInitiated = true;
                            }
                        }
                        catch (System.Exception exception)
                        {
                            Trace.WriteLine(exception.Message + " " + exception.StackTrace);
                        }
                    }
                }
            }
        }

        public static void ConfigureFromFile(string rootPath, string filename)
        {
            if (!string.IsNullOrWhiteSpace(filename))
            {
                lock (_lock)
                {
                    try
                    {
                        System.IO.FileInfo info = new System.IO.FileInfo(filename);
                        if (!info.Exists || !info.DirectoryName.Equals(rootPath))
                        {
                            filename = System.IO.Path.Combine(rootPath, filename);
                            info = new System.IO.FileInfo(filename);
                            if (!info.Exists)
                            {
                                return;
                            }
                        }
                        XmlConfigurator.Configure((System.IO.FileInfo)info);
                    }
                    catch (System.Exception exception)
                    {
                        Trace.WriteLine(exception.Message + " " + exception.StackTrace);
                        throw exception;
                    }
                }
            }
        }

        public static void ConfigureRollingFileAppenders(string rootPath, string filename)
        {
            if (!_fileLogInitiated && !string.IsNullOrWhiteSpace(rootPath))
            {
                lock (_lock)
                {
                    if (!_fileLogInitiated)
                    {
                        try
                        {
                            log4net.Repository.Hierarchy.Hierarchy repository = LogManager.GetRepository() as log4net.Repository.Hierarchy.Hierarchy;
                            RollingFileAppender newAppender = null;

                            if (repository != null)
                                newAppender = (RollingFileAppender)repository.Root.GetAppender(DEFAULT_FILE_APPENDER);
                            else
                            {
                                repository = LogManager.CreateRepository((System.Type)typeof(log4net.Repository.Hierarchy.Hierarchy)) as log4net.Repository.Hierarchy.Hierarchy;
                                repository.Configured = true;
                            }

                            bool flag = false;
                            if (newAppender == null)
                            {
                                flag = true;
                                newAppender = new RollingFileAppender
                                {
                                    AppendToFile = true,
                                    LockingModel = new FileAppender.MinimalLock(),
                                    RollingStyle = RollingFileAppender.RollingMode.Size,
                                    MaxSizeRollBackups = 10,
                                    MaximumFileSize = "5000KB",
                                    StaticLogFileName = true,
                                    File = DEFAULT_FILENAME_LOG,
                                    Layout = new PatternLayout("%-5p %d %5rms %-30.30c{1} %-30.30M [%property{NDC}] - %message%newline")
                                };

                                repository.Root.AddAppender(newAppender);
                            }
                            if (newAppender != null)
                            {
                                if (string.IsNullOrWhiteSpace(filename))
                                {
                                    if (string.IsNullOrWhiteSpace(newAppender.File))
                                        filename = DEFAULT_FILENAME_LOG;
                                    else
                                        filename = new System.IO.FileInfo(newAppender.File).Name;
                                }

                                var fullName = System.IO.Path.Combine(rootPath, filename);
                                
                                var info2 = new System.IO.FileInfo(fullName);
                                if (!info2.Directory.Exists)
                                    System.IO.Directory.CreateDirectory(info2.DirectoryName);
                                
                                if (!info2.Directory.Exists || !info2.Directory.FullName.ToUpper().Equals(rootPath.Trim().ToUpper()))
                                    fullName = System.IO.Path.Combine(rootPath, fullName);
                                
                                newAppender.StaticLogFileName = true;
                                newAppender.File = fullName;
                                
                                if (flag)
                                    BasicConfigurator.Configure(newAppender);

                                newAppender.ActivateOptions();
                            }
                            _fileLogInitiated = true;
                        }
                        catch (System.Exception exception)
                        {
                            Trace.WriteLine(exception.Message + " " + exception.StackTrace);
                        }
                    }
                }
            }
        }

        private static System.Type GetCallingType(int stackFrame)
        {
            var trace = new System.Diagnostics.StackTrace();
            
            if (stackFrame > trace.FrameCount)
                stackFrame = (int)(trace.FrameCount - 1);

            var frame = trace.GetFrame(stackFrame);
            if (frame != null)
                return frame.GetMethod().DeclaringType;

            return _thisType;
        }

        public static ILog HetLogInstance(int stackFrame)
        {
            return GetLogInstance(GetCallingType(stackFrame));
        }

        public static ILog GetLogInstance(System.Type callingType)
        {
            ILog log = null;

            if (callingType == null)
                callingType = _thisType;

            if (_logInitiated)
            {
                if (!_logs.ContainsKey(callingType))
                    lock (_key)
                    {
                        if (!_logs.ContainsKey(callingType))
                        {
                            log = LogManager.GetLogger((System.Type)callingType);
                            _logs.Add(callingType, log);
                            return log;
                        }
                        return _logs[callingType];
                    }

                return _logs[callingType];
            }

            return LogManager.GetLogger((System.Type)callingType);
        }
    }
}