using System.Net.Mail;

namespace Postcard.Senders.Smtp
{
    public static class PostcardOptionsExtensions
    {
        public static ISmtpSenderBuilder UseSmtpSender(this PostcardOptions options, string host = "localhost")
        {
            var builder = new SmtpSenderBuilder(options);
            return builder.UsingHostAndPort(host);
        }

        public static void UseSmtpSender(this PostcardOptions options, SmtpConfiguration configuration)
        {
            options.EmailSender = new SmtpEmailSender(configuration);
        }
    }
}