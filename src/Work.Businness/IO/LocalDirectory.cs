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

        public string Path
        {
            get { return System.IO.Path.Combine(Party.Host.Address, Party.Path.Trim(new [] { '/', '\\'})); } 
        }

        public ICollection<LocalFile> GetFiles()
        {
            var info = new DirectoryInfo(Path);

            return info.Exists
                ? Array.AsReadOnly(info.GetFiles("*.*", Party.Recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly)
                    .Select(file => new LocalFile(file.FullName.Substring(info.FullName.Length), this))
                    .ToArray())
                : null;
        }
    }
}
