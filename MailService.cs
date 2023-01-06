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
    }

    }
