using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Input;

namespace PCBanking
{
    public class TransactionsViewModel : BaseViewModel
    {

        public string Suma { get; set; }
        
        public string FullName { get; set; }

        private ICommand _VerificareSold;


        public List<string> CardNumbers { get; set; }

        public List<string> ContNumbers { get; set; }

        public List<string> Transferuri { get; set; }

        public string ItemSelected { get; set; }

      

        public ICommand VerificareSold { get; set; }
        public ICommand Back { get; set; }


        private bool _canExecuteSold = true;

        private SolidColorBrush _nameColor = Brushes.White;

        public bool CanExecuteSold
        {
            get
            {
                return this._canExecuteSold;
            }
            set
            {
                if (this._canExecuteSold == value)
                {
                    return;
                }
                this._canExecuteSold = value;
            }
        }


        public TransactionsViewModel()
        {

            DataService dataService = new DataService();
            Back = new CommandHandler(GoBack, param => this._canExecuteSold);
            CardNumbers = dataService.GetCardsNumbers(MasterUser.Instance.UserName);
            VerificareSold = new CommandHandler(Sold, param => this._canExecuteSold);
            Transferuri = dataService.GetTransactions(MasterUser.Instance.UserName);
            ContNumbers = dataService.GetContNumbers(MasterUser.Instance.UserName);
            FullName = MasterUser.Instance.FullName.ToUpper();
            
        }

        private void GoBack(object obj)
        {
            PageManager.Instance.CurrentPage = new HomePage();
        }

        public void Sold(object obj)
        {
            DataService dataService = new DataService();
            if (ItemSelected == null)
            {
                Suma = "Selectati un cont!";
            }
            else
            {
                Suma = dataService.GetMoney(MasterUser.Instance.UserName, ItemSelected);
            }
        }




        public SolidColorBrush NameColor
        {
            get
            {
                return _nameColor;
               
            }
            set
            {
                _nameColor = value;
            }
        }


    }
}
