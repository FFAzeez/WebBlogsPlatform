using System.Net.Http;
using System.Threading.Tasks;

namespace WebBloggingPlatform.Utility
{
    public interface ISendHttpRequest
    {
        Task<T> SendAsync<T>();
    }
}