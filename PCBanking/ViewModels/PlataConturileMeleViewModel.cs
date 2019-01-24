using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PCBanking
{
    class PlataConturileMeleViewModel : BaseViewModel
    {
        #region Private Properties
        private string _selectedSender;
        #endregion

        #region Public Properties
        public ObservableCollection<String> CurrentAccounts { get; set; }
        public ObservableCollection<String> EconomyAccounts { get; set; }
        public ObservableCollection<Account> UserAccounts { get; set; }

        public String Currency { get; set; }
        public String Sum { get; set; }
        public String Details { get; set; }
        public String ErrorText { get; set; }


        public string SelectedSender
        {
            get
            {
                return _selectedSender;
            }

            set
            {
                foreach(var account in UserAccounts)
                {
                    if(account.IBAN == value)
                    {
                        Currency = account.Moneda;
                    }
                }
                _selectedSender = value;
            }
        }
        public string SelectedDestination { get; set; }

        public DataService DatabaseInteraction { get; set; }

        public bool CanExecute { get; set; }

        #endregion

        #region Public Commands
        public ICommand CancelCommand { get; set; }
        public ICommand ApplyCommand { get; set; }
        public ICommand ChangeCommand { get; set; }
        #endregion


        #region Constructor
        public PlataConturileMeleViewModel()
        {
            ErrorText = string.Empty;
            CanExecute = true;

                        
            #region Initialize

            DatabaseInteraction = new DataService();
            CurrentAccounts = new ObservableCollection<string>();
            EconomyAccounts = new ObservableCollection<string>();
            UserAccounts = new ObservableCollection<Account>();


            CancelCommand = new CommandHandler(Cancel, param => CanExecute);
            ApplyCommand = new CommandHandler(Apply, param => CanExecute);
            ChangeCommand = new CommandHandler(Change, param => CanExecute);
            #endregion

            UserAccounts = DatabaseInteraction.GetAccountsForMasterUser();

            foreach (var account in UserAccounts)
            {
                if (account.TipCont == "Curent")
                {
                    CurrentAccounts.Add(account.IBAN);
                }
                else
                    EconomyAccounts.Add(account.IBAN);
            }


        }
        #endregion

        #region Private command actions

        bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

        private void Cancel(object obj)
        {
            PageManager.Instance.CurrentPage = new PlatiPage();
        }

        private void Apply(object obj)
        {
            double _sumDouble = 0;
            if (Sum != null && IsDigitsOnly(Sum))
            {
                _sumDouble = double.Parse(Sum);
            }
            if (SelectedSender != string.Empty && SelectedDestination != string.Empty && _sumDouble > 0)
            {
                foreach (var account in UserAccounts)
                {
                    if (SelectedSender == account.IBAN)
                    {

                        if (_sumDouble <= account.SumaCont)
                        {
                            DatabaseInteraction.CreateTranzaction(SelectedSender, SelectedDestination, Details, _sumDouble,"Catre utilizator");
                            DatabaseInteraction.UpdateAccounts(SelectedSender, SelectedDestination, _sumDouble);
                            PageManager.Instance.CurrentPage = new PlatiPage();
                        }
                        else
                        {
                            ErrorText = "Nu aveti bani suficienti in cont!";
                        }
                    }
                }
            }
            else
            {
                ErrorText = "Toate campurile sunt obligatorii, in afara de detalii. Suma trebuie sa fie mai mare decat 0";
            }
        }

        private void Change(object obj)
        {
            ObservableCollection<string> aux = new ObservableCollection<string>();

            aux = CurrentAccounts;
            CurrentAccounts = EconomyAccounts;
            EconomyAccounts = aux;
            Currency = string.Empty;
            Sum = string.Empty;
        }
        #endregion
    }
}
