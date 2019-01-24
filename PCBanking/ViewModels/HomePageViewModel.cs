using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace PCBanking
{
    public class HomePageViewModel : BaseViewModel
    {
        private ICommand _clickTranzactii;
        private bool _canclickTranzactii = true;
        private DataService _databaseInteraction;

        private ICommand _clickService;
        public ICommand clickTranzactii { get; set; }
        public ICommand LogoutCommand { get; set; }
        public ICommand PlatiCommand { get; set; }
        public ICommand ServiceCommand { get; set; }

        public User UserA { get; set; }

        private DataService DataBaseInteraction { get; set; }
       


 

        public HomePageViewModel()
        {
            DataBaseInteraction = new DataService();

            clickTranzactii = new CommandHandler(OpenTranzactii, param => this._canclickTranzactii);
            LogoutCommand = new CommandHandler(GoToLogin, param => this._canclickTranzactii);
            PlatiCommand = new CommandHandler(OpenPlati, param => this._canclickTranzactii);
            ServiceCommand = new CommandHandler(OpenService, param => this._canclickTranzactii);


            ObservableCollection<PlataPlanificataUtilizator> _tempUtilizator = new ObservableCollection<PlataPlanificataUtilizator>();
            ObservableCollection<PlataPlanificataFurnizor> _tempFurnizor = new ObservableCollection<PlataPlanificataFurnizor>();

            _tempUtilizator = DataBaseInteraction.GetPlatiPlanificateUtilizator();
            _tempFurnizor = DataBaseInteraction.GetPlatiPlanificateFurnizor();

            foreach(var plata in _tempUtilizator)
            {
                if((DateTime.Today - plata.LastPayed).TotalDays >= plata.FrecventaZile)
                {
                    string _tempIBAN = DataBaseInteraction.GetIBANByIdPlataForUser(plata.IdPlata);
                    DataBaseInteraction.CreateTranzaction(plata.IBANSursa, _tempIBAN, "Plata recurenta", plata.Suma, "Catre utilizator");
                    DataBaseInteraction.UpdateAccounts(plata.IBANSursa, _tempIBAN, plata.Suma);
                    DataBaseInteraction.UpdateLastPayedById(plata.IdPlata, DateTime.Today);
                }
            }

            foreach(var plata in _tempFurnizor)
            {
                if((DateTime.Today - plata.LastPayed).TotalDays >= plata.FrecventaZile)
                {
                    string _tempIBAN = DataBaseInteraction.GetIBANByIdPlataForFurnizor(plata.IdPlata);
                    DataBaseInteraction.CreateTranzaction(plata.IBANSursa, _tempIBAN, "Plata recurenta", plata.Suma, "Catre furnizor");
                    DataBaseInteraction.UpdateAccounts(plata.IBANSursa, null, plata.Suma);
                    DataBaseInteraction.UpdateLastPayedById(plata.IdPlata, DateTime.Today);

                }
            }
        }


        public void OpenService(object obj)
        {
            PageManager.Instance.CurrentPage = new ServiciiPage();
        }

        public void OpenPlati(object obj)
        {
            PageManager.Instance.CurrentPage = new PlatiPage();
        }

        public void OpenTranzactii(object obj)
        {

            PageManager.Instance.CurrentPage = new Transactions();

        }

        public void GoToLogin(object obj)
        {
            PageManager.Instance.CurrentPage = new LoginPage();
            PageManager.Instance.IsLoggedIn = false;
        }

        

    }
}
