using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace IpClient.Clients
{
    class UDP : IClient
    {
        UdpClient Client { get; set; } = new UdpClient();
        IPEndPoint Remote { get; set; }
        public int Timeout { get; set; } = -1;

        public UDP(IPAddress ip, int port)
        {
            Remote = new IPEndPoint(ip, port);
        }

        public void Connect()
        {
        }

        public void Disconnect()
        {
            Client.Close();
            Client.Dispose();
        }

        public void Send(byte[] request)
        {
            Client.Client.SendTimeout = Timeout;
            Client.Send(request, request.Length, Remote);
        }

        public byte[] Receive()
        {
            Client.Client.ReceiveTimeout = Timeout;
            var remote = Remote;
            return Client.Receive(ref remote);
        }
    }
}
