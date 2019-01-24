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
    class CursViewModel : BaseViewModel
    {

        private DataService _databaseInteraction;
        private string _from;
        private string _to;
        private string _amount;
        private string _result;

        private ICommand _backComm;
        private ICommand _calcComm;
        private bool _execute = true;

        private ObservableCollection<string> _statuses;
        private ObservableCollection<string> _cursVal;

        string _statusTip = "Alege o moneda.";


        public string StatusTip
        {
            get
            {
                return _statusTip;
            }
            set
            {
                _statusTip = value;
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
        public string To
        {
            get
            {
                return _to;
            }
            set
            {
                _to = value;
            }
        }
        public string Amount
        {
            get
            {
                return _amount;
            }
            set
            {
                _amount = value;
            }
        }

        public string Result
        {
            get
            {
                return _result;
            }
            set
            {
                _result = value;
            }
        }


        public ObservableCollection<string> Statuses
        {
            get
            {
                return _statuses;
            }
            set
            {
                _statuses = value;
            }
        }

        public ObservableCollection<string> ListOfCurrency
        {
            get
            {
                return _cursVal;
            }
            set
            {
                _cursVal = value;
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
        public ICommand CalcComm
        {
            get
            {
                return _calcComm;
            }
            set
            {
                _calcComm = value;
            }
        }

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

        public CursViewModel()
        {

            _databaseInteraction = new DataService();
            Statuses = _databaseInteraction.GetAllCurrency();
            ListOfCurrency = _databaseInteraction.GetCurrency();
            BackComm = new CommandHandler(Back, param => this._execute);
            CalcComm = new CommandHandler(Calc, param => this._execute);

        }

        public string Textbox { get; set; } = "\t---";
        public void Calc(object obj)
        {
            if (_from == null || _to == null || _amount == null)
            {
                Textbox = " \t EROARE";
            }
            else
            {
                string r = _databaseInteraction.Convert(this._from, this._to, float.Parse(this._amount, System.Globalization.CultureInfo.InvariantCulture.NumberFormat));

                Textbox = " " + r;

            }

        }
        public void Back(object obj)
        {
            if (PageManager.Instance.IsLoggedIn == false)
                PageManager.Instance.CurrentPage = new LoginPage();
            else
            {
                PageManager.Instance.CurrentPage = new PlatiPage();
            }
        }

    }
}
