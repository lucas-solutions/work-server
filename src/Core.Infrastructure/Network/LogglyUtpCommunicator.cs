using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Web;

namespace Lucas.Solutions.Network
{
    public class LogglyUtpCommunicator : LogglyCommunicator, IDisposable
    {
        private IPEndPoint _ipEndPoint;
        private UdpClient _udpClient;

        public LogglyUtpCommunicator()
        {
        }

        public IPAddress IPAddress
        {
            get { return IPAddress.Parse("255.255.255.255"); }
        }

        public int Port
        {
            get { return 11; }
        }

        public IPEndPoint IPEndPoint
        {
            get
            {
                return _ipEndPoint ?? (_ipEndPoint = new IPEndPoint(IPAddress, Port));
            }
        }

        public UdpClient UdpClient
        {
            get { return _udpClient ?? (_udpClient = new UdpClient(IPEndPoint)); }
        }

        public void Send(Dictionary<string, object> sendData)
        {
            var sendText = "";
            Send(sendText);
        }

        public void Send(string sendText)
        {
            var sendBytes = Encoding.ASCII.GetBytes(sendText);

            UdpClient.Send(sendBytes, sendBytes.Length, IPEndPoint);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                var udpClient = _udpClient;

                _udpClient = null;

                if (udpClient != null)
                {
                    // calls internal dispose
                    udpClient.Close();
                }

                GC.SuppressFinalize(this);
            }
        }

        void IDisposable.Dispose()
        {
            this.Dispose(true);
        }
    }
}