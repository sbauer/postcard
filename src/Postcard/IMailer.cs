using System.Threading;
using System.Threading.Tasks;

namespace Postcard
{
    public interface IMailer
    {
        Task SendAsync(IEmail email);
        Task SendAsync(IEmail email, CancellationToken cancellationToken);
    }

    public class PostcardMailer: IMailer
    {
        private readonly IEmailSender _emailSender;
        private readonly IEmailViewRenderer _emailViewRenderer;

        public PostcardMailer(IEmailSender emailSender, IEmailViewRenderer emailViewRenderer)
        {
            _emailSender = emailSender;
            _emailViewRenderer = emailViewRenderer;
        }

        public Task SendAsync(IEmail email)
        {
            return SendAsync(email, CancellationToken.None);
        }

        public async Task SendAsync(IEmail email, CancellationToken cancellationToken)
        {
            await email.RenderUsing(_emailViewRenderer).ConfigureAwait(false);
            await email.SendUsing(_emailSender, cancellationToken).ConfigureAwait(false);
        }
    }
}