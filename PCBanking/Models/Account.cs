using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCBanking
{
    class Account
    {
        public string NumeProprietar { get; set; }
        public string IBAN { get; set; }
        public double SumaCont { get; set; }
        public string Moneda { get; set; }
        public string TipCont { get; set; }
    }
}
