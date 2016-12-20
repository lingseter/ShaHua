using System.Collections.Generic;
using System.Configuration;

namespace Utility
{
    public class ConfigHelper
    {
        public static string GetAppSetting(string key)
        {
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings[key]))
                return ConfigurationManager.AppSettings[key];
            return string.Empty;
        }

        public static Dictionary<string, string> GetAppSettings(string[] keys)
        {
            Dictionary<string, string> appStrs = new Dictionary<string, string>();
            foreach (string key in keys)
            {
                appStrs[key] = ConfigurationManager.AppSettings[key];
            }
            return appStrs;
        }

        public static string GetConnctionString(string key)
        {
            if (ConfigurationManager.ConnectionStrings[key] != null)
                return ConfigurationManager.ConnectionStrings[key].ConnectionString;
            return string.Empty;
        }

        public static Dictionary<string, string> GetConnctionStrings(string[] keys)
        {
            Dictionary<string, string> conStrs = new Dictionary<string, string>();
            foreach (string key in keys)
            {
                conStrs[key] = ConfigurationManager.ConnectionStrings[key].ConnectionString;
            }
            return conStrs;
        }

        public static int GetSettingToInt(string key)
        {
            return NumberHelper.ToInt(GetAppSetting(key), 0);
        }

        public static bool GetSettingToBool(string key)
        {
            return GetAppSetting(key) == "1";
        }

        public static void SaveAppSetting(string key, string value)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings.Remove(key);
            config.AppSettings.Settings.Add(key, value);
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        public static void ReadConfig<T>(T t) where T : class,new()
        {
            foreach (var key in ConfigurationManager.AppSettings.AllKeys)
            {
                foreach (var property in t.GetType().GetProperties())
                {
                    if (property.Name.ToLower() == key.ToLower())
                        property.SetValue(t, GetAppSetting(key));
                }
            }
        }

    }
}
