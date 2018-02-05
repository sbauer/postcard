using System.Threading;
using System.Threading.Tasks;

namespace Postcard
{
    public interface IEmailSender
    {
        Task SendAsync(Email email, CancellationToken? cancellationToken);
    }
}