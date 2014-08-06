using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucas.Solutions.Configuration
{
    public static class ApplicationSettings
    {
        public static string GetApplicationSetting(string name)
        {
            return string.IsNullOrWhiteSpace(name) ? null : ConfigurationManager.AppSettings[name];
        }

        public static string GetConnectionString(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return null;

            var value = ConfigurationManager.ConnectionStrings[name];
            return value != null ? value.ToString() : null;
        }
    }
}
