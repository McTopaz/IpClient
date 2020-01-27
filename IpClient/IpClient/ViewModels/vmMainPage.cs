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

        public IPAddress IpAddress { get; set; } = IPAddress.Broadcast;
        public ushort Port { get; set; } = 10001;
        public string Protocol { get; set; } = UDP;
        public uint Timeout { get; set; } = 3000;
        public IEnumerable<string> Protocols { get; private set; } = new string[] { UDP, TCP };
        public string Request { get; set; } = "DEADBEEF";
        public Color RequestColor { get; set; }
        public bool EnableSendAndRecive { get; set; } = false;
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
            var allHex = Enumerable.Range(0, Request.Length)
                .Select(i => Request[i])
                .All(c => Uri.IsHexDigit(c));
            RequestColor = allHex ? Color.FromRgba(0, 0, 0, 0) : Color.Red;
            EnableSendAndRecive = allHex;
        }

        private void SendAndReceive_Callback(object parameter = null)
        {
            Response = ClientHandler.SendAndReceive(IpAddress, Port, Protocol, Request);
        }
    }
}
