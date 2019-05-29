﻿using System;
using System.Windows.Input;

namespace PoEMarketLookup.ViewModels.Commands
{
    public class BasicCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly Action<object> _action;

        public BasicCommand(Action<object> action)
        {
            _action = action;
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter) => _action(parameter);
    }
}
