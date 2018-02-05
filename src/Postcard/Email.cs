using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Postcard
{
    public class Email : IEmail
    {
        public Email(string viewName)
        {
            ViewName = viewName;
            Attachments = new List<Attachment>();
            To = new List<MailAddress>();
            Cc = new List<MailAddress>();
            Bcc = new List<MailAddress>();
        }
        public List<MailAddress> To { get; set; }
        public MailAddress From { get; set; }
        public List<MailAddress> Cc { get; set; }
        public List<MailAddress> Bcc { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsHtml { get; set; }
        public string ViewName { get; protected set; }
        public List<Attachment> Attachments { get; set; }

        public virtual Task RenderUsing(IEmailViewRenderer renderer)
        {
            return renderer.RenderAsync(this);
        }

        public Task SendUsing(IEmailSender emailSender)
        {
            return SendUsing(emailSender, CancellationToken.None);
        }

        public Task SendUsing(IEmailSender emailSender, CancellationToken cancellationToken)
        {
            return emailSender.SendAsync(this, cancellationToken);
        }
    }
}