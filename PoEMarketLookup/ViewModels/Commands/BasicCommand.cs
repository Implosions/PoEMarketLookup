using System;
using System.Windows.Input;

namespace PoEMarketLookup.ViewModels.Commands
{
    public class BasicCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly Action _action;
        private readonly Func<bool> _canExecute;

        public BasicCommand(Action action, Func<bool> canExecute = null)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute?.Invoke() ?? true;

        public void Execute(object parameter) => _action();

        public void InvokeCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
