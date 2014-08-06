using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucas.Solutions.IO
{
    using Lucas.Solutions.Automation;

    public interface ITransferWorker : IWorker<Transfer>, IProgress
    {
        Transfer Transfer { get; set; }
    }
}
