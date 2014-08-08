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
        public RemoteFile(string relativePath, RemoteDirectory directory)
        {
            Path = relativePath;
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

        public override void Read(Stream outputStream, Action<float> progress)
        {
            var request = (FtpWebRequest)WebRequest.Create(Url);
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = Directory.Credentials;

            var response = (FtpWebResponse)request.GetResponse();
            var responseStream = response.GetResponseStream();

            Copy(responseStream, outputStream, progress);
            responseStream.Close();

            Console.WriteLine("Download File Complete, status {0}", response.StatusDescription);

            response.Close();
        }

        public override void Write(Stream inputStream, Action<float> progress)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(Url);
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = Directory.Credentials;
            request.ContentLength = inputStream.Length;

            var requestStream = request.GetRequestStream();
            Copy(inputStream, requestStream, progress);
            requestStream.Close();

            var response = (FtpWebResponse)request.GetResponse();

            Console.WriteLine("Upload File Complete, status {0}", response.StatusDescription);

            response.Close();
        }
    }
}
