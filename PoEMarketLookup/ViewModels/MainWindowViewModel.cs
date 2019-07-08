using PoEMarketLookup.PoE.Parsers;
using PoEMarketLookup.ViewModels.Commands;
using PoEMarketLookup.Web;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
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

        public string[] Leagues { get; set; }
        public int SelectedLeagueIndex { get; set; }
        public SearchResultsViewModel ResultsViewModel { get; set; }

        private ItemViewModel _itemViewModel;
        public ItemViewModel ItemViewModel
        {
            get => _itemViewModel;
            set
            {
                _itemViewModel = value;
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

                ItemViewModel = ItemViewModel.CreateViewModel(item);
            }
            catch
            {
                ItemViewModel = null;
            }
        }

        private async void SearchButtonClick()
        {
            ResultsViewModel = await RequestItemSearch(Leagues[SelectedLeagueIndex], ItemViewModel);
        }

        protected virtual string GetClipboard()
        {
            return Clipboard.GetText();
        }

        protected async virtual Task<SearchResultsViewModel> RequestItemSearch(string league, ItemViewModel vm)
        {
            var client = new HttpClient();
            var payload = new PoEJsonConverter(ItemViewModel).SerializeSearchParameters();
            string endpoint = @"https://www.pathofexile.com/api/trade/search/" + league;
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            var response = client.PostAsync(endpoint, content).Result;

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();

                return SearchResultsViewModel.CreateViewModel(result);
            }

            return null;
        }
    }
}
