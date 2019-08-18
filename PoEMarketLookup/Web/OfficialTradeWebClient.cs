using Newtonsoft.Json.Linq;
using PoEMarketLookup.ViewModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PoEMarketLookup.Web
{
    public class OfficialTradeWebClient : IWebClient
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private const string URL_TRADE = @"https://www.pathofexile.com/api/trade/search/";
        private const string URL_STATS = @"https://www.pathofexile.com/api/trade/data/stats";
        private const string URL_LEAGUES = @"https://www.pathofexile.com/api/trade/data/leagues";

        public async Task<string> SearchAsync(string league, ItemViewModel vm, double lowerBound, double upperBound)
        {
            var converter = new PoEJsonConverter(vm, lowerBound, upperBound);
            string endpoint = URL_TRADE + league;
            var payload = new StringContent(converter.SerializeSearchParameters(), 
                Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(endpoint, payload);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return await response.Content.ReadAsStringAsync();
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

        public async Task<IList<string>> FetchLeaguesAsync()
        {
            var response = await _httpClient.GetAsync(URL_LEAGUES);

            if (response.IsSuccessStatusCode)
            {
                string leagues = await response.Content.ReadAsStringAsync();

                return ParseLeaguesJson(leagues);
            }

            return null;
        }

        private IList<string> ParseLeaguesJson(string rawLeagues)
        {
            var leagues = new List<string>();
            var json = JToken.Parse(rawLeagues);

            foreach(var league in json["result"])
            {
                leagues.Add(league["text"].ToString());
            }

            return leagues;
        }
    }
}
