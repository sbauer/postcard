using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Postcard
{
    public interface IEmail
    {
        List<MailAddress> To { get; set; }
        MailAddress From { get; set; }
        List<MailAddress> Cc { get; set; }
        List<MailAddress> Bcc { get; set; }
        string Subject { get; set; }
        string Body { get; set; }
        bool IsHtml { get; set; }
        string ViewName { get; }
        List<Attachment> Attachments { get; set; }
        Task RenderUsing(IEmailViewRenderer renderer);
        Task SendUsing(IEmailSender emailSender);
        Task SendUsing(IEmailSender emailSender, CancellationToken cancellationToken);
    }
}