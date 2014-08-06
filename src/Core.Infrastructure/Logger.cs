using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucas.Solutions
{
    using log4net.Core;
    using log4net.Repository;

    public class Logger : ILogger
    {
        bool ILogger.IsEnabledFor(Level level)
        {
            throw new NotImplementedException();
        }

        void ILogger.Log(LoggingEvent logEvent)
        {
            throw new NotImplementedException();
        }

        void ILogger.Log(Type callerStackBoundaryDeclaringType, Level level, object message, Exception exception)
        {
            throw new NotImplementedException();
        }

        string ILogger.Name
        {
            get { throw new NotImplementedException(); }
        }

        ILoggerRepository ILogger.Repository
        {
            get { throw new NotImplementedException(); }
        }
    }
}
