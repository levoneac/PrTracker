using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PrTracker.MVVM
{
    public class RelayCommand : ICommand
    {
        private Action<object> execute; //func with one param that cant return a value
        private Func<object, bool> canExecute; //func with one param that can return a value

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }
        public bool CanExecute(object? parameter)
        {
            return canExecute == null || canExecute(parameter); //if no function is given as canExecute, we assume that we can execute
                                                                //if they do give a function, we execute that function, which will tell us if we can execute
                                                                //So basically its just a function that checks if this.execute can run considering the current state
        }

        public void Execute(object? parameter)
        {
            execute(parameter);
        }
    }
}
