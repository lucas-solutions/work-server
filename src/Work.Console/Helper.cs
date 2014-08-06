using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucas.Solutions
{
    public static class Helper
    {
        public static void Assert<T>(this T self, T other, string message = null)
            where T : IComparable<T>
        {
            if (!self.Equals(other))
                throw new Exception(message);
        }

        public static void Assert<T>(this T self, T other, Func<string> message)
            where T : IComparable<T>
        {
            if (!self.Equals(other))
                throw new Exception(message != null ? message() : null);
        }
    }
}
