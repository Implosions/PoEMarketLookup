using System;
using System.Windows.Input;

namespace PoEMarketLookup.ViewModels.Commands
{
    public class BasicCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action _action;
        private bool canExcute = true;

        public BasicCommand(Action action)
        {
            _action = action;
        }

        public bool CanExecute(object parameter) => canExcute;

        public void Execute(object parameter) => _action();

        public void SetCanExecute(bool value)
        {
            if(canExcute == value)
            {
                return;
            }

            canExcute = value;

            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
