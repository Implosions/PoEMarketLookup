using PoEMarketLookup.ViewModels;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PoEMarketLookup.Web
{
    public class OfficialTradeWebClient
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private const string URL_TRADE = @"https://www.pathofexile.com/api/trade/search/";
        private const string URL_STATS = @"https://www.pathofexile.com/api/trade/data/stats";

        public async Task<SearchResultsViewModel> SearchAsync(string league, ItemViewModel vm)
        {
            var converter = new PoEJsonConverter(vm);
            string endpoint = URL_TRADE + league;
            var payload = new StringContent(converter.SerializeSearchParameters(), 
                Encoding.UTF8, "application/json");

            var response = _httpClient.PostAsync(endpoint, payload).Result;

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();

                return SearchResultsViewModel.CreateViewModel(result, league);
            }

            return null;
        }

        public async Task<string> FetchStatsAsync()
        {
            var response = await _httpClient.GetAsync(URL_STATS);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return null;
        }
    }
}
