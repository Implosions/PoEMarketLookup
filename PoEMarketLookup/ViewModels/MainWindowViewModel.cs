using PoEMarketLookup.PoE.Parsers;
using PoEMarketLookup.ViewModels.Commands;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace PoEMarketLookup.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public ICommand PasteFromClipboardCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

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

        protected virtual string GetClipboard()
        {
            return Clipboard.GetText();
        }
    }
}
