using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCBanking
{
    class MasterUser : User
    {
        #region Private Properties
        static private MasterUser _instance = null;

        #endregion


        #region Public Properties

        static public MasterUser Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new MasterUser();
                }
                return _instance;
            }
        }

        #endregion



    }
}
