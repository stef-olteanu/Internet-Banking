using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace PCBanking
{
    class ContactViewModel
    {

        private ICommand _exitComm;
        private ICommand _backComm;
        private bool _execute = true;



        public ICommand ExitComm
        {
            get
            {
                return _exitComm;
            }
            set
            {
                _exitComm = value;
            }
        }
        public ICommand BackComm
        {
            get
            {
                return _backComm;
            }
            set
            {
                _backComm = value;
            }
        }


        public ContactViewModel()
        {
            ExitComm = new CommandHandler(Exit, param => this._execute);
            BackComm = new CommandHandler(Back, param => this._execute);
        }


        public void Back(object obj)
        {
            PageManager.Instance.CurrentPage = new LoginPage();
        }
        public void Exit(object obj)
        {
            System.Windows.Application.Current.Shutdown();
        }

    }
}
