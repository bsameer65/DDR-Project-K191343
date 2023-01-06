using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ACM_System
{
    public interface IMailService
    {
         void sendmail();
    }
    public class MailService : IMailService
    {
        string loggedUserEmail, loggedUserPassword;
        string textTo, subject, msg;

        public MailService(string logemail, string logpass, string text, string subj, string message)
        {
            loggedUserEmail = logemail;
            loggedUserPassword = logpass;
            textTo = text;
            subject = subj;
            msg = message;

        }
        public void sendmail()
        {
            MailMessage mailMessage = new MailMessage(this.loggedUserEmail, this.textTo, this.subject, this.msg);
            mailMessage.IsBodyHtml = true;
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.UseDefaultCredentials = false;
            NetworkCredential ClientCredential = new NetworkCredential(this.loggedUserEmail, this.loggedUserPassword);
            client.Credentials = ClientCredential;
            client.EnableSsl = true;
            client.Send(mailMessage);

        }
    }
    public class MailServiceVirtualProxy : IMailService
    {
        string loggedUserEmail, loggedUserPassword;
        string textTo, subject, msg;
        private MailService ms;
        public MailServiceVirtualProxy(string logemail, string logpass, string text, string subj, string message)
        {
            loggedUserEmail = logemail;
            loggedUserPassword = logpass;
            textTo = text;
            subject = subj;
            msg = message;
        }
        public void sendmail()
        {
            if (ms == null)
            {
                ms = createMailService();
            }
            ms.sendmail();

        }
        private MailService createMailService()
        {
            return new MailService(loggedUserEmail, loggedUserPassword, textTo, subject, msg);
        }
    }

}
