using Postcard;

namespace Samples.AspNetCore.Models
{
    public class WelcomeEmail: Email
    {
        public WelcomeEmail() : base("Welcome")
        {
            From = new MailAddress("shane@shanebauer.com");
            To.Add(new MailAddress("shane@shanebauer.com"));
            Subject = "Test Email";
            
        }
    }
}