using System;
using System.IO;
using System.Threading.Tasks;
using Postcard.Renderers.Razor;
using Xunit;

namespace Postcard.Tests.Renderers.Razor
{
    public class RenderTests
    {
        [Fact]
        public void Null_Or_Empty_Path_Should_Throw_Exception()
        {
            Assert.Throws<ArgumentException>(() => new RazorEmailViewRenderer(null) );
        }

        [Fact]
        public async Task RenderAsync_Should_Render_Valid_View()
        {
            var renderer = CreateRenderer();

            var email = new WelcomeEmail("Person");
            await renderer.RenderAsync(email);
            
            Assert.Equal("<strong>Welcome, Person</strong>", email.Body);
        }

        [Fact] public async Task RenderAsync_Should_Set_IsHtml_To_True()
        {
            var renderer = CreateRenderer();

            var email = new WelcomeEmail("Person");
            await renderer.RenderAsync(email);
            
            Assert.True(email.IsHtml);
        }

        private RazorEmailViewRenderer CreateRenderer()
        {
            return new RazorEmailViewRenderer(Path.Combine(Directory.GetCurrentDirectory(), "Views/Emails"));
        }
    }

    public class WelcomeEmail : Email
    {
        public string FirstName { get; }

        public WelcomeEmail(string firstName) : base("Welcome")
        {
            FirstName = firstName;
        }
    }
}