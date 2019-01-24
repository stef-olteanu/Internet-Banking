using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PCBanking
{
    class PlatiRecurenteViewModel : BaseViewModel
    {

        #region Private proerties
        private string _selectedIBAN;
        private string _destinationIBAN;

        #endregion

        #region Public Properties
        public PlataSalvata SavedAlready { get; set; }
        public PlataSalvata ToSave { get; set; }

        public ObservableCollection<Account> MasterUserAccounts { get; set; }
        public ObservableCollection<String> Accounts { get; set; }
        public ObservableCollection<String> Frequency { get; set; }

        public string SelectedIBAN
        {
            get
            {
                return _selectedIBAN;
            }
            set
            {
                foreach (var account in MasterUserAccounts)
                {
                    if (account.IBAN == value)
                    {
                        Currency = account.Moneda;
                    }
                }
                _selectedIBAN = value;
            }
        }
        public string DestinationIBAN
        {
            get
            {
                return _destinationIBAN;
            }
            set
            {
                Account _tempAccount = new Account();
                _tempAccount = DatabaseInteraction.GetAccountByIBAN(value);
                if (_tempAccount != null)
                {
                    BANK = "BATM";
                }
                _destinationIBAN = value;
            }
        }
        public string BANK { get; set; }
        public string ReceiverName { get; set; }
        public string ErrorText { get; set; }

        public string SelectedFrequency { get; set; }


        public string Sum { get; set; }
        public string Currency { get; set; }


        public DataService DatabaseInteraction = new DataService();

        public bool CanExecute { get; set; }

        #endregion


        #region Public Commands

        public ICommand Cancel { get; set; }
        public ICommand Apply { get; set; }


        #endregion


        #region Constructor

        public PlatiRecurenteViewModel()
        {
            CanExecute = true;
            MasterUserAccounts = new ObservableCollection<Account>();
            Accounts = new ObservableCollection<string>();
            Frequency = new ObservableCollection<string>();
            Cancel = new CommandHandler(GoBack, param => CanExecute);
            Apply = new CommandHandler(DoIt, param => CanExecute);
            ErrorText = "Salveaza o plata recurenta";

            Frequency.Add("30");
            Frequency.Add("60");
            Frequency.Add("90");
            SelectedFrequency = string.Empty;

            MasterUserAccounts = DatabaseInteraction.GetAccountsForMasterUser();
            foreach (var account in MasterUserAccounts)
            {
                Accounts.Add(account.IBAN);
            }
        }


        #endregion


        #region Public Command Actions

        public void GoBack(object obj)
        {
            PageManager.Instance.CurrentPage = new ListaPlatiRecurentePage();
        }

        bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

        public void DoIt(object obj)
        {
            double _sumDouble = 0;
            if (Sum != null && IsDigitsOnly(Sum))
            {
                _sumDouble = double.Parse(Sum);
            }
            if (SelectedIBAN != string.Empty && DestinationIBAN != string.Empty && _sumDouble > 0 && SelectedFrequency != string.Empty )
            {
                DatabaseInteraction.AddPlataPlanificataUtilizator(DestinationIBAN, _sumDouble, DateTime.Today, Int32.Parse(SelectedFrequency), SelectedIBAN);
                PageManager.Instance.CurrentPage = new ListaPlatiRecurentePage();
            }
            else
            {
                ErrorText = "Toate campurile sunt obligatorii, in afara de detalii. Suma trebuie sa fie mai mare decat 0";
            }
        }

        #endregion
    }
}
