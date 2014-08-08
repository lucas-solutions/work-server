using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lucas.Solutions.IO
{
    public class RemoteDirectory : TransferDirectory
    {
        private static Regex UnixEntry = new Regex(
            @"^([\-dbclps])" +                // Directory flag [1]
            @"([\-rwxs]{9})\s+" +            // Permissions [2]
            @"(\d+)\s+" +                    // Number of items [3]
            @"(\w+)\s+" +                    // File owner [4]
            @"(\w+)\s+" +                    // File group [5]
            @"(\d+)\s+" +                    // File size in bytes [6]
            @"(\w{3}\s+\d{1,2}\s+" +       // 3-char month and 1/2-char day of the month [7]
            @"(?:\d{1,2}:\d{1,2}|\d{4}))" + // Time or year (need to check conditions) [+= 7]
            @"\s+(.+)$'"                      // File/directory name [8]
        );

        private static Regex WindowsFile = new Regex(
            @"^(\d\d-\d\d-\d\d)\s+" + // Date
            @"(\d\d:\d\d(AM|PM))\s+" +  // Time, 
            @"([\w<>]*)\s+" +              // Type 
            @"(\d*)\s+" +                  // Size 
            @"([\w\._\-]+)\s*$"          // Name
            );

        delegate bool TryParse(string text, out Dictionary<string, object> data);

        private static bool TryParseUnixEntry(string text, out Dictionary<string, object> data)
        {
            var result = new Dictionary<string, object>();

            if (UnixEntry.IsMatch(text))
            {
                var match = UnixEntry.Match(text);
                result["Type"] = match.Groups[1].Value;
                var type = match.Groups[1].Value;
                switch (type)
                {
                    case "<DIR>":
                        result["Type"] = "DIR";
                        data = result;
                        return true;
                    default:
                        result["Type"] = match.Groups[1].Value;
                        result["perms"] = match.Groups[2].Value;
                        result["items"] = match.Groups[3].Value;
                        result["owner"] = match.Groups[4].Value;
                        result["group"] = match.Groups[5].Value;
                        result["Size"] = long.Parse(match.Groups[6].Value);
                        result["ModifidOn"] = match.Groups[7];
                        result["Path"] = match.Groups[8].Value;

                        data = result;
                        return true;
                }
            }

            data = null;
            return false;
        }

        private static bool TryParseWindowsFile(string text, out Dictionary<string, object> data)
        {
            var result = new Dictionary<string, object>();

            if (WindowsFile.IsMatch(text))
            {
                var match = WindowsFile.Match(text);
                //result.Add("Type", matches[1].Value);
                var type = match.Groups[4].Value;
                var dateS = match.Groups[1].Value + " " + match.Groups[2].Value;
                var sizeS = match.Groups[5].Value;
                long size;
                var sizeValid = long.TryParse(sizeS, out size);
                DateTime date;
                var dateValid = DateTime.TryParseExact(dateS, new[] { "MM-dd-yy hh:sstt", "MM-dd-yy hh-sstt" }, null, DateTimeStyles.AllowWhiteSpaces, out date); ;
                switch (type)
                {
                    case "<DIR>":
                        result.Add("Date", date);
                        result.Add("Dir", true);
                        result.Add("Size", -1);
                        result.Add("Name", match.Groups[6].Value);
                        data = result;
                        return true;
                    default:
                        result.Add("Date", date);
                        result.Add("Dir", false);
                        result.Add("Size", sizeValid ? size : -1);
                        result.Add("Name", match.Groups[6].Value);
                        data = result;
                        return true;
                }
            }

            data = null;
            return false;
        }

        private static TryParse[] Parsers = new TryParse[]
            {   
                TryParseWindowsFile,
                TryParseUnixEntry
            };

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
                    _credentials = new NetworkCredential(Party.User ?? Party.Email, Party.Password);
                else if (Party.Host.Password != null)
                    _credentials = new NetworkCredential(Party.Host.User, Party.Host.Password);
                else
                    _credentials = new NetworkCredential("anonymous", Party.Email);

                return _credentials;
            }
        }

        public Uri Url
        {
            get
            {
                var url = new Uri(new Uri(Party.Host.Address, UriKind.Absolute), new Uri(Party.Path, UriKind.Relative));

                if (url.Scheme != Uri.UriSchemeFtp)
                {
                    // log warning
                }

                return url;
            }
        }

        public ICollection<RemoteFile> GetFiles()
        {
            var files = new List<RemoteFile>();
            var uriBase = new Uri(Party.Host.Address);
            if (uriBase.Scheme != Uri.UriSchemeFtp)
            {
                // Uri FTP uri scheme mising
                return Array.AsReadOnly(new RemoteFile[0]);
            }

            var directories = new Queue<string>();
            directories.Enqueue(Party.Path ?? "");

            do
            {
                var path = directories.Dequeue();

                var request = (FtpWebRequest)WebRequest.Create(new Uri(uriBase, path));
                request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                request.Credentials = Credentials;

                var response = (FtpWebResponse)request.GetResponse();
                var responseStream = response.GetResponseStream();

                Console.WriteLine("Directory List Complete, status {0}", response.StatusDescription);



                var reader = new StreamReader(responseStream);
                for (var line = reader.ReadLine(); line != null; line = reader.ReadLine())
                {
                    foreach (var parser in Parsers)
                    {
                        Dictionary<string, object> data;
                        if (parser(line, out data))
                        {
                            if ((bool)data["Dir"])
                            {
                                if (Party.Recursive)
                                {
                                    directories.Enqueue((string)data["Name"]);
                                }
                            }
                            else
                            {
                                files.Add(new RemoteFile((string)data["Name"], this)
                                {
                                    ModifiedOn = (DateTime)data["Date"],
                                    Size = (long)data["Size"],
                                });
                            }
                            break;
                        }
                    }
                    Console.WriteLine(line);
                }
                reader.Close();
                response.Close();
            }
            while (directories.Count > 0);

            return Array.AsReadOnly(files.ToArray());
        }
    }
}
