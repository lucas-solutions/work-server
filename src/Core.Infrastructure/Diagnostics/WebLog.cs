using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Web;
using System.Web.Hosting;

namespace Lucas.Solutions.Diagnostics
{
    public class WebLog<TLog> : Log<TLog>
         where TLog : WebLog<TLog>
    {
        private static readonly bool _development;
        private static readonly string _application;
        private static readonly Version _version;
        private static readonly string _hostname;
        private static readonly IPAddress _localAddr;

        static WebLog()
        {   
            var assembly = GetApplicationAssembly();
            var assemblyName = assembly.GetName();

            _application = assemblyName.Name ?? HostingEnvironment.SiteName;
            _version = assemblyName.Version;
            _hostname = Dns.GetHostName();
            _localAddr = Dns.GetHostEntry(_hostname)
                .AddressList
                .First(ipAddress => ipAddress.AddressFamily == AddressFamily.InterNetwork);
            _development = HostingEnvironment.IsDevelopmentEnvironment;
        }

        public WebLog(ILogger logger)
            : this(logger, null)
        {
            
        }

        public WebLog(ILogger logger, IDictionary<string, object> data)
            : base(logger, data)
        {
            this
                .Application(_application)
                .Version(_version)
                .Hostname(_hostname)
                .LocalAddr(_localAddr)
                .Development(_development);
        }

        private const string AspNetNamespace = "ASP";

        private static Assembly GetApplicationAssembly()
        {
            // Try the EntryAssembly, this doesn't work for ASP.NET classic pipeline (untested on integrated)
            Assembly ass = Assembly.GetEntryAssembly();

            // Look for web application assembly
            HttpContext ctx = HttpContext.Current;
            if (ctx != null)
                ass = GetWebApplicationAssembly(ctx);

            // Fallback to executing assembly
            return ass ?? (Assembly.GetExecutingAssembly());
        }

        private static Assembly GetWebApplicationAssembly(HttpContext context)
        {
            //Guard.AgainstNullArgument(context);

            object app = context.ApplicationInstance;
            if (app == null) return null;

            Type type = app.GetType();
            while (type != null && type != typeof(object) && type.Namespace == AspNetNamespace)
                type = type.BaseType;

            return type.Assembly;
        }
    }
}