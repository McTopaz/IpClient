using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net;

using IpClient.Clients;

namespace IpClient
{
    static class ClientHandler
    {
        public static string SendAndReceive(IPAddress ip, int port, string protocol, string request)
        {
            var client = LookupClient(ip, port, protocol);
            var req = Hex2Bytes(request);
            var res = SendAndReceive(client, req);
            var response = Bytes2Hex(res);
            return response;
        }

        private static IClient LookupClient(IPAddress ip, int port, string protocol)
        {
            if (protocol == ViewModels.vmMainPage.UDP)
            {
                return new UDP(ip, port);
            }
            else if (protocol == ViewModels.vmMainPage.TCP)
            {
                return new TCP(ip, port);
            }
            else
            {
                return new NoClient();
            }
        }

        private static byte[] Hex2Bytes(string data)
        {
            return Enumerable.Range(0, data.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(data.Substring(x, 2), 16))
                .ToArray();
        }

        private static byte[] SendAndReceive(IClient client, byte[] request)
        {
            client.Connect();
            client.Send(request);
            var data = client.Receive();
            client.Disconnect();
            return data;
        }

        private static string Bytes2Hex(byte[] data)
        {
            var columns = 12;
            var line = "";
            for (int i = 0; i < data.Length; i++)
            {
                if (((i % columns) == 0) && i != 0)
                {
                    line += Environment.NewLine;
                }
                line += $"{data[i].ToString("X2")} ";
            }
            return line;
        }
    }
}
