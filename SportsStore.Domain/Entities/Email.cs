using SportsStore.Domain.Interfaces;
using System.Threading.Tasks;

namespace SportsStore.Domain.Entities
{
    public class Email
    {
        public string Body { get; }
        public string Subject { get; }
        public string Receiver { get; }

        public Email(string body, string subject, string receiver)
        {
            Body = body;
            Subject = subject;
            Receiver = receiver;
        }

        public async Task Send(IEmailSender emailSender)
        {
            await emailSender.Send(this).ConfigureAwait(false);
        }
    }
}