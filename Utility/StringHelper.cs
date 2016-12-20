﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.XPath;

namespace Utility
{
    public class StringHelper
    {
        public static string[] Objects2Strings(object[] objs)
        {
            List<string> listStr = new List<string>();
            foreach (object o in objs) 
                listStr.Add(o.ToString());
            return listStr.ToArray();
        }

        public static string[] DecimalArray2StringArray(decimal[] ds)
        {
            List<string> listStr = new List<string>();
            foreach (decimal d in ds) 
                listStr.Add(d.ToString());
            return listStr.ToArray();
        }

        public static Dictionary<string, string> String2Dictionary(string content, char split_main, char split_sub)
        {
            Dictionary<string, string> dct_ret = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(content))
            {
                foreach (string s in content.Split(split_main))
                {
                    string[] c = s.Split(split_sub);
                    if (c.Length == 2)
                    {
                        dct_ret[c[0].Trim().ToUpper()] = c[1];
                    }
                }
            }
            return dct_ret;
        }

        public static string Array2String(string[] array, string separate)
        {
            if (array == null) 
                return string.Empty;
            StringBuilder sb_ret = new StringBuilder();
            for (int i = 0; i < array.Length; i++)
            {
                if (sb_ret.Length == 0)
                    sb_ret.Append(array[i]);
                else
                    sb_ret.Append(separate).Append(array[i]);
            }
            return sb_ret.ToString();
        }

        public static string ObjectArray2String(object[] array, string separate)
        {
            return Array2String(Objects2Strings(array), separate);
        }

        public static int[] StringArray2IntArray(string[] array)
        {
            List<int> ret = new List<int>();
            foreach (string s in array)
                ret.Add(int.Parse(s));
            return ret.ToArray();
        }

        public static int[] String2IntArray(string str, char separator)
        {
            return StringArray2IntArray(str.Split(separator));
        }

        public static int[] String2IntArray(string paraStr, string preFix, char separator)
        {
            return StringArray2IntArray(paraStr.Replace(preFix, "").Split(separator));
        }

        public static List<T> String2List<T>(string str, char separator)
        {
            List<T> result = new List<T>();
            if (!string.IsNullOrEmpty(str) && str.Length > 0)
            {
                Type t = typeof(T);
                foreach (string s in str.Trim().Split(separator))
                {
                    if (!string.IsNullOrEmpty(s) && s.Trim().Length > 0)
                    {
                        T item;
                        try
                        {
                            item = (T)Convert.ChangeType(s.Trim(), t);
                        }
                        catch
                        {

                            continue;
                        }
                        if (!result.Contains(item))
                            result.Add(item);
                    }
                }
            }
            return result;
        }       

        public static string IntArray2String(int[] array, string separate)
        {
            if (array == null) return string.Empty;
            StringBuilder sb_ret = new StringBuilder();
            for (int i = 0; i < array.Length; i++)
            {
                if (sb_ret.Length == 0)
                    sb_ret.Append(array[i]);
                else
                    sb_ret.Append(separate).Append(array[i]);
            }
            return sb_ret.ToString();
        }

        public static string Array2String<T>(IEnumerable<T> array, string separate)
            where T : struct
        {
            if (array == null) return string.Empty;
            StringBuilder sb_ret = new StringBuilder();
            foreach (T a in array)
            {
                if (sb_ret.Length == 0)
                    sb_ret.Append(a);
                else
                    sb_ret.Append(separate).Append(a);
            }
            return sb_ret.ToString();
        }

        public static string[] String2StringArray(string str, char separator)
        {
            return str.Split(separator);
        }

        public static string HtmlEncode(string theString)
        {
            theString = theString.Replace(">", "&gt;");
            theString = theString.Replace("<", "&lt;");
            theString = theString.Replace("  ", " &nbsp;");
            theString = theString.Replace("  ", " &nbsp;");
            theString = theString.Replace("\"", "&quot;");
            theString = theString.Replace("\'", "&#39;");
            theString = theString.Replace("\n", "<br/> ");
            return theString;
        }

