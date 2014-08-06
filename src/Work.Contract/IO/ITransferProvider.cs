using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucas.Solutions.IO
{
    public class ITransferProvider
    {
        public Task PullAsync(Transfer transfer)
        {
            return null;
        }

        public Task PushAsync(Transfer transfer)
        {
            return null;
        }
    }
}
