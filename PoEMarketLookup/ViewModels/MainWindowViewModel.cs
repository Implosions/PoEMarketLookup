using PoEMarketLookup.ViewModels.Commands;
using System.Windows;
using System.Windows.Input;

namespace PoEMarketLookup.ViewModels
{
    public class MainWindowViewModel
    {
        public ICommand PasteFromClipboardCommand { get; }

        public string ItemText { get; set; }

        public MainWindowViewModel()
        {
            PasteFromClipboardCommand = new BasicCommand(SetItemTextFromClipboard);
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
