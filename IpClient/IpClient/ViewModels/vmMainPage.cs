using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

using IpClient.Misc;

using PropertyChanged;

namespace IpClient.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    class vmMainPage
    {
        public IPAddress IpAddress { get; set; } = IPAddress.Broadcast;
        public ushort Port { get; set; } = 10001;
        public string Protocol { get; set; } = "UDP";
        public uint Timeout { get; set; } = 3000;
        public IEnumerable<string> Protocols { get; private set; } = new string[] { "UDP", "TCP" };
        public IEnumerable<byte> Request { get; set; } = new byte[] { 0xDE, 0xAD, 0xBE, 0xEF };
        public IEnumerable<byte> Response = new List<byte>();

        public RelayCommand Send { get; private set; } = new RelayCommand();

        public vmMainPage()
        {
            Send.Callback += Send_Callback;
        }

        private void Send_Callback(object parameter = null)
        {
            
        }
    }
}
