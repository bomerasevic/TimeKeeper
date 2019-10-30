using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using TimeKeeper.Mail;

namespace TimeKeeper.API.Services
{
    public static class MailService
    {
        public static void Send(string mailTo, string subject, string body)
        {
            SmtpClient client = new SmtpClient()
            {
                Port = 587,
                Host = "smtp.gmail.com",
                EnableSsl = true,
                Timeout = 10000,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(Startup.Configuration["mailFrom:mail"], Startup.Configuration["mailFrom:password"])
            };
            MailMessage message = new MailMessage(Startup.Configuration["mailFrom:mail"], mailTo, subject, body);
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            message.ReplyToList.Add(mailTo);

            client.Send(message);
        }
    }
}
