using System.Net;

namespace Postcard.Senders.Smtp
{
    public class SmtpSenderBuilder : ISmtpSenderBuilder
    {
        private readonly PostcardOptions _options;

        public string Host { get; private set; } = "localhost";
        public int Port { get; private set; } = 25;
        public NetworkCredential Credentials { get; private set; }
        public bool RequireSsl { get; private set; }

        public SmtpSenderBuilder(PostcardOptions options)
        {
            _options = options;
        }

        public ISmtpSenderBuilder UsingHostAndPort(string host, int port = 25)
        {
            Host = host;
            Port = port;
            
            CreateEmailSender();

            return this;
        }

        public ISmtpSenderBuilder RequiresSsl()
        {
            RequireSsl = true;
            
            CreateEmailSender();
            
            return this;
        }

        public ISmtpSenderBuilder WithCredentials(string username, string password)
        {
            Credentials = new NetworkCredential(username, password);
            
            CreateEmailSender();

            return this;
        }
        
        private void CreateEmailSender()
        {
            _options.EmailSender = new SmtpEmailSender(CreateConfiguration());
        }

        private SmtpConfiguration CreateConfiguration()
        {
            return new SmtpConfiguration() {Host = Host, Port = Port, Credentials = Credentials, RequiresSsl = RequireSsl};
        }
    }
}