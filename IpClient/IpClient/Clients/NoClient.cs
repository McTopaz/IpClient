using System;
using System.Collections.Generic;
using System.Text;

namespace IpClient.Clients
{
    class NoClient : IClient
    {
        public void Connect()
        {
        }

        public void Disconnect()
        {
        }

        public byte[] Receive()
        {
            return new byte[0];
        }

        public void Send(byte[] request)
        {
        }
    }
}
