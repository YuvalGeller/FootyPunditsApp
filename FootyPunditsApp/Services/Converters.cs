using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace FootyPunditsApp.Services
{
    public class ImageSourceConverter : IValueConverter
    {
        static WebClient Client = new WebClient();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            byte[] byteArray;
            try
            {
                byteArray = Client.DownloadData(value.ToString());
            }
            catch (System.Net.WebException e)
            {
                return null;
            }

            return ImageSource.FromStream(() => new MemoryStream(byteArray));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class TeamImageConverter : IValueConverter
    {
        private static readonly Regex sWhitespace = new Regex(@"\s+");
        public static string ReplaceWhitespace(string input, string replacement)
        {
            return sWhitespace.Replace(input, replacement);
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                //string imgPath = ReplaceWhitespace(((string)value + ".png"), "");
                string imgPath = "";
                foreach (char c in (string)value)
                {
                    if ("abcdefghijklmnopqrstuvwxyz1234567890".Contains(c.ToString().ToLower()))
                    {
                        imgPath += c;
                    }
                }
                return imgPath + ".png";
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
