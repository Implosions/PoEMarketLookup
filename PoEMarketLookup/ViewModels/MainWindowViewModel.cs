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

        private string _itemText;
        public string ItemText
        {
            get => _itemText;
            set
            {
                if(value != _itemText)
                {
                    _itemText = value;
                    OnPropertyChanged();
                }
            }
        }

        public MainWindowViewModel()
        {
            PasteFromClipboardCommand = new BasicCommand(SetItemTextFromClipboard);
        }

        private void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SetItemTextFromClipboard(object param)
        {
            ItemText = GetClipboard();
        }

        protected virtual string GetClipboard()
        {
            return Clipboard.GetText();
        }
    }
}
