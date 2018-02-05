using System.Threading.Tasks;

namespace Postcard
{
    public interface IEmailViewRenderer
    {
        Task RenderAsync(IEmail email);
    }
}