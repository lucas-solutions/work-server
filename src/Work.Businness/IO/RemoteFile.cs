using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Lucas.Solutions.IO
{
    public class RemoteFile : TransferFile
    {
        public RemoteFile(RemoteDirectory directory)
        {
            Directory = directory;
        }

        public RemoteDirectory Directory
        {
            get;
            private set;
        }

        public Uri Url
        {
            get { return new Uri(Directory.Url, Path); }
        }

        protected static void Copy(Stream source, Stream target)
        {
            var buffer = new byte[0x1000];
            int count, position = 0;
            for (; buffer.Length == (count = source.Read(buffer, 0, buffer.Length)); position += count)
                target.Write(buffer, 0, count);
            target.Write(buffer, 0, count);
            position += count;
        }

        public Task PullAsync(Stream stream, Action<float> progress)
        {
            return new Task(() =>
            {
                var request = (FtpWebRequest)WebRequest.Create(Url);
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Credentials = Directory.Credentials;

                var response = (FtpWebResponse)request.GetResponse();
                var responseStream = response.GetResponseStream();
                
                Copy(responseStream, stream);
                stream.Flush();
                stream.Close();

                responseStream.Close();
                
                Console.WriteLine("Download File Complete, status {0}", response.StatusDescription);

                response.Close();
            });
        }

        public override Task PushAsync(Stream stream, Action<float> progress)
        {
            return new Task(() =>
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(Url);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = Directory.Credentials;
                request.ContentLength = stream.Length;

                var requestStream = request.GetRequestStream();
                Copy(stream, requestStream);
                requestStream.Close();

                var response = (FtpWebResponse)request.GetResponse();

                Console.WriteLine("Upload File Complete, status {0}", response.StatusDescription);

                response.Close();
            });
        }
    }
}
