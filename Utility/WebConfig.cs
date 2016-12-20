namespace Utility
{
    public class WebConfig
    {
        public static string ConnectionString = ConfigHelper.GetConnctionString("ShaHuaConnection");
        public static string ValidateImgPath = ConfigHelper.GetAppSetting("ValidateImgPath ");
        public static string LogPath = ConfigHelper.GetAppSetting("LogPath");
        public static bool IsDebug = ConfigHelper.GetSettingToBool("IsDebug");
    }
}
