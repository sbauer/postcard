using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Net;
using System.Threading;

namespace Postcard.Senders.Smtp
{
    public class SmtpEmailSender: IEmailSender
    {
        private readonly SmtpConfiguration _smtpConfiguration;
        
        public SmtpEmailSender(string host): this(new SmtpConfiguration() { Host = host})
        {
            
        }

        public SmtpEmailSender(SmtpConfiguration smtpConfiguration)
        {
            _smtpConfiguration = smtpConfiguration;
        }
        
        public async Task SendAsync(Email email, CancellationToken? cancellationToken = null)
        {
            var message = CreateMailMessage(email);

            if (cancellationToken?.IsCancellationRequested ?? false)
                return;
                
            using (var client = CreateClient())
            {
                await client.SendMailAsync(message).ConfigureAwait(false);
            }
        }

        private SmtpClient CreateClient()
        {
            return new SmtpClient(_smtpConfiguration.Host, _smtpConfiguration.Port) { EnableSsl = _smtpConfiguration.RequiresSsl, Credentials = _smtpConfiguration.Credentials};
        }

        private MailMessage CreateMailMessage(IEmail email)
        {
            var message = new MailMessage();
            message.Body = email.Body;
            message.Subject = email.Subject;
            message.From = new System.Net.Mail.MailAddress(email.From.Email, email.From.Name);
            message.IsBodyHtml = email.IsHtml;

            foreach (var to in email.To)
            {
                message.To.Add(new System.Net.Mail.MailAddress(to.Email, to.Name));
            }
            
            foreach(var attachment in email.Attachments)
                message.Attachments.Add(new System.Net.Mail.Attachment(attachment.Data, attachment.FileName));

            return message;
        }
    }

    public class SmtpConfiguration
    {
        public bool RequiresSsl { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public NetworkCredential Credentials { get; set; }
    }
}