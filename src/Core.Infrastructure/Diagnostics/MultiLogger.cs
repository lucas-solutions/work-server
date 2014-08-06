using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Web;

namespace Lucas.Solutions.Diagnostics
{
    public class MultiLogger : Logger<MultiLogger>
    {
        private readonly ConcurrentDictionary<string, ILogger> _Loggers;

        public MultiLogger()
        {
            _Loggers = new ConcurrentDictionary<string, ILogger>();
        }

        protected override void Log<TLog>(TLog log)
        {
            var arr = _Loggers.Values.ToArray();
            foreach (var logger in arr)
                if (logger != null)
                    logger.Log(log);
        }

        public MultiLogger Add(string name, ILogger logger)
        {
            _Loggers.AddOrUpdate(name, logger, (string key, ILogger value) => value);
            return this;
        }

        public MultiLogger Remove(string name)
        {
            ILogger logger;
            _Loggers.TryRemove(name, out logger);
            return this;
        }
    }
}