using System;
using System.IO;

namespace Postcard.Renderers.Razor
{
    public static class PostcardOptionsExtensions
    {
        public static void UseRazorRenderer(this PostcardOptions options, string path = null)
        {
            if (path == null)
                path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Views{Path.DirectorySeparatorChar}Email");
            
            options.ViewRenderer = new RazorEmailViewRenderer(path);
        }
    }
}