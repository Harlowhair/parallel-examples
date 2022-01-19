using System;
using System.Collections.Generic;
using System.Text;

namespace CoreConsole.Extensions
{
    public static class StringExtensions
    {
        public static string ToDomain(this string str)
        {
            try
            {
                return new Uri(str).DnsSafeHost.ToLower().Replace("www.", "");
            }
            catch
            {
                return "";
            }
        }

        public static string ToReverseDomain(this string str)
        {
            try
            {
                var domain = new Uri(str).DnsSafeHost.ToLower().Replace("www.", "");
                return string.Join('.', domain.Split('.')); //.Reverse();
            }
            catch
            {
                return "";
            }
        }
    }
}
