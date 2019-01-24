using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCBanking
{
    class Furnizor
    {
        #region Public Properties
        public string NumeFurnizor { get; set; }
        public string IBAN { get; set; }
        public ObservableCollection<string> CoduriAbonati { get; set; }

        #endregion

        #region Constructor

        public Furnizor()
        {
            CoduriAbonati = new ObservableCollection<string>();
        }

        #endregion
    }
}
