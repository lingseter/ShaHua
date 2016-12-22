using System;
using System.IO;

namespace Utility
{
    public class LogHelper : IDisposable
    {
        #region Constants
        const string SPLIT_CHARS = " ";
        private static log4net.ILog logScope = log4net.LogManager.GetLogger("Scope");
        private static log4net.ILog logInfo = log4net.LogManager.GetLogger("Info");
        private static log4net.ILog logException = log4net.LogManager.GetLogger("Exception");
        #endregion

        #region Fields
        private string scopeName = string.Empty;
        #endregion

        #region Constructor
        public LogHelper(string name, params object[] data)
        {
            if (WebConfig.IsDebug)
            {
                logScope.Info(string.Format("Enter {0}. params: ", name) + string.Join(SPLIT_CHARS, data));
                this.scopeName = name;
            }
        }
        #endregion

        #region Static public methods

        public static void LogException(string message, Exception ex)
        {
            if (WebConfig.IsDebug)
            {
                logException.Error(message, ex);
            }
        }

        public static void Info(params object[] data)
        {
            if (WebConfig.IsDebug)
            {
                logInfo.Info(string.Join(SPLIT_CHARS, data));
            }
        }

        #endregion

        #region IDisposable
        public void Dispose()
        {
            if (WebConfig.IsDebug)
            {
                logScope.Info(string.Format("Exit {0}.", this.scopeName));
            }
        }
        #endregion

        public static void SimpleLog(string path, string name, string body)
        {
            if (WebConfig.IsDebug)
            {
                if (string.IsNullOrEmpty(path))
                {
                    path = WebConfig.LogPath;
                }
                path = Path.Combine(path, DateTime.Now.ToString("yyyy-MM-dd"));
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                path = string.Concat(path, "\\", name, ".txt");
                using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                {
                    StreamWriter w = new StreamWriter(fs);
                    w.WriteLine(string.Concat("Time:", DateTime.Now));
                    w.WriteLine(string.Concat("Exception:", body));
                    w.WriteLine("------------------------------------");
                    w.Flush();
                }
            }
        }

        public static void SimpleLog(string name, string body)
        {
            SimpleLog(null, name, body);
        }
    }
}
