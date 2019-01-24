using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PCBanking
{
    class CommandHandler : ICommand
    {
        #region Proprietati Private
        private Action<object> _action;
        private Predicate<object> _canExecute;
        private event EventHandler CanExecuteChangedInternal;

        private static bool DefaultCanExecute(object parameter)
        {
            return true;
        }
        #endregion


        #region Constructor
        public CommandHandler(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            if (canExecute == null)
            {
                throw new ArgumentNullException("canExecute");
            }

            this._action = execute;
            this._canExecute = canExecute;

        }
        #endregion


        #region Proprietati publice
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
                this.CanExecuteChangedInternal += value;
            }
            remove
            {
                CommandManager.RequerySuggested += value;
                this.CanExecuteChangedInternal += value;
            }
        }


        #endregion

        #region Helpers

        public bool CanExecute(object parameter)
        {
           
            return this._canExecute != null && this._canExecute(parameter);
        }

        public void Execute(object parameter)
        {
           
            this._action(parameter);
        }

        public void OnCanExecuteChanged()
        {
            EventHandler handler = this.CanExecuteChangedInternal;
            if( handler != null)
            {
                handler.Invoke(this, EventArgs.Empty);
            }
        }

        #endregion
    }
}
