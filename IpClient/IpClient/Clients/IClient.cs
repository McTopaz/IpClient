using System;
using System.Collections.Generic;
using System.Text;

namespace IpClient.Clients
{
    interface IClient
    {
        int Timeout { get; set; }
        void Connect();
        void Disconnect();
        void Send(byte[] request);
        byte[] Receive();
    }
}
