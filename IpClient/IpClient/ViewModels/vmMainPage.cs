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
        public IEnumerable<string> Protocols { get; private set; } = new string[] { "UDP", "TCP" };
        public IEnumerable<byte> Request = new List<byte>();
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
