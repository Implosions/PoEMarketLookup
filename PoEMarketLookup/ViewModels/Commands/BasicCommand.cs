using System;
using System.Windows.Input;

namespace PoEMarketLookup.ViewModels.Commands
{
    public class BasicCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly Action _action;

        public BasicCommand(Action action)
        {
            _action = action;
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter) => _action();
    }
}
