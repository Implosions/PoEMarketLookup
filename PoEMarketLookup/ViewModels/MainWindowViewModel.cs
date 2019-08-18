﻿using Newtonsoft.Json.Linq;
using PoEMarketLookup.PoE.Parsers;
using PoEMarketLookup.ViewModels.Commands;
using PoEMarketLookup.Web;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            string searchResult = null;

            try
            {
                searchResult = await WebClient.SearchAsync(league, (ItemViewModel)ItemVM,
                    FieldValueLowerBound, FieldValueUpperBound);
            }
            catch
            {
                ResultsViewModel = new ErrorViewModel("Could not connect to pathofexile.com");
                return;
            }

            if (searchResult == null)
            {
                ResultsViewModel = new ErrorViewModel("Problem requesting search results");
                return;
            }

            ResultsViewModel = new SearchResultsViewModel()
            {
                League = league
            };
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
    }
}
