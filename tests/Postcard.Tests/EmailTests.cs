using System.Threading.Tasks;
using Xunit;

namespace Postcard.Tests
{
    public class EmailTests
    {
        [Fact]
        public void To_Should_Not_Be_Null()
        {
            var email = new Email("test");
            Assert.NotNull(email.To);
        }
        
        [Fact]
        public void ViewName_Should_Not_Be_Null_Or_Emtpy()
        {
            var email = new Email("test");
            Assert.NotEmpty(email.ViewName);
            Assert.NotNull(email.ViewName);
        }

        [Fact]
        public async Task EmailUsing_Should_Populate_Body_Based_On_Renderer()
        {
            var email = new Email("test");
            await email.RenderUsing(new FakeEmailViewRenderer());
            
            Assert.Equal("testing", email.Body);
        }
    }

    public class FakeEmailViewRenderer : IEmailViewRenderer
    {
        public Task RenderAsync(IEmail email)
        {
            email.Body = "testing";
            
            return Task.CompletedTask;
        }
    }
}