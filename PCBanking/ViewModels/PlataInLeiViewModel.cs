using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PCBanking
{
    class PlataInLeiViewModel : BaseViewModel
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
        public string SelectedIBAN
        {
            get
            {
                return _selectedIBAN;
            }
            set
            {
                foreach(var account in MasterUserAccounts)
                {
                    if(account.IBAN == value)
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
                if(_tempAccount != null)
                {
                    BANK = "BATM";
                }
                _destinationIBAN = value;
            }
        }
        public string BANK { get; set; }
        public string ReceiverName { get; set; }
        public string Details { get; set; }
        public string NumePlataToSave { get; set; }
        public string ErrorText { get; set; }


        public string Sum { get; set; }
        public string Currency { get; set; }


        public DataService DatabaseInteraction = new DataService();

        public bool CanExecute { get; set; }

        #endregion


        #region Public Commands

        public ICommand Cancel { get; set; }
        public ICommand Apply { get; set; }
        public ICommand Save { get; set; }

        #endregion


        #region Constructor

        public PlataInLeiViewModel()
        {
            CanExecute = true;
            MasterUserAccounts =new ObservableCollection<Account>();
            Accounts = new ObservableCollection<string>();
            Cancel = new CommandHandler(GoBack, param => CanExecute);
            Apply = new CommandHandler(DoIt, param => CanExecute);
            Save = new CommandHandler(SavePlata, param => CanExecute);
            ErrorText = "Efectueaza o plata in lei";

            //SelectedIBAN = string.Empty;
            //Sum = string.Empty;
            //Currency = string.Empty;
            //DestinationIBAN = string.Empty;
            //BANK = string.Empty;
            //ReceiverName = string.Empty;
            //Details = string.Empty;
            //NumePlataToSave = string.Empty;

            MasterUserAccounts = DatabaseInteraction.GetAccountsForMasterUser();
            foreach (var account in MasterUserAccounts)
            {
                Accounts.Add(account.IBAN);
            }
        }

        public PlataInLeiViewModel(PlataSalvata saved)
        {
            MasterUserAccounts = new ObservableCollection<Account>();
            Accounts = new ObservableCollection<string>();
            SavedAlready = new PlataSalvata();
            SavedAlready = saved;
            DestinationIBAN = SavedAlready.IbanDestinatar;
            BANK = SavedAlready.BancaDestinatar;

            //Currency = string.Empty;
            CanExecute = true;
            Cancel = new CommandHandler(GoBack, param => CanExecute);
            Apply = new CommandHandler(DoIt, param => CanExecute);
            Save = new CommandHandler(SavePlata, param => CanExecute);
            ErrorText = "Efectueaza o plata in lei";

            //SelectedIBAN = string.Empty;
            //Sum = string.Empty;
            //Currency = string.Empty;
            //DestinationIBAN = string.Empty;
            //BANK = string.Empty;
            //ReceiverName = string.Empty;
            //Details = string.Empty;
            //NumePlataToSave = string.Empty;

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
            PageManager.Instance.CurrentPage = new PlatiPage();
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
            if (SelectedIBAN != string.Empty && DestinationIBAN != string.Empty && _sumDouble > 0)
            {
                foreach (var account in MasterUserAccounts)
                {
                    if (SelectedIBAN == account.IBAN)
                    {

                        if (_sumDouble <= account.SumaCont)
                        {
                            DatabaseInteraction.CreateTranzaction(SelectedIBAN, DestinationIBAN, Details, _sumDouble, "Catre utilizator");
                            DatabaseInteraction.UpdateAccounts(SelectedIBAN, DestinationIBAN, _sumDouble);
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

        public void SavePlata(object obj)
        {
            ToSave = new PlataSalvata();
            if (BANK != string.Empty && DestinationIBAN != string.Empty && NumePlataToSave != String.Empty)
            {
                ToSave.BancaDestinatar = BANK;
                ToSave.IbanDestinatar = DestinationIBAN;
                ToSave.NumePlata = NumePlataToSave;
                DatabaseInteraction.InsertPlata(ToSave);
            }
        }
        #endregion
    }
}
