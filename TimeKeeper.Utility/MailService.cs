﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace TimeKeeper.Utility
{
    public static class MailService
    {
        public static LoggerService Log = new LoggerService();
        public static void Send(string mailTo, string subject, string body)
        {
            try
            {
                string mail = "ntg.infodesk@gmail.com";
                string password = "Company19892016";
                SmtpClient client = new SmtpClient()
                {
                    Port = 587,
                    Host = "smtp.gmail.com",
                    EnableSsl = true,
                    Timeout = 10000,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(mail, password)
                };
                MailMessage message = new MailMessage(mail, "teambravo321@gmail.com", subject, body);
                message.BodyEncoding = Encoding.UTF8;
                message.IsBodyHtml = true;
                message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                message.ReplyToList.Add(mailTo);

                client.Send(message);
            }
            catch(Exception)
            {
                Log.Fatal("Server error");
                return;
            }
        }
    }
}
