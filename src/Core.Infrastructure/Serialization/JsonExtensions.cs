using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucas.Solutions.Serialization
{
    using Newtonsoft.Json;

    public static class JsonExtensions
    {
        public static string ToJson(this object value)
        {
           return JsonConvert.SerializeObject(value);
        }
    }
}
