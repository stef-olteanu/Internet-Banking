using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PCBanking
{
    class ListaPlatiSalvateViewModel : BaseViewModel
    {
        #region Public Properties

        public ObservableCollection<PlataSalvata> PlatiSalvate { get; set; }
        public ObservableCollection<string> NumePlati { get; set; }

        DataService DatabaseInstance { get; set; }

        public string SelectedPlata { get; set; }

        #endregion

        #region Public Commands
        public ICommand Back { get; set; }
        public ICommand Delete { get; set; }
        public bool CanExecute { get; set; }
        #endregion

        #region Constructor
        public ListaPlatiSalvateViewModel()
        {
            CanExecute = true;
            DatabaseInstance = new DataService();
            Back = new CommandHandler(goBack, param => CanExecute);
            Delete = new CommandHandler(goDelete, param => CanExecute);

            PlatiSalvate = new ObservableCollection<PlataSalvata>();
            NumePlati = new ObservableCollection<string>();

            PlatiSalvate = DatabaseInstance.GetPlati();

            foreach(var plata in PlatiSalvate)
            {
                NumePlati.Add(plata.NumePlata);
            }

        }


        #endregion

        #region Private Command Actions
        private void goBack(object obj)
        {
            PageManager.Instance.CurrentPage = new PlatiPage();
        }

        private void goDelete(object obj)
        {
            int contor = 0;
            if (!string.IsNullOrEmpty(SelectedPlata))
            {
                foreach (var plata in NumePlati)
                {
                    if (plata == SelectedPlata)
                    {
                        DatabaseInstance.RemovePlata(SelectedPlata);
                        break;
                    }
                    contor++;
                }
                NumePlati.RemoveAt(contor);
                PlatiSalvate.RemoveAt(contor);
                
            }
        }
        #endregion
    }
}
