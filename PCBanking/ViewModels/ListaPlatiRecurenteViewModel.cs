using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PCBanking
{
    class ListaPlatiRecurenteViewModel
    {
        #region  Public Properties
        public ObservableCollection<PlataPlanificataFurnizor> PlatiFurnizor { get; set; }
        public ObservableCollection<PlataPlanificataUtilizator> PlatiUtilizator { get; set; }

        public ObservableCollection<string> PlatiPlanificate { get; set; }

        public DataService DatabaseInteraction { get; set; }

        public bool IsUtilizatorChecked { get; set; }
        public bool IsFurnizorChecked { get; set; }

        public string SelectedPlata { get; set; }

        #endregion


        #region Public Commands
        public ICommand Back { get; set; }
        public ICommand Delete { get; set; }
        public ICommand Add { get; set; }

        public bool CanExecute { get; set; }
        #endregion

        #region Constructor
        public ListaPlatiRecurenteViewModel()
        {
            CanExecute = true;
            DatabaseInteraction = new DataService();
            Back = new CommandHandler(GoBack, param => CanExecute);
            Add = new CommandHandler(AddPlata, param => CanExecute);
            Delete = new CommandHandler(DeletePlata, param => CanExecute);

            PlatiFurnizor = new ObservableCollection<PlataPlanificataFurnizor>();
            PlatiUtilizator = new ObservableCollection<PlataPlanificataUtilizator>();
            PlatiPlanificate = new ObservableCollection<string>();
            PlatiPlanificate.Add("Nume Suma Frecventa");

            PlatiFurnizor = DatabaseInteraction.GetPlatiPlanificateFurnizor();
            PlatiUtilizator = DatabaseInteraction.GetPlatiPlanificateUtilizator();

            foreach(var plata in PlatiFurnizor)
            {
                PlatiPlanificate.Add(plata.NumeFurnizor + " " + plata.Suma + " " + plata.FrecventaZile);
            }
            foreach(var plata in PlatiUtilizator)
            {
                PlatiPlanificate.Add(plata.NumeUtilizator + " " + plata.Suma + " " + plata.FrecventaZile);
            }
        }
        #endregion


        #region Private Command Actions
        private void GoBack(object obj)
        {
            PageManager.Instance.CurrentPage = new PlatiPage();
        }

        private bool isDigit(char c)
        {
            if(c>='0' && c<='9')
            {
                return true;
            }
            return false;
        }

        private void DeletePlata(object obj)
        {
            int contorPlatiPlanificate = 0;
            if (SelectedPlata != "Nume Suma Frecventa")
            {
                if (SelectedPlata != null && SelectedPlata != string.Empty)
                {
                    foreach (var plata in PlatiPlanificate)
                    {
                        if (plata == SelectedPlata)
                        {
                            string NumeBeneficiar = string.Empty;
                            int count = 0;
                            while (!isDigit(plata[count]))
                            {
                                if (!isDigit(plata[count + 1]))
                                    NumeBeneficiar = NumeBeneficiar + plata[count];
                                count++;
                            }
                            bool remove = false;
                            count = 0;
                            foreach (var plataAux in PlatiUtilizator)
                            {
                                if (plataAux.NumeUtilizator == NumeBeneficiar)
                                {
                                    remove = true;
                                    DatabaseInteraction.RemovePlataPlanificata(plataAux.IdPlata);
                                }
                                count++;
                            }
                            if (remove == true)
                            {
                                PlatiUtilizator.RemoveAt(count-1);
                                break;
                            }
                            count = 0;
                            remove = false;
                            foreach (var plataAux in PlatiFurnizor)
                            {
                                if (plataAux.NumeFurnizor == NumeBeneficiar)
                                {
                                    remove = true;
                                    DatabaseInteraction.RemovePlataPlanificata(plataAux.IdPlata);
                                }
                                count++;
                            }
                            if (remove == true)
                            {
                                PlatiFurnizor.RemoveAt(count-1);
                                break;
                            }

                        }
                        contorPlatiPlanificate++;
                    }
                    PlatiPlanificate.RemoveAt(contorPlatiPlanificate);
                }
                
            }
        }

        private void AddPlata(object obj)
        {
            if ((IsUtilizatorChecked == true && IsFurnizorChecked == false) || (IsUtilizatorChecked == false && IsFurnizorChecked == true))
            {
                if (IsFurnizorChecked == true)
                {
                    PageManager.Instance.CurrentPage = new PlatiRecurenteFurnizor();
                }
                if (IsUtilizatorChecked == true)
                {
                    PageManager.Instance.CurrentPage = new PlatiRecurentePage();
                }
            }
        }
        #endregion

    }
}
