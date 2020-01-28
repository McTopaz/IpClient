using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net;

using Xamarin.Forms;

using IpClient.Misc;
using IpClient.Clients;

using PropertyChanged;

namespace IpClient.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    class vmMainPage
    {
        public const string UDP = "UDP";
        public const string TCP = "TCP";

        public IPAddress IpAddress { get; set; } = IPAddress.Parse("192.168.8.107");
        public ushort Port { get; set; } = 10001;
        public string Protocol { get; set; } = UDP;
        public uint Timeout { get; set; } = 3000;
        public IEnumerable<string> Protocols { get; private set; } = new string[] { UDP, TCP };
        public string Request { get; set; } = "DEADBEEF";
        public Color RequestColor { get; set; }
        public bool EnableSendAndRecive { get; set; } = true;
        public string Response { get; set; }

        public RelayCommand RequestChanged { get; private set; } = new RelayCommand();
        public RelayCommand SendAndReceive { get; private set; } = new RelayCommand();

        public vmMainPage()
        {
            RequestChanged.Callback += RequestChanged_Callback;
            SendAndReceive.Callback += SendAndReceive_Callback;
        }

        private void RequestChanged_Callback(object parameter = null)
        {
            var even = Request.Length % 2 == 0;
            var allHex = Enumerable.Range(0, Request.Length)
                .Select(i => Request[i])
                .All(c => Uri.IsHexDigit(c));
            var ok = even && allHex;
            RequestColor = ok ? Color.FromRgba(0, 0, 0, 0) : Color.Red;
            EnableSendAndRecive = ok;
        }

        private void SendAndReceive_Callback(object parameter = null)
        {
            Response = ClientHandler.SendAndReceive(IpAddress, Port, Protocol, Request);
        }
    }
}
