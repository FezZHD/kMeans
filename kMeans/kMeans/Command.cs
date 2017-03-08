using System;
using System.Windows.Input;

namespace kMeans
{
    public class Command:ICommand
    {

        private Action command;

        public Command(Action action)
        {
            command = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            command.Invoke();
        }

        public event EventHandler CanExecuteChanged;
    }
}