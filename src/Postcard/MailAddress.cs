namespace Postcard
{
    public class MailAddress
    {
        public string Name { get; }
        public string Email { get; }

        public MailAddress(string email) : this(email, string.Empty)
        {
        }

        public MailAddress(string email, string name)
        {
            Email = email;
            Name = name;
        }
    }
}