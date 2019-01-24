using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCBanking
{
    class PlataPlanificata
    {
        public int IdPlata { get; set; }
        public double Suma { get; set; }
        public int FrecventaZile { get; set; }
        public DateTime DataIncepere { get; set; }
        public string IBANSursa { get; set; }
        public DateTime LastPayed { get; set; }
    }
}
