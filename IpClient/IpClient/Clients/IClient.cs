using System;
using System.Collections.Generic;
using System.Text;

namespace IpClient.Clients
{
    interface IClient
    {
        void Connect();
        void Disconnect();
        void Send(byte[] request);
        byte[] Receive();
    }
}
