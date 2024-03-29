﻿using PoEMarketLookup.ViewModels.Commands;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PoEMarketLookup.ViewModels
{
    public class SearchResultsViewModel
    {
        private const string BASE_SEARCH_URL = @"http://www.pathofexile.com/trade/search/";

        public ICommand OpenSearchURL { get; } 

        public string Id { get; set; }
        public int Total { get; set; }
        public string League { get; set; }
        public string MinimumListingPrice { get; set; }
        public string MaximumListingPrice { get; set; }
        public string MedianListingPrice { get; set; }

        public string SearchURL
        {
            get
            {
                return BASE_SEARCH_URL + League + '/' + Id;
            }
        }

        public SearchResultsViewModel()
        {
            OpenSearchURL = new AsyncCommand(OpenSearchURLInBrowser);
        }

        private async Task OpenSearchURLInBrowser()
        {
            await Task.Run(() => Process.Start(SearchURL));
        }
    }
}
