using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Lucas.Solutions.IO
{
    public class RemoteDirectory : TransferDirectory
    {
        public ICredentials _credentials;

        public RemoteDirectory(Party party)
            : base(party)
        {
        }

        public ICredentials Credentials
        {
            get
            {
                if (_credentials != null)
                    return _credentials;

                if (Party.Password != null)
                    _credentials = new NetworkCredential(Party.Email, Party.Password);
                else if (Party.Host.Password != null)
                    _credentials = new NetworkCredential(Party.Host.Credential, Party.Host.Password);
                else
                    _credentials = new NetworkCredential("anonymous", Party.Email);

                return _credentials;
            }
        }

        public Uri Url
        {
            get
            {
                var url = new Uri(System.IO.Path.Combine(Party.Host.Address, Party.Path));

                if (url.Scheme != Uri.UriSchemeFtp)
                    url = new Uri(Path.Combine("ftp://", url.AbsoluteUri));

                return url;
            }
        }

        public IEnumerable<RemoteFile> GetFiles()
        {
            var uri = new Uri(Party.Host.Address);
            if (uri.Scheme != Uri.UriSchemeFtp)
            {
                // Uri FTP uri scheme mising
                return Array.AsReadOnly(new RemoteFile[0]);
            }

            var request = (FtpWebRequest)WebRequest.Create(uri);
            request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            request.Credentials = Credentials;

            var response = (FtpWebResponse)request.GetResponse();
            var responseStream = response.GetResponseStream();
            var reader = new StreamReader(responseStream);
            Console.WriteLine(reader.ReadToEnd());
            Console.WriteLine("Directory List Complete, status {0}", response.StatusDescription);
            reader.Close();
            response.Close();

            return Array.AsReadOnly(new RemoteFile[0]);
        }
    }
}
