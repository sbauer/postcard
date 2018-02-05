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
           
            serviceCollection.AddTransient(p =>
            {
                var configuration = p.GetService<IOptions<PostcardOptions>>();

                return configuration.Value.EmailSender;
            });
            
            serviceCollection.AddTransient(p =>
            {
                var configuration = p.GetService<IOptions<PostcardOptions>>();

                return configuration.Value.ViewRenderer;
            });
            
            serviceCollection.AddSingleton<IMailer, PostcardMailer>();
        }
    }
}