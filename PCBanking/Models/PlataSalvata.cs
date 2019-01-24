using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCBanking
{
    class PlataSalvata
    {
        #region Public Properties

        public string NumePlata { get; set; }
        public string IbanDestinatar { get; set; }
        public string BancaDestinatar { get; set; }

        #endregion


        #region Constructor
        public PlataSalvata()
        {
            NumePlata = string.Empty;
            IbanDestinatar = string.Empty;
            BancaDestinatar = string.Empty;
        }
        #endregion
    }
}
