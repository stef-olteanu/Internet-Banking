using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Security.Cryptography;
using System.Windows;

namespace PCBanking
{
    class ForgetPasswordViewModel : BaseViewModel
    {
        #region Public Properties
        public string Name { get; set; }
        public string SerieBuletin { get; set; }
        public string Parola { get; set; }
        public User UserC { get; set; }


        public ICommand Back { get; set; }
        public ICommand RetrievePassword { get; set; }

        public bool CanExecuteBack { get; set; }
        public bool CanExecuteRetrieve { get; set; }

        public DataService DatabaseInteraction { get; set; }
        #endregion


        #region Private Properties
        private BackgroundWorker _worker;
        #endregion


        #region Constructor
        public ForgetPasswordViewModel()
        {
            UserC = new User();
            DatabaseInteraction = new DataService();
            Parola = string.Empty;
            Name = string.Empty;
            SerieBuletin = string.Empty;
            CanExecuteBack = true;
            CanExecuteRetrieve = false;
            Back = new CommandHandler(GoToLogin, param => CanExecuteBack);
            RetrievePassword = new CommandHandler(GetPassword, param => CanExecuteRetrieve);

            _worker = new BackgroundWorker();
            _worker.WorkerSupportsCancellation = true;
            _worker.DoWork += worker_DoWork;

            _worker.RunWorkerAsync();
        }
        #endregion


        #region Private Commands

        private void GoToLogin(object obj)
        {
            _worker.CancelAsync();
            _worker.Dispose();
            PageManager.Instance.CurrentPage = new LoginPage();
            
        }

        private void GetPassword(object obj)
        {

            

            string Pass = Parola;
            string EncPass = string.Empty;

            byte[] data = Encoding.UTF8.GetBytes(Pass);
            using (HashAlgorithm sha = new SHA256Managed())
            {
                byte[] encryptedBytes = sha.TransformFinalBlock(data, 0, data.Length);
                EncPass = Convert.ToBase64String(sha.Hash);
            }


            DataService DS = new DataService();
            DS.ChangeOldPass(Name, EncPass, SerieBuletin);

            MessageBox.Show("Password Changed");
             _worker.CancelAsync();
            _worker.Dispose();
            PageManager.Instance.CurrentPage = new LoginPage();

        }
        #endregion

        #region Helpers

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while(true)
            {
                if(_worker.CancellationPending == true)
                {
                    _worker.Dispose();
                    _worker.CancelAsync();
                    break;
                }
                if(Name.Length > 0)
                {
                    User _databaseUser = new User();
                    _databaseUser = DatabaseInteraction.GetUser(Name);
                    if(_databaseUser != null)
                    {
                        UserC = _databaseUser;
                        if(_databaseUser.SerieNumar == SerieBuletin)
                        {
                            CanExecuteRetrieve = true;
                            
                        }
                    }
                }
            }
        }

        #endregion
    }
}
