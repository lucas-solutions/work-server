using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucas.Solutions.IO
{
    public class LocalDirectory : TransferDirectory
    {
        public LocalDirectory(Party party)
            : base(party)
        {
        }

        public IEnumerable<LocalFile> GetFiles()
        {
            var path = System.IO.Path.Combine(Party.Host.Address, Party.Path);
            var info = new DirectoryInfo(path);

            return info.Exists
                ? Array.AsReadOnly(info.GetFiles("*.*", Party.Recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly)
                    .Select(file => new LocalFile(file, this))
                    .ToArray())
                : null;
        }
    }
}
