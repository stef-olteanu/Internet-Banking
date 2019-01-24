using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCBanking
{
    /// <summary>
    /// Clasa ce tine detaliile despre obiectul Card Fidelitate
    /// </summary>
    class CardFidelitate
    {
        #region Private Properties
        string _nameCard;
        string _codCard;
        string _frontPath;
        string _backPath;
        #endregion


        #region Public Properties
        public string NameCard
        {
            get
            {
                return _nameCard;
            }
            set
            {
                _nameCard = value;
            }
        }

        public string CodCard
        {
            get
            {
                return _codCard;
            }
            set
            {
                _codCard = value;
            }
        }

        public string FrontPath
        {
            get
            {
                return _frontPath;
            }
            set
            {
                _frontPath = value;
            }
        }

        public string BackPath
        {
            get
            {
                return _backPath;
            }
            set
            {
                _backPath = value;
            }
        }

        #endregion

        #region Constructor
        public CardFidelitate(string _name, string _code, string _front, string _back)
        {
            
            _nameCard = _name;
            _codCard = _code;
            _frontPath = _front;
            _backPath = _back;
        }

        public CardFidelitate()
        {

        }
        #endregion  
    }
}
