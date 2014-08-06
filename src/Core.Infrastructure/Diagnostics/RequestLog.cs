using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace Lucas.Solutions.Diagnostics
{
    public class RequestLog : RequestLog<RequestLog>
    {
        public RequestLog(ILogger logger)
            : this(logger, null)
        {
        }

        public RequestLog(ILogger logger, IDictionary<string, object> data)
            : base(logger, data)
        {
        }
    }

    public abstract class RequestLog<TLog> : WebLog<TLog>
        where TLog : RequestLog<TLog>
    {
        protected RequestLog(ILogger logger, IDictionary<string, object> data)
            : base(logger, data)
        {
        }

        public TLog IP(string value)
        {
            Context["IP"] = value;
            return (TLog)this;
        }

        public TLog Request(HttpRequest request)
        {
            return this
                .Method(request.HttpMethod)
                .IP(request.UserHostAddress)
                .ServerVariables(request.ServerVariables);
        }

        public TLog Request(HttpRequestBase request)
        {
            return this
                .Method(request.HttpMethod)
                .IP(request.UserHostAddress)
                .ServerVariables(request.ServerVariables);
        }

        protected TLog ServerVariables(NameValueCollection serverVariables)
        {
            Context["AuthUser"] = serverVariables["AUTH_USER"];
            Context["UserAgent"] = serverVariables["HTTP_USER_AGENT"];
            Context["QueryString"] = serverVariables["QueryString"];
            Context["Url"] = serverVariables["URL"];
            return (TLog)this;
        }
    }
}