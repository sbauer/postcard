using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Postcard;
using Postcard.Renderers.Razor;
using Postcard.Senders.Smtp;

namespace Samples.ConsoleCore
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var provider = CreateContainer(serviceCollection);

            var emailService = provider.GetService<IMailer>();
            
            emailService.SendAsync(new AccountNoticeEmail("Test Person")).GetAwaiter().GetResult();
        }

        private static ServiceProvider CreateContainer(ServiceCollection serviceCollection)
        {
            var provider = serviceCollection.BuildServiceProvider();
            return provider;
        }

        private static void ConfigureServices(ServiceCollection serviceCollection)
        {
            serviceCollection.AddPostcard(options =>
            {
                options.UseSmtpSender();
                options.UseRazorRenderer();
            });
        }
    }

    public class AccountNoticeEmail : Email
    {
        public string Name { get; }

        public AccountNoticeEmail(string name) : base("AccountNotice")
        {
            Name = name;
            To.Add(new MailAddress("shane@shanebauer.com"));
            From = new MailAddress("shane@shanebauer.com");
            Subject = "Account Notification";
        }
    }
}