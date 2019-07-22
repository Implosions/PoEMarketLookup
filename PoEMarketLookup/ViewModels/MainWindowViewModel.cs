using PoEMarketLookup.PoE.Parsers;
using PoEMarketLookup.ViewModels.Commands;
using PoEMarketLookup.Web;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PoEMarketLookup.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public ICommand PasteFromClipboardCommand { get; }
        public ICommand SearchCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public IList<string> Leagues { get; set; } = new List<string>() { "Standard", "Hardcore" };
        public int SelectedLeagueIndex { get; set; }
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

        public MainWindowViewModel()
        {
            PasteFromClipboardCommand = new BasicCommand(PasteButtonClick);
            SearchCommand = new BasicCommand(SearchButtonClick);
        }

        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void PasteButtonClick()
        {
            try
            {
                var factory = new PoEItemParserFactory(GetClipboard());
                var parser = factory.GetParser();
                var item = parser.Parse();

                ItemVM = ItemViewModel.CreateViewModel(item);
            }
            catch
            {
                ItemVM = new ErrorViewModel("Item data is not in the correct format");
            }
        }

        private async void SearchButtonClick()
        {
            SearchResultsViewModel vm = null;

            try
            {
                vm = await RequestItemSearch(Leagues[SelectedLeagueIndex], (ItemViewModel)ItemVM);
            }
            catch
            {
                ResultsViewModel = new ErrorViewModel("Could not connect to pathofexile.com");
                return;
            }

            if (vm == null)
            {
                ResultsViewModel = new ErrorViewModel("Problem requesting search results");
            }
            else
            {
                ResultsViewModel = vm;
            }
        }

        protected virtual string GetClipboard()
        {
            return Clipboard.GetText();
        }

        protected async virtual Task<SearchResultsViewModel> RequestItemSearch(string league, ItemViewModel vm)
        {
            var client = new OfficialTradeWebClient();

            return await client.SearchAsync(league, vm);
        }
    }
}
