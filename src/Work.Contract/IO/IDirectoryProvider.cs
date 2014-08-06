using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucas.Solutions.IO
{
    public interface IDirectoryProvider
    {
        TransferDirectory Resolve(Party party);
    }
}
