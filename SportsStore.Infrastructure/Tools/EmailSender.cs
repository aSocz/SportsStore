using SportsStore.Domain.Entities;
using SportsStore.Domain.Interfaces;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SportsStore.Infrastructure.Tools
{
    public class EmailSender : IEmailSender
    {
        private readonly SmtpSection section;

        public EmailSender()
        {
            section = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
        }

        public async Task Send(Email email)
        {
            using (var message = new MailMessage(section.From, email.Receiver, email.Subject, email.Body))
            using (var client = new SmtpClient(section.Network.Host, section.Network.Port))
            {
                client.Credentials = new NetworkCredential(section.Network.UserName, section.Network.Password);
                client.EnableSsl = section.Network.EnableSsl;
                try
                {
                    await client.SendMailAsync(message);
                }
                catch (Exception e)
                {
                    Debug.WriteLine("An exception occured during email sending" + e.StackTrace);
                }
            }
        }
    }
}