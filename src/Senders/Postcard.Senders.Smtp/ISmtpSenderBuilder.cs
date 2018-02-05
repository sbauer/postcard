namespace Postcard.Senders.Smtp
{
    public interface ISmtpSenderBuilder
    {
        ISmtpSenderBuilder UsingHostAndPort(string host, int port = 25);
        ISmtpSenderBuilder RequiresSsl();
        ISmtpSenderBuilder WithCredentials(string username, string password);
    }
}