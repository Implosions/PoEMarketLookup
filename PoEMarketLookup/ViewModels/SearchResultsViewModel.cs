using Newtonsoft.Json.Linq;
using System;
namespace PoEMarketLookup.ViewModels
{
    public class SearchResultsViewModel
    {
        private const string BASE_SEARCH_URL = @"http://www.pathofexile.com/trade/search/";

        public string Id { get; set; }
        public int Total { get; set; }
        public string League { get; set; }
        public string SearchURL
        {
            get
            {
                return BASE_SEARCH_URL + League + '/' + Id;
            }
        }

        public static SearchResultsViewModel CreateViewModel(string result, string league)
        {
            var json = JToken.Parse(result);

            return new SearchResultsViewModel()
            {
                Id = json["id"].ToString(),
                Total = (int)json["total"],
                League = league
            };
        }
    }
}
