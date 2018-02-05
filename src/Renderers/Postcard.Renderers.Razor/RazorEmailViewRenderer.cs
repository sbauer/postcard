using System;
using System.IO;
using System.Threading.Tasks;
using RazorLight;

namespace Postcard.Renderers.Razor
{
    public class RazorEmailViewRenderer: IEmailViewRenderer
    {
        private readonly IRazorLightEngine _razorLightEngine;

        public RazorEmailViewRenderer(string pathToViews)
        {
            if (String.IsNullOrEmpty(pathToViews))
                throw new ArgumentException($"{nameof(pathToViews)} should not be null or empty.");
            
            _razorLightEngine = new RazorLightEngineBuilder().UseFilesystemProject(pathToViews).UseMemoryCachingProvider().Build();
        }
        
        public async Task RenderAsync(IEmail email)
        {
            var emailBody = await _razorLightEngine.CompileRenderAsync(email.ViewName, email)
                .ConfigureAwait(false);

            email.Body = emailBody;
            email.IsHtml = true;
        }
    }
}