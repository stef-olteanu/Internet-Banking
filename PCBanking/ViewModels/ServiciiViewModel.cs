using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PCBanking
{
    class ServiciiViewModel : BaseViewModel
    {
        public int id_user;
        private ICommand _backComm;
        private ICommand _activateComm;

        public ICommand ActivateComm
        {
            get
            {
                return _activateComm;
            }
            set
            {
                _activateComm = value;
            }
        }

        private string _from;

        private ObservableCollection<string> _services;

        public ObservableCollection<string> Servicii
        {
            get
            {
                return _services;
            }
            set
            {
                _services = value;
            }
        }

        public string From
        {
            get
            {
                return _from;
            }
            set
            {
                _from = value;
            }
        }

        private ICommand _modifyName;
        public ICommand ModifyName
        {
            get
            {
                return _modifyName;
            }
            set
            {
                _modifyName = value;
            }
        }
        private ICommand _modifySerie;
        public ICommand ModifySerie
        {
            get
            {
                return _modifySerie;
            }
            set
            {
                _modifySerie = value;
            }
        }
        private ICommand _modifyAdress;
        public ICommand ModifyAdress
        {
            get
            {
                return _modifyAdress;
            }
            set
            {
                _modifyAdress = value;
            }
        }
        private ICommand _modifyCountry;
        public ICommand ModifyCountry
        {
            get
            {
                return _modifyCountry;
            }
            set
            {
                _modifyCountry = value;
            }
        }

        private bool _execute = true;

        private string _nume;
        private string _pass;
        private string _serie;
        private string _adress;
        private string _country;

        private ObservableCollection<string> _taxes;
        public ObservableCollection<string> ListOfTaxes
        {
            get
            {
                return _taxes;
            }
            set
            {
                _taxes = value;
            }
        }
        public string Nume
        {
            get
            {
                return _nume;
            }
            set
            {
                _nume = value;
            }
        }
        public string Pass
        {
            get
            {
                return _pass;
            }
            set
            {
                _pass = value;
            }
        }
        public string Serie
        {
            get
            {
                return _serie;
            }
            set
            {
                _serie = value;
            }
        }
        public string Adress
        {
            get
            {
                return _adress;
            }
            set
            {
                _adress = value;
            }
        }
        public string Country
        {
            get
            {
                return _country;
            }
            set
            {
                _country = value;
            }
        }


        private DataService _databaseInteraction;
        public DataService DataBaseInteraction
        {
            get
            {
                return _databaseInteraction;
            }
            set
            {
                _databaseInteraction = value;
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



        public string NumeBox { get; set; } = "\t---";
        public string PassBox { get; set; } = "\t---";
        public string SerieBox { get; set; } = "\t---";
        public string AdressBox { get; set; } = "\t---";
        public string CountryBox { get; set; } = "\t---";
        public string CostBox { get; set; }

        public ServiciiViewModel()
        {
            _databaseInteraction = new DataService();
            NumeBox = MasterUser.Instance.FullName;

            SerieBox = MasterUser.Instance.SerieNumar;
            AdressBox = MasterUser.Instance.Adresa;
            CountryBox = MasterUser.Instance.Tara;
            id_user = _databaseInteraction.GetUserId(MasterUser.Instance.UserName);
            ListOfTaxes = _databaseInteraction.GetAllTaxes(id_user);

            // CostBox = _databaseInteraction.GetCostInfo();

            Servicii = _databaseInteraction.GetServicii();
            BackComm = new CommandHandler(Back, param => this._execute);
            ActivateComm = new CommandHandler(Activate, param => this._execute);
            ModifyName = new CommandHandler(Mod_Name, param => this._execute);
            ModifySerie = new CommandHandler(Mod_Serie, param => this._execute);
            ModifyCountry = new CommandHandler(Mod_Country, param => this._execute);
            ModifyAdress = new CommandHandler(Mod_Adress, param => this._execute);
        }

        public void Activate(object obj)
        {
            _databaseInteraction.ActivateServ(id_user, this._from);
        }


        public void Mod_Name(object obj)
        {
            _databaseInteraction.ModifyName(id_user, NumeBox);
            MasterUser.Instance.FullName = NumeBox;
        }

        public void Mod_Serie(object obj)
        {
            _databaseInteraction.ModifySerie(id_user, SerieBox);
            MasterUser.Instance.SerieNumar = SerieBox;
        }

        public void Mod_Country(object obj)
        {
            _databaseInteraction.ModifyCountry(id_user, CountryBox);
            MasterUser.Instance.Tara = CountryBox;
        }

        public void Mod_Adress(object obj)
        {
            _databaseInteraction.ModifyAdress(id_user, AdressBox);
            MasterUser.Instance.Adresa = AdressBox;
        }


        public void Back(object obj)
        {
            PageManager.Instance.CurrentPage = new HomePage();
        }



    }
}
