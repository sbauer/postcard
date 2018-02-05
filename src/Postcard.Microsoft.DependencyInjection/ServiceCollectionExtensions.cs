using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace Postcard
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPostcard(this IServiceCollection serviceCollection, Action<PostcardOptions> configureOptions)
        {
            if (configureOptions == null)
                throw new ArgumentNullException(nameof(configureOptions));

            serviceCollection.AddOptions();
            
            serviceCollection.Configure(configureOptions);
           
            serviceCollection.AddTransient(GetEmailSender);
            
            serviceCollection.AddTransient(GetViewRenderer);
            
            serviceCollection.AddSingleton<IMailer, PostcardMailer>();
        }

        private static IEmailSender GetEmailSender(IServiceProvider provider)
        {
            var configuration = GetOptions(provider);

            return configuration.Value.EmailSender;
        }
        
        private static IEmailViewRenderer GetViewRenderer(IServiceProvider provider)
        {
            var configuration = GetOptions(provider);

            return configuration.Value.ViewRenderer;
        }

        private static IOptions<PostcardOptions> GetOptions(IServiceProvider serviceProvider)
        {
            return serviceProvider.GetService<IOptions<PostcardOptions>>();
        }
    }
}