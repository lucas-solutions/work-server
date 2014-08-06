using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Custom.Diagnostics
{
    public class ControllerLog : ControllerLog<ControllerLog>
    {
        public ControllerLog(ILogger logger)
            : this(logger, null)
        {
        }

        public ControllerLog(ILogger logger, IDictionary<string, object> data)
            : base(logger, data)
        {
        }
    }

    public abstract class ControllerLog<TLog> : RequestLog<TLog>
        where TLog : ControllerLog<TLog> 
    {
        protected ControllerLog(ILogger logger, IDictionary<string, object> data)
            : base(logger, data)
        {
        }

        public TLog ActionLatency(TimeSpan latency)
        {
            Context["ActionLatency"] = latency.ToString();
            return (TLog)this;
        }

        public TLog Controller(ControllerBase controller)
        {
            return this
                .Request(controller.ControllerContext.RequestContext);
        }

        public TLog Request(RequestContext requestContext)
        {
            return this
                .Request(requestContext.HttpContext.Request)
                .Route(requestContext.RouteData);
        }

        public TLog ResultLatency(TimeSpan latency)
        {
            Context["ResultLatency"] = latency.ToString();
            return (TLog)this;
        }

        public TLog Route(RouteData route)
        {
            Context["Controller"] = route.Values["controller"];
            Context["Action"] = route.Values["action"];
            Context["Area"] = route.Values["area"];

            return (TLog)this;
        }
    }
}