using PoEMarketLookup.ViewModels;
using System.Threading.Tasks;

namespace PoEMarketLookup.Web
{
    public interface IWebClient
    {
        Task<string> SearchAsync(string league, ItemViewModel vm, double lowerBound, double upperBound, ListTime timeWindow);
        Task<string> FetchListingsAsync(string[] hashes);
    }
}
