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
        public static string SendAndReceive(IPAddress ip, int port, string protocol, string request, int timeout)
        {
            try
            {
                var client = LookupClient(ip, port, protocol);
                client.Timeout = timeout;
                var req = Hex2Bytes(request);
                var res = SendAndReceive(client, req);
                var response = Bytes2Hex(res);
                return response;
            }
            catch (Exception e)
            {
                return e.Message;
            }
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
            var data = new byte[0];
            try
            {
                client.Send(request);
                data = client.Receive();
            }
            catch (Exception e)
            {
                throw e;    // Rethrow to access the pass the exception's message to the app's response-field.
            }
            finally
            {
                client.Disconnect();
            }
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
