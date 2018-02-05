namespace Postcard
{
    public class PostcardOptions
    {
        public IEmailViewRenderer ViewRenderer { get; set; }
        public IEmailSender EmailSender { get; set; }
    }
}