using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucas.Solutions
{
    /// <summary>
    /// Enumerate TEnum fields by name
    /// </summary>
    /// <typeparam name="TEnum"></typeparam>
    public class EnumStore<TEnum>
        where TEnum : struct
    {
        public IEnumerable<TEnum> Values { get { return null; } }
    }
}
