using System.Threading.Tasks;

namespace CommonLib
{
    public interface IHttpClient
    {     
        Task<string> GetResultsAsync();
    }
}
