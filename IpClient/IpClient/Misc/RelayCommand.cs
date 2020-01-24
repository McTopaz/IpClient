using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MobileNetworkClient.Misc
{
    class RelayCommand : ICommand
    {
        public delegate void CallbackHandler(object parameter = null);
        public event CallbackHandler Callback;
        public event EventHandler CanExecuteChanged;

        public Predicate<object> Enable { get; set; }

        public RelayCommand()
        {
            Callback += RelayCommand_Callback;
        }

        private void RelayCommand_Callback(object parameter = null)
        {
        }


        public bool CanExecute(object parameter)
        {
            return Enable == null ? true : Enable(parameter);
        }

        public void Execute(object parameter = null)
        {
            Callback(parameter);
        }
    }

    class RelayCommand<T> : ICommand
    {
        public delegate void CallbackHandler(T parameter);
        public event CallbackHandler Callback;
        public event EventHandler CanExecuteChanged;

        public Predicate<object> Enable { get; set; }

        public RelayCommand()
        {
            Callback += RelayCommand_Callback;
        }

        private void RelayCommand_Callback(T parameter)
        {
        }

        public bool CanExecute(object parameter)
        {
            return Enable == null ? true : Enable(parameter);
        }

        public void Execute(object parameter)
        {
            Callback((T)parameter);
        }
    }
}
