using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucas.Solutions
{
    public enum TalkFormat : byte
    {
        Binary = 0xFF,

        Text = 1,

        Json = 2,

        Xml = 4
    }
}
