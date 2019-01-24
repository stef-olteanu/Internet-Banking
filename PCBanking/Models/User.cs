using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCBanking
{
    public class User
    {
        #region Proprietati Private
        private string _userName;
        private string _password;
        private string _serieNumar;
        private string _adresa;
        private string _tara;
        private string _statusFinanciar;
        private string _FullName;
        #endregion

        #region Proprietati Publice

        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
            }
        }

        public string FullName { get; set; }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;

            }
        }

        public string SerieNumar
        {
            get
            {
                return _serieNumar;
            }
            set
            {
                _serieNumar = value;
            }
        }

        public string Adresa
        {
            get
            {
                return _adresa;
            }
            set
            {
                _adresa = value;

            }
        }

        public string Tara
        {
            get
            {
                return _tara;
            }
            set
            {
                _tara = value;
            }
        }

        public string StatusFinanciar
        {
            get
            {
                return _statusFinanciar;
            }
            set
            {
                _statusFinanciar = value;
            }
        }

        #endregion

        #region Constructor
        public User()
        {
            _userName = string.Empty;
            _password = string.Empty;
            _adresa = string.Empty;
            _tara = string.Empty;
            _serieNumar = string.Empty;
            _statusFinanciar = string.Empty;
            _FullName = string.Empty;

        }
        #endregion

    }
}
