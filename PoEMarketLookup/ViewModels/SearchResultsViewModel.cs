using Newtonsoft.Json.Linq;
using System;
namespace PoEMarketLookup.ViewModels
{
    public class SearchResultsViewModel
    {
        public string Id { get; set; }
        public int Total { get; set; }

        public static SearchResultsViewModel CreateViewModel(string result)
        {
            var json = JToken.Parse(result);

            return new SearchResultsViewModel()
            {
                Id = json["id"].ToString(),
                Total = (int)json["total"]
            };
        }
    }
}
