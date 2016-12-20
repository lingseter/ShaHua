using System;
using System.Collections.Generic;

namespace Utility
{
    public class Enums
    {
        #region property
        public enum Language
        {
            en_us,
            zh_cn,
            zh_tw
        }

        public enum Themes
        {
            day,
            Night,
            spring,
            summer,
            autumn,
            winter,
        }

        public enum Handle
        {
            Create,
            Delete,
            Update,
            Search,
            Read,
            Write,
        }

        public enum Gender
        {
            Undefined,
            Male,
            Female,
        }

        public enum Role
        {
            Admin,
            User
        }

        public enum CollectType
        {
            Photo,
            Video,
            Artist
        }

        public enum PhotoType
        {
            JPG,
            PNG,
            GIF,
            BMP,
            SVG
        }

        public enum VideoType
        {
            MP4,
            FLV,
            AVI,
        }
        #endregion

        #region function
        public static string GetLang2String(Language lang)
        {
            string name = string.Empty;
            switch (lang)
            {
                case Language.zh_cn:
                    name = "zh-cn";
                    break;
                case Language.zh_tw:
                    name = "zh-tw";
                    break;
                default:
                    name = "en-us";
                    break;
            }
            return name;
        }

        public static Language GetLang2Enum(string lang)
        {
            if (lang == null)
                return Language.zh_cn;
            else if (lang.ToLower() == "zh-tw")
                return Language.zh_tw;
            else if (lang.ToLower() == "en-us")
                return Language.en_us;
            else
                return Language.zh_cn;
        }

        public static Dictionary<int, string> Enum2Dictionary(Type enumType)
        {
            Dictionary<int, string> d = new Dictionary<int, string>();
            string[] values = Enum.GetNames(enumType);
            for (var i = 0; i < values.Length; i++)
            {
                d.Add(i, values[i]);
            }
            return d;
        }
        #endregion
    }
}
