using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace IpClient.Clients
{
    class TCP : IClient
    {
        TcpClient Client { get; set; } = new TcpClient();
        IPEndPoint Remote { get; set; }
        NetworkStream Stream { get; set; }
        public int Timeout { get; set; } = -1;

        public TCP(IPAddress ip, int port)
        {
            Remote = new IPEndPoint(ip, port);
        }

        public void Connect()
        {
            Client.Connect(Remote);
            Stream = Client.GetStream();
        }

        public void Disconnect()
        {
            Stream.Dispose();
            Client.Close();
            Client.Dispose();
        }

        public void Send(byte[] request)
        {
            Stream.Write(request, 0, request.Length);
        }

        public byte[] Receive()
        {
            var data = new byte[1024];
            var num = Stream.Read(data, 0, data.Length);
            var response = data.Take(num).ToArray();
            return response;
        }
    }
}
