using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lucas.Solutions.Diagnostics
{
    using Lucas.Solutions.Diagnostics.Responses;
    using Newtonsoft.Json;

    public class LogContext<TLog>
        where TLog : Log<TLog>
    {
        public static implicit operator Action<LogResponse>(LogContext<TLog> context)
        {
            return response =>
            {
                context.Response = response;
                context.Completed = true;
                var callback = context.Callback;
                if (callback != null)
                    callback(context);
            };
        }

        private readonly ILogger _logger;
        private readonly IDictionary<string, object> _data;
        private bool _sent;

        public LogContext(ILogger logger, IDictionary<string, object> data)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");

            _logger = logger;
            _data = data ?? new Dictionary<string, object>();
        }

        public object this[string name]
        {
            get
            {
                object value;
                if (!string.IsNullOrWhiteSpace(name) && _data.TryGetValue(name, out value))
                    return value;

                return null;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(name) && value != null && !string.IsNullOrWhiteSpace(value.ToString()))
                    _data[name] = value;
            }
        }

        public Action<LogContext<TLog>> Callback
        {
            get;
            set;
        }

        public LogCategories Category
        {
            get;
            set;
        }

        public IDictionary<string, object> Data
        {
            get { return _data; }
        }

        public ILogger Logger
        {
            get { return _logger; }
        }

        public LogResponse Response
        {
            get;
            set;
        }

        public bool Completed
        {
            get;
            set;
        }

        public bool Sending
        {
            get { return _sent && !Completed; }
        }

        public void Sent()
        {
            _sent = true;
        }

        /// <summary>
        /// Return JSON string
        /// </summary>
        /// <returns>JSON string</returns>
        public override string ToString()
        {
            var setting = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            var json = JsonConvert.SerializeObject(Data/*, setting*/);

            return json;
        }
    }
}