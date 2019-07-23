using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PoEMarketLookup.ViewModels.Commands
{
    public class AsyncCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public bool IsExecuting { get; private set; }

        private readonly Func<Task> _execute;
        private readonly Func<bool> _canExecute;

        public AsyncCommand(Func<Task> execute, Func<bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute?.Invoke() ?? true;

        public void Execute(object parameter) => RunAsync(ExecuteAsync());

        public async Task ExecuteAsync()
        {
            IsExecuting = true;

            try
            {
                await _execute();
            }
            finally
            {
                IsExecuting = false;
            }
        }

        public void InvokeCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        private async void RunAsync(Task task) => await task;
    }
}
