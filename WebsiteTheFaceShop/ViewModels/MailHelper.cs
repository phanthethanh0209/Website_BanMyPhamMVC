using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace WebsiteTheFaceShop.ViewModels
{
    public class MailHelper
    {
        public void SendEmail(string toEmailAddress, string subject, string content)
        {
            var FromEmailAddress = ConfigurationManager.AppSettings["FromEmailAddress"].ToString();
            var FromEmailDisplayName = ConfigurationManager.AppSettings["FromEmailDisplayName"].ToString();
            var FromEmailPassword = ConfigurationManager.AppSettings["FromEmailPassword"].ToString();
            var smtpHost = ConfigurationManager.AppSettings["SMTPHost"].ToString();
            var smtpPort = ConfigurationManager.AppSettings["SMTPPort"].ToString();

            bool enabledSs1 = bool.Parse(ConfigurationManager.AppSettings["EnabledSSL"].ToString());

            string body = content;
            // Tạo message với kiểu MailMessage để lưu 2 tham số về mail gửi và mail nhận
            MailMessage message = new MailMessage(new MailAddress(FromEmailAddress, FromEmailDisplayName), new MailAddress(toEmailAddress));
            message.Subject = subject; // gán tiêu đề 
            message.IsBodyHtml = true; // content có cho phép Html hay kh
            message.Body = body; // gán nôi dung

            var client = new SmtpClient();
            client.Credentials = new NetworkCredential(FromEmailAddress, FromEmailPassword);
            client.Host = smtpHost;
            client.EnableSsl = enabledSs1;
            client.Port = !string.IsNullOrEmpty(smtpPort) ? Convert.ToInt32(smtpPort) : 0;
            client.Send(message);
        }
    }
}