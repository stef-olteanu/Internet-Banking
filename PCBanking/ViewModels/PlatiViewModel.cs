using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PCBanking
{
    class PlatiViewModel : BaseViewModel
    {
        #region Public Properties

        #region Commands
        public ICommand HomeCommand { get; set; }
        public ICommand PlataConturiCommand { get; set; }
        public ICommand SchimbCommand { get; set; }
        public ICommand PlataLeiCommand { get; set; }
        public ICommand PlataFurnizorCommand { get; set; }
        public ICommand PlatiPlanificateCommand { get; set; }
        public ICommand PlatileMeleCommand { get; set; }
        public ICommand SearchPlata { get; set; }

        public ICommand ChangePlataSaved { get; set; }

        #endregion

        public DataService DatabaseInteraction { get; set; }

        public string SearchBar { get; set; }

        public bool CanExecute { get; set; }

        public string NumePlata { get; set; }

        public PlataSalvata ToSave { get; set; }

        #endregion


        #region Constructor

        public PlatiViewModel()
        {
            #region Initialize
            CanExecute = true;
            SearchBar = string.Empty;
            DatabaseInteraction = new DataService();
            NumePlata = string.Empty;


            
            HomeCommand = new CommandHandler(ChangeToHome, param => CanExecute);
            PlataConturiCommand= new CommandHandler(ChangeToPlataConturi, param => CanExecute);
            SchimbCommand = new CommandHandler(ChangeToSchimb, param => CanExecute);
            PlataLeiCommand = new CommandHandler(ChangeToPlataLei, param => CanExecute);
            PlataFurnizorCommand = new CommandHandler(ChangeToPlataFurnizori, param => CanExecute);
            PlatiPlanificateCommand = new CommandHandler(ChangeToPlanificate, param => CanExecute);
            PlatileMeleCommand = new CommandHandler(ChangeToPlatileMele, param => CanExecute);
            SearchPlata = new CommandHandler(ReturnPlata, param => CanExecute);
            ChangePlataSaved = new CommandHandler(ChangeToPlataSaved, param => CanExecute);

            #endregion




        }

        #endregion


        #region Public Commands
        public void ChangeToPlataSaved(object obj)
        {
            if(NumePlata != string.Empty)
                PageManager.Instance.CurrentPage = new PlataInLeiPage() { DataContext = new PlataInLeiViewModel(ToSave) };
        }
        
        public void ReturnPlata(object obj)
        {
            NumePlata = string.Empty;
            PlataSalvata returnPlata = new PlataSalvata();
            if (SearchBar != string.Empty)
            {
                returnPlata = DatabaseInteraction.GetPlata(SearchBar.ToString());
                if (returnPlata != null)
                {
                    NumePlata =  returnPlata.NumePlata ;
                    ToSave = returnPlata;
                }
            }
        }

        public void ChangeToHome(object obj)
        {
            PageManager.Instance.CurrentPage = new HomePage();
        }

        public void ChangeToPlataConturi(object obj)
        {
            PageManager.Instance.CurrentPage = new PlataConturileMelePage();
        }

        public void ChangeToSchimb(object obj)
        {
            PageManager.Instance.CurrentPage = new CursValutarPage();
        }

        public void ChangeToPlataLei(object obj)
        {
            PageManager.Instance.CurrentPage = new PlataInLeiPage();
        }

        public void ChangeToPlataFurnizori(object obj)
        {
            PageManager.Instance.CurrentPage = new PlataFurnizoriPage();
        }

        public void ChangeToPlanificate(object obj)
        {
            PageManager.Instance.CurrentPage = new ListaPlatiRecurentePage();
        }

        public void ChangeToPlatileMele(object obj)
        {
            PageManager.Instance.CurrentPage = new ListaPlatiSalvatePage();
        }
        #endregion


    }
}
