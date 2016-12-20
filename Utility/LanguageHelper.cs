using System.Globalization;
using System.Threading;

namespace Utility
{
    public class LanguageHelper
    {
        public static void SetCurrentLang(Enums.Language lang)
        {
            CultureInfo ci = new CultureInfo(Enums.GetLang2String(lang));
            if (ci != null)
                Thread.CurrentThread.CurrentCulture = ci;
        }

        public static Enums.Language GetCultureType()
        {
            string lang = string.Empty;
            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            if(ci!=null)
                lang = ci.ToString().ToLower();
            return Enums.GetLang2Enum(lang);
        }

        public static string GetCulture()
        {
            string lang = string.Empty;
            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            if (ci != null)
                lang = ci.ToString().ToLower();
            else
                lang = "zh-cn";
            return lang;
        }
    }
}
