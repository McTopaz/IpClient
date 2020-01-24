﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Globalization;
using System.Linq;

using Xamarin.Forms;

namespace IpClient.Misc
{
    public class TextToIpAddress : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var ip = value as IPAddress;
            return ip.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = value.ToString();
            if (IPAddress.TryParse(str, out IPAddress ip))
            {
                return ip;
            }
            else
            {
                return str;
            }
        }
    }

    public class TextToPort : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var port = (ushort)value;
            return port.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = value.ToString();
            if (!ushort.TryParse(str, out ushort port))
            {
                return port;
            }
            else
            {
                return str;
            }
        }
    }

    public class TextToBytes : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var request = value as IEnumerable<byte>;
            var str = string.Join(" ", request);
            return str;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = value.ToString();
            str = str.Replace(" ", "");
            return Enumerable.Range(0, str.Length)
                     .Where(x => x % 2 == 0)
                     .Select(x => System.Convert.ToByte(str.Substring(x, 2), 16));
        }
    }
}
