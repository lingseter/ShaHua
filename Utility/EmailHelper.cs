using System;
using System.Text;
using System.Net.Mail;

namespace Utility
{
    public class EmailHelper
    {
        public static bool SendEmails(string smtpHost, string userName, string passWord, string from, string[] to, string[] cc, string[] bcc, string subject, string body)
        {
            try
            {
                SmtpClient sc = new SmtpClient(string.IsNullOrEmpty(smtpHost) ? "smtp.qq.com" : smtpHost);
                sc.DeliveryMethod = SmtpDeliveryMethod.Network;
                sc.UseDefaultCredentials = false;
                sc.Credentials = new System.Net.NetworkCredential(userName, passWord);
                sc.EnableSsl = true;
                MailMessage mm = new MailMessage();
                mm.From = new MailAddress(from);
                if (to != null)
                {
                    foreach (string t in to)
                    {
                        if (!string.IsNullOrEmpty(t))
                        {
                            mm.To.Add(t);
                        }
                    }
                }
                if (cc != null)
                {
                    foreach (string c in cc)
                    {
                        if (!string.IsNullOrEmpty(c))
                        {
                            mm.To.Add(c);
                        }
                    }
                }
                if (bcc != null)
                {
                    foreach (string bc in bcc)
                    {
                        if (!string.IsNullOrEmpty(bc))
                        {
                            mm.To.Add(bc);
                        }
                    }
                }
                mm.Subject = subject;
                mm.Body = body;
                mm.BodyEncoding = Encoding.UTF8;
                mm.Priority = MailPriority.High;

                sc.Send(mm);
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.LogException("EmailException", ex);
                return false;
            }
        }

        public static bool SendEmail(string userName, string passWord, string from, string to, string subject, string body)
        {
            return SendEmails(null, userName, passWord, from, new string[] { to }, null, null, subject, body);
        }

        public static bool isValidEmail(string email)
        {
            bool isEmail = false;
            string myRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,50}\.[0-9]{1,50}\.[0-9]{1,50}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{1,50}|[0-9]{1,50})(\]?)$";
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(myRegex);
            if (reg.IsMatch(email))
            {
                isEmail = true;
            }
            return isEmail;
        }
    }
}
