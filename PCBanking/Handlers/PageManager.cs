using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PCBanking
{
    /// <summary>
    /// Clasa ce se va ocupa de evidenta paginilor din MainWindow
    /// </summary>
    class PageManager : BaseViewModel
    {
        #region Private Properties
        private Page _currentPage = null;
        private static PageManager _instance = null;

        private string _registerPassword;
        private bool _isLoggedIn = false;
        #endregion


        #region Constructor
        public PageManager()
        {
        }
        #endregion


        #region Public Properties
        public bool IsLoggedIn
        {
            get
            {
                return _isLoggedIn;
            }
            set
            {
                _isLoggedIn = value;
            }
        }

        public Page CurrentPage
        {
            get
            {
                return _currentPage;
            }

            set
            {
                _currentPage = value;
            }
        }

        public static PageManager Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new PageManager();
                }
                return _instance;
            }
        }

        public string RegisterPassword
        {
            get
            {
                return _registerPassword;
            }
            set
            {
                _registerPassword = value;
            }
        }

        #endregion



    }
}
