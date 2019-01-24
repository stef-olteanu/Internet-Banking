using PCBanking.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace PCBanking
{
    class FAQViewModel : BaseViewModel
    {
        private ICommand _backCommand;
        private ICommand _clickCommand;
        private ICommand _clickCommand1;

        private bool _canExecuteBack = true;
        private bool _canExecuteClick = true;
        private bool _canExecuteClick1 = true;


        public ICommand clickCommand { get; set; }
        public ICommand clickCommand1 { get; set; }
        public ICommand clickCommand2 { get; set; }

        public string Message { get; set; } = FAQHelper.Instance.Questions[0].Hint;
        public string Message1 { get; set; } = FAQHelper.Instance.Questions[1].Hint;
        public string Message2 { get; set; } = FAQHelper.Instance.Questions[2].Hint;


        public string Hint { get; set; } = FAQHelper.Instance.Questions[0].Description;
        public string Hint1 { get; set; } = FAQHelper.Instance.Questions[1].Description;
        public string Hint2 { get; set; } = FAQHelper.Instance.Questions[2].Description;


        public ICommand BackCommand { get; set; }


        public void ChangeToLogin(object obj)
        {
            PageManager.Instance.CurrentPage = new LoginPage();
        }

        public void Mesaj(object obj)
        {

            MessageBox.Show(FAQHelper.Instance.Questions[0].Description);


        }

        public void Mesaj1(object obj)
        {

            MessageBox.Show(FAQHelper.Instance.Questions[1].Description);


        }

        public void Mesaj2(object obj)
        {

            MessageBox.Show(FAQHelper.Instance.Questions[2].Description);


        }

        public FAQViewModel()
        {
            BackCommand = new CommandHandler(ChangeToLogin, param => this._canExecuteBack);
            clickCommand = new CommandHandler(Mesaj, param => this._canExecuteClick);
            clickCommand1 = new CommandHandler(Mesaj1, param => this._canExecuteClick);
            clickCommand2 = new CommandHandler(Mesaj2, param => this._canExecuteClick);

        }




    }
}
