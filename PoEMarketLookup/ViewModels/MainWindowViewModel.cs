using Newtonsoft.Json.Linq;
using PoEMarketLookup.PoE.Parsers;
using PoEMarketLookup.ViewModels.Commands;
using PoEMarketLookup.Web;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace PoEMarketLookup.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        protected IWebClient WebClient { get; set; } = new OfficialTradeWebClient();

        public AsyncCommand PasteFromClipboardCommand { get; }
        public AsyncCommand SearchCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public IList<string> Leagues { get; set; } = new List<string>() { "Standard", "Hardcore" };

        private int _selectedLeagueIndex;
        public int SelectedLeagueIndex
        {
            get => _selectedLeagueIndex;
            set
            {
                if(_selectedLeagueIndex != value)
                {
                    _selectedLeagueIndex = value;
                    SaveLeagueSelectionIndex();
                }
            }
        }

        public int SelectedListTimeIndex { get; set; } = 3;

        public bool CanSearch
        {
            get => ItemVM != null && !(ItemVM is ErrorViewModel);
        }

        private object _resultsViewModel;
        public object ResultsViewModel
        {
            get => _resultsViewModel;
            set
            {
                _resultsViewModel = value;
                OnPropertyChanged();
            }
        }

        private object _itemVM;
        public object ItemVM
        {
            get => _itemVM;
            set
            {
                _itemVM = value;
                OnPropertyChanged();
            }
        }

        public double FieldValueUpperBound { get; set; } = 110;
        public double FieldValueLowerBound { get; set; } = 90;

        public MainWindowViewModel()
        {
            PasteFromClipboardCommand = new AsyncCommand(PasteButtonClick);
            SearchCommand = new AsyncCommand(SearchButtonClick, () => CanSearch);
        }

        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async Task PasteButtonClick()
        {
            string item = GetClipboard();
            await Task.Run(() => ParseItem(item));
            SearchCommand.InvokeCanExecuteChanged();
        }

        private void ParseItem(string clipboard)
        {
            try
            {
                var factory = new PoEItemParserFactory(clipboard);
                var parser = factory.GetParser();
                var item = parser.Parse();

                ItemVM = ItemViewModel.CreateViewModel(item);
            }
            catch(FormatException)
            {
                ItemVM = new ErrorViewModel("Item data is not in the correct format");
            }
            catch
            {
                ItemVM = new ErrorViewModel("An error occured while parsing the item");
            }
        }

        private async Task SearchButtonClick()
        {
            string league = Leagues[SelectedLeagueIndex];

            try
            {
                string searchResult = await WebClient.SearchAsync(league, (ItemViewModel)ItemVM,
                    FieldValueLowerBound, FieldValueUpperBound, (ListTime)SelectedListTimeIndex);

                var searchJson = JToken.Parse(searchResult);
                int total = (int)searchJson["total"];

                var vm = new SearchResultsViewModel()
                {
                    League = league,
                    Id = searchJson["id"].ToString(),
                    Total = total
                };

                int fetchTotal = Math.Min(total, 100);

                if (fetchTotal == 0)
                {
                    ResultsViewModel = vm;
                    return;
                }

                string[] hashes;

                if (fetchTotal > 2)
                {
                    hashes = new string[]
                    {
                        searchJson["result"][0].ToString(),
                        searchJson["result"][fetchTotal / 2].ToString(),
                        searchJson["result"][fetchTotal - 1].ToString()
                    };
                }
                else
                {
                    hashes = new string[fetchTotal];

                    for (int i = 0; i < fetchTotal; i++)
                    {
                        hashes[i] = searchJson["result"][i].ToString();
                    }
                }

                string listings = await WebClient.FetchListingsAsync(hashes);
                var listingsJson = JToken.Parse(listings);
                int listingsCount = listingsJson["result"].Count();

                vm.MinimumListingPrice = GetPriceString(listingsJson["result"][0]);
                vm.MedianListingPrice = GetPriceString(listingsJson["result"][listingsCount / 2]);
                vm.MaximumListingPrice = GetPriceString(listingsJson["result"][listingsCount - 1]);
                ResultsViewModel = vm;
            }
            catch(HttpRequestException)
            {
                ResultsViewModel = new ErrorViewModel("Could not connect to pathofexile.com");
            }
            catch (InvalidOperationException)
            {
                ResultsViewModel = new ErrorViewModel("Problem requesting search results");
            }
        }

        protected virtual string GetClipboard()
        {
            return Clipboard.GetText();
        }

        protected virtual void SaveLeagueSelectionIndex()
        {
            Properties.Settings.Default.SelectedLeagueIndex = SelectedLeagueIndex;
            Properties.Settings.Default.Save();
        }

        private string GetPriceString(JToken listing)
        {
            return listing["listing"]["price"]["amount"].ToString() + ' ' + 
                listing["listing"]["price"]["currency"].ToString();
        }
    }
}
