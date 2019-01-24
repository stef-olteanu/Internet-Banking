using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PCBanking
{
    class PlataFurnizoriViewModel : BaseViewModel
    {
        #region Private Properties
        private string _selectedAccount;
        private string _selectedName;
        private bool CanExecute { get; set; }
        #endregion

        #region Public Properties
        public ObservableCollection<Account> MasterUserAccounts { get; set; }
        public ObservableCollection<string> Accounts { get; set; }

        public ObservableCollection<Furnizor> Furnizors { get; set; }
        public ObservableCollection<string> NumeFurnizori { get; set; }

        public DataService DatabaseInteraction { get; set; }

        public string SelectedAccount
        {
            get
            {
                return _selectedAccount;
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
                _selectedAccount = value;
            }
        }

        public string SelectedName
        {
            get
            {
                return _selectedName;
            }
            set
            {
                foreach(var furnizor in Furnizors)
                {
                    if(furnizor.NumeFurnizor == value)
                    {
                        FurnizorIBAN = furnizor.IBAN;
                    }
                }
                _selectedName = value;
            }
        }


        public string Sum { get; set; }
        public string Currency { get; set; }
        public string FurnizorIBAN { get; set; }
        public string CodAbonat { get; set; }
        public string Details { get; set; }

        public string ErrorText { get; set; }
        #endregion


        #region Public Commands
        public ICommand Apply { get; set; }
        public ICommand Cancel { get; set; }

        #endregion

        #region Constructor
        public PlataFurnizoriViewModel()
        {
            CanExecute = true;
            DatabaseInteraction = new DataService();
            Accounts = new ObservableCollection<string>();
            NumeFurnizori = new ObservableCollection<string>();

            Apply = new CommandHandler(DoIt, param => CanExecute);
            Cancel = new CommandHandler(GoBack, param => CanExecute);

            ErrorText = "Efectueaza o plata noua";

            MasterUserAccounts = DatabaseInteraction.GetAccountsForMasterUser();
            foreach(var account in MasterUserAccounts)
            {
                Accounts.Add(account.IBAN);
            }

            Furnizors = DatabaseInteraction.GetFurnizors();
            foreach(var furnizor in Furnizors)
            {
                NumeFurnizori.Add(furnizor.NumeFurnizor);
            }
        }

        #endregion

        #region Private Command Actions
        bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

        private void DoIt(object obj)
        {
            double _sumDouble = 0;
            if (Sum != null && IsDigitsOnly(Sum))
            {
                _sumDouble = double.Parse(Sum);
            }
            if (SelectedAccount != string.Empty && FurnizorIBAN != string.Empty && _sumDouble > 0)
            {
                foreach (var account in MasterUserAccounts)
                {
                    if (SelectedAccount == account.IBAN)
                    {

                        if (_sumDouble <= account.SumaCont)
                        {
                            foreach(var furnizor in Furnizors)
                            {
                                if(furnizor.NumeFurnizor == SelectedName)
                                {
                                    foreach(var abonat in furnizor.CoduriAbonati)
                                    {
                                        if(abonat == CodAbonat)
                                        {
                                            DatabaseInteraction.CreateTranzaction(SelectedAccount, FurnizorIBAN, Details, _sumDouble, "Catre furnizor");
                                            DatabaseInteraction.UpdateAccounts(SelectedAccount, null, _sumDouble);
                                            PageManager.Instance.CurrentPage = new PlatiPage();
                                            return;
                                        }
                                    }
                                    ErrorText = "Nu sunteti abonat la acest furnizor!";
                                }
                            }
                           
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

        private void GoBack(object obj)
        {
            PageManager.Instance.CurrentPage = new PlatiPage();
        }

        #endregion


    }
}
