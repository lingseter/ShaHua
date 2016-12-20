using System;
using System.Text.RegularExpressions;

namespace Utility
{
    public class NumberHelper
    {
        public static int ToInt(string str,int defaultValue)
        {
            try
            {
                return Convert.ToInt32(str);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static bool IsNumber(string sValue)
        {
            Regex r = new Regex(@"^(-)?\d+(\.)?\d*$");
            if (r.IsMatch(sValue))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsInt(string sValue)
        {
            return new Regex(@"\d+").IsMatch(sValue);
        }

        public static decimal ToDecimal(string s, decimal default_value)
        {
            try
            {
                return decimal.Parse(s);
            }
            catch
            {
                return default_value;
            }
        }
    }
}
