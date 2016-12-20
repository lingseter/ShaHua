using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web;
using System.Management;

namespace Utility
{
    public class WebHelper
    {
        public static string GetUserHostAddress()
        {
            return GetUserHostAddress(HttpContext.Current.Request);
        }

        public static string GetUserHostAddress(HttpRequest request)
        {
            if (request != null)
            {
                return request.UserHostAddress;
            }
            return "";
        }

        public static string GetRequestInfo()
        {
            return GetRequestInfo(HttpContext.Current.Request);
        }

        public static string GetRequestInfo(HttpRequest request)
        {
            if (request == null) return string.Empty;
            StringBuilder sbRet = new StringBuilder();
            sbRet.AppendLine("[Specific Information]");
            sbRet.Append("FORM DATA:").AppendLine(request.Form.ToString());
            sbRet.AppendLine("[Server Variables]");
            if (HttpContext.Current != null && HttpContext.Current.Server != null)
            {
                sbRet.Append("Machine Name: ").AppendLine(HttpContext.Current.Server.MachineName);
            }
            sbRet.Append("Request Stream: ").AppendLine(GetRequestInputString(request));
            if (request.ServerVariables != null && request.ServerVariables.Count > 0)
            {
                for (int i = 0; i < request.ServerVariables.Count; i++)
                {
                    string sv = request.ServerVariables[i];
                    if (string.IsNullOrEmpty(sv)) continue;
                    sbRet.Append(request.ServerVariables.Keys[i]).Append(": ").AppendLine(sv);
                }
            }
            return sbRet.ToString();
        }

        public static string GetRequestUrl()
        {
            if (HttpContext.Current != null && HttpContext.Current.Request != null && HttpContext.Current.Request.Url != null)
            {
                return HttpContext.Current.Request.Url.AbsoluteUri;
            }
            return string.Empty;
        }

        public static string GetCurrentUrl()
        {
            string abPath = HttpContext.Current.Request.Url.AbsolutePath;
            abPath = abPath.Substring(1, abPath.Length - 1);

            string siteName = string.Empty;
            if (abPath.Split('/').Length > 1)
            {
                siteName = abPath.Substring(0, abPath.IndexOf("/"));
                if (!string.IsNullOrEmpty(siteName))
                    siteName = "/" + siteName;
            }
            return string.Format("http://{0}:{1}{2}/", HttpContext.Current.Request.Url.Host, HttpContext.Current.Request.Url.Port.ToString(), siteName);
        }

        public static string GetRequestInputString(HttpRequest request)
        {
            request.InputStream.Position = 0;
            System.IO.Stream stream = request.InputStream;
            System.IO.StreamReader sr = new System.IO.StreamReader(request.InputStream);
            StringBuilder sbRequest = new StringBuilder();
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                sbRequest.Append(line);
            }
            request.InputStream.Position = 0;
            return sbRequest.ToString();
        }
        
        public static string GetHostName()
        {
            return Dns.GetHostName();
        }
        
        public static List<string> GetIpList()
        {
            List<string> ipList = new List<string>();
            IPAddress[] addressList = Dns.GetHostEntry(GetHostName()).AddressList;
            for (int i = 0; i < addressList.Length; i++)
            {
                ipList.Add(addressList[i].ToString());
            }
            return ipList;
        }
       
        public static List<string> GetMacList()
        {
            List<string> macList = new List<string>();
            ManagementClass mc;
            mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if (mo["IPEnabled"].ToString() == "True")
                    macList.Add(mo["MacAddress"].ToString());
            }
            return macList;
        }
    }
}