        public static string HtmlDiscode(string theString)
        {
            theString = theString.Replace("&gt;", ">");
            theString = theString.Replace("&lt;", "<");
            theString = theString.Replace("&nbsp;", " ");
            theString = theString.Replace(" &nbsp;", "  ");
            theString = theString.Replace("&quot;", "\"");
            theString = theString.Replace("&#39;", "\'");
            theString = theString.Replace("<br/> ", "\n");
            return theString;
        }

        public static string DealHtml(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }
            str = Regex.Replace(str, @"\<(img)[^>]*>|<\/(img)>", "", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, @"\<(table|tbody|tr|td|th|)[^>]*>|<\/(table|tbody|tr|td|th|)>", "", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, @"\<(div|blockquote|fieldset|legend)[^>]*>|<\/(div|blockquote|fieldset|legend)>", "", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, @"\<(font|i|u|h[1-9]|s)[^>]*>|<\/(font|i|u|h[1-9]|s)>", "", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, @"\<(style|strong)[^>]*>|<\/(style|strong)>", "", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, @"\<a[^>]*>|<\/a>", "", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, @"\<(meta|iframe|frame|span|tbody|layer)[^>]*>|<\/(iframe|frame|meta|span|tbody|layer)>", "", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, @"\<a[^>]*", "", RegexOptions.IgnoreCase);
            return str;
        }

        public static string CleanHtml(string str)
        {
            Regex reg = new Regex("<[^>]*>");
            str = reg.Replace(str, "");
            return str;
        }

        public static string ReplaceSpecialChars(string input)
        {
            input = input.Replace(" ", "_x0020_")   // space 	-> 	_x0020_
                .Replace("%", "_x0025_")            // %		-> 	_x0025_
                .Replace("#", "_x0023_")            // #		->	_x0023_
                .Replace("&", "_x0026_")            // &		->	_x0026_
                .Replace("/", "_x002F_");           // /		->	_x002F_
            return input;
        }

        public static string EncodeSpecialChars(string str)
        {
            return str.Replace("%", "_x0025_").Replace("#", "_x0023_").Replace("&", "_x0026_");
        }

        public static string DecodeSpecialChars(string str)
        {
            return str.Replace("_x0025_", "%").Replace("_x0023_", "#").Replace("_x0026_", "&");
        }

        public static bool IsEnglish(string input)
        {
            return Regex.IsMatch(input, @"[a-zA-Z]");
        }

        public static bool IsAllEnglish(string input)
        {
            if (string.IsNullOrEmpty(input)) return false;
            input = input.Replace(" ", "");
            bool isEnglish = true;
            for (int i = 0; i < input.Trim().Length; i++)
            {
                if (!IsEnglish(input.Trim().Substring(i, 1)))
                    isEnglish = false;
            }
            return isEnglish;
        }

        public static bool IsExistChineseChar(string input)
        {
            if (string.IsNullOrEmpty(input)) return false;
            bool isExist = false;
            int charLen = input.Length;
            int byteLeng = Encoding.UTF8.GetBytes(input).Length;
            if (charLen < byteLeng)
            {
                isExist = true;
            }
            return isExist;
        }        

        public static object Evaluate(string sExpression)
        {
            string xsltExpression = string.Format("number({0})",
                    new Regex(@"([\+\-\*])").Replace(sExpression, " ${1} ")
                                            .Replace("/", " div ")
                                            .Replace("%", " mod "));
            return new XPathDocument(new StringReader("<r/>")).CreateNavigator().Evaluate(xsltExpression);
        }

        public static string JoinString(string seperator, params string[] args)
        {
            return string.Join(seperator, args);
        }        

        public static string Trim(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                return str.Trim();
            }
            else
            {
                return str;
            }
        }
    }
}
