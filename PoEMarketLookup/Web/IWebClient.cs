using PoEMarketLookup.ViewModels;
using System.Threading.Tasks;

namespace PoEMarketLookup.Web
{
    public interface IWebClient
    {
        Task<string> SearchAsync(string league, ItemViewModel vm, double lowerBound, double upperBound);
        Task<string> FetchListingsAsync(string[] hashes);
    }
}
