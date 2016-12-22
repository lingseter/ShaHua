using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Utility
{
    public class HttpMocker
    {
        //解决这个问题：基础连接已经关闭: 未能为 SSL/TLS 安全通道建立信任关系
        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {   // 总是接受    
            return true;
        }
        public static HttpWebResponse HttpMock(string url, CookieCollection curCookie, string method, string postData = "")
        {
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);//验证服务器证书回调自动验证  
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.CookieContainer = new CookieContainer();
            request.CookieContainer.Add(curCookie);
            request.Method = method.ToUpper();
            request.KeepAlive = true;
            request.AllowAutoRedirect = false;
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64; rv:45.0) Gecko/20100101 Firefox/45.0";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            request.ContentType = "application/x-www-form-urlencoded";
            if (method.ToUpper() == "POST")
            {
                byte[] postBytes = Encoding.UTF8.GetBytes(postData.ToString());
                request.ContentLength = postBytes.Length;
                Stream postDataStream = request.GetRequestStream();
                postDataStream.Write(postBytes, 0, postBytes.Length);
                postDataStream.Close();
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            return response;
        }    
    }
}
