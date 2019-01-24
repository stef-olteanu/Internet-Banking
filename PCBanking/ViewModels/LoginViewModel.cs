using PCBanking;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using PCBanking;
using System.Security.Cryptography;

namespace PCBanking
{
    class LoginViewModel : BaseViewModel
    {

        #region Proprietati Private
        private ICommand _registerCommand;
        private ICommand _loginCommand;
        private ICommand _fidelityCommand;
        

        private bool _canExecuteRegister = true;
        private bool _canExecuteContact = true;
        private bool _canExecuteLogin = true;
        private bool _canExecuteFaq = true;
        private User _user;

        private BackgroundWorker _worker;

        private string _nameToolTip;
        private string _passwordToolTip;

        private SolidColorBrush _nameColor = Brushes.Black;
        private SolidColorBrush _passwordColor = Brushes.Black;

        private DataService _databaseInteraction;
        #endregion


        #region Proprietati Publice
        public BackgroundWorker Worker
        {
            get
            {
                return _worker;
            }
            set
            {
                _worker = value;
            }
        }

        
        public bool CanExecuteRegister
        {
            get
            {
                return this._canExecuteRegister;
            }
            set
            {
                if (this._canExecuteRegister == value)
                {
                    return;
                }
                this._canExecuteRegister = value;
            }

        }

        public bool CanExecuteLogin
        {
            get
            {
                return this._canExecuteLogin;
            }
            set
            {
                if (this._canExecuteLogin == value)
                {
                    return;
                }
                this._canExecuteLogin = value;
            }

        }

        public bool CanExecuteFaq
        {
            
                 get
            {
                return this._canExecuteFaq;
            }
            set
            {
                if (this._canExecuteFaq == value)
                {
                    return;
                }
                this._canExecuteFaq = value;
            }

        }

        public ICommand RegisterCommand { get; set; }

        public ICommand FAQPage { get; set; }
     

        public ICommand LoginCommand { get; set; }

        public ICommand ContactCommand { get; set; }

        public ICommand FidelityCommand { get; set; }

        public ICommand CursCommand { get; set; }

        public ICommand ForgetPassword { get; set; }

        public User UserA { get; set; }
        

        public string NameToolTip { get; set; }
      

        public string PasswordToolTip { get; set; }
       

        public SolidColorBrush NameColor
        {
            get
            {
                return _nameColor;
            }
            set
            {
                _nameColor = value;
            }
        }

        public SolidColorBrush PasswordColor
        {
            get
            {
                return _passwordColor;
            }
            set
            {
                _passwordColor = value;
            }
        }

        public DataService DataBaseInteraction
        {
            get
            {
                return _databaseInteraction;
            }
            set
            {
                _databaseInteraction = value;
            }
        }


        #endregion


        #region Constructor
        public LoginViewModel()
        { 

            UserA = new User();
            DataBaseInteraction = new DataService();

            RegisterCommand = new CommandHandler(ChangeToRegister, param => this._canExecuteRegister);
            LoginCommand = new CommandHandler(LoginAction, param => this._canExecuteLogin);
            FAQPage = new CommandHandler(OpenFaq,param => this.CanExecuteFaq);
            FidelityCommand = new CommandHandler(ChangeToFidelity, param => this.CanExecuteRegister);
            ContactCommand = new CommandHandler(ContactO, param => this._canExecuteContact);
            CursCommand = new CommandHandler(ChangeToCurs, param => this._canExecuteFaq);
            ForgetPassword = new CommandHandler(ChangeToForget, param => this.CanExecuteFaq);
            

            _worker = new BackgroundWorker();
            _worker.WorkerSupportsCancellation = true;
            _worker.DoWork += worker_DoWork;

           Worker.RunWorkerAsync();

        }
        #endregion


        #region Helpers
        public void ChangeToCurs(object obj)
        {
            Worker.CancelAsync();
            Worker.Dispose();
            PageManager.Instance.CurrentPage = new CursValutarPage();
        }

        public void ChangeToForget(object obj)
        {
            Worker.CancelAsync();
            Worker.Dispose();
            PageManager.Instance.CurrentPage = new ForgotPasswordPage();
        }

        public void ChangeToFidelity(object obj)
        {
            Worker.CancelAsync();
            Worker.Dispose();
            PageManager.Instance.CurrentPage = new FidelityPage();
        }

        public void ContactO(object obj)
        {
            Worker.CancelAsync();
            Worker.Dispose();
            PageManager.Instance.CurrentPage = new Contact();
        }


        public void ChangeToRegister(object obj)
        {
            this.Worker.CancelAsync();
            this.Worker.Dispose();
            PageManager.Instance.CurrentPage = new RegisterPage();
        }

        public void OpenFaq(object obj)
        {
            Worker.CancelAsync();
            Worker.Dispose();
            PageManager.Instance.CurrentPage = new FAQ();
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if (Worker.CancellationPending == true)
                {

                    Worker.Dispose();
                    Worker.CancelAsync();
                    break;
                }
                if (UserA.UserName.Length > 0)
                {
                    User _databaseUser = DataBaseInteraction.GetUser(UserA.UserName);
                    UserA.Password = PageManager.Instance.RegisterPassword;

                    string Pass = UserA.Password;
                    string EncPass = string.Empty;
                    if (Pass != null)
                    {
                        byte[] data = Encoding.UTF8.GetBytes(Pass);
                        using (HashAlgorithm sha = new SHA256Managed())
                        {
                            byte[] encryptedBytes = sha.TransformFinalBlock(data, 0, data.Length);
                            EncPass = Convert.ToBase64String(sha.Hash);
                        }
                    }

                    if (_databaseUser != null)
                    {
                        NameColor = Brushes.Black;
                        NameToolTip = "";
                        if (EncPass == _databaseUser.Password)
                        {
                            _canExecuteLogin = true;
                            PasswordColor = Brushes.Black;
                            PasswordToolTip = "";
                        }
                        else
                        {
                            _canExecuteLogin = false;
                            PasswordColor = Brushes.Red;
                            PasswordToolTip = "Parola este incorecta";
                        }
                    }
                    else
                    {
                        NameToolTip = "Numele introdus nu exista";
                        NameColor = Brushes.Red;
                        _canExecuteLogin = false;
                    }
                }
                else
                {
                    NameToolTip = "Trebuie sa introduceti un nume";
                    _canExecuteLogin = false;
                }
            }
        }

        public void LoginAction(object obj)
        {
            _worker.CancelAsync();
            _worker.Dispose();

            PageManager.Instance.IsLoggedIn = true;
            User _databaseUser = DataBaseInteraction.GetUser(UserA.UserName);

            MasterUser.Instance.UserName = _databaseUser.UserName;
            MasterUser.Instance.Password = _databaseUser.Password;
            MasterUser.Instance.SerieNumar = _databaseUser.SerieNumar;
            MasterUser.Instance.Adresa = _databaseUser.Adresa;
            MasterUser.Instance.Tara = _databaseUser.Tara;
            MasterUser.Instance.StatusFinanciar = _databaseUser.StatusFinanciar;
            MasterUser.Instance.FullName = _databaseUser.FullName;
            
         
            PageManager.Instance.CurrentPage =  new HomePage();

            //PageManager.Instance.CurrentPage = new Transactions() { DataContext = new TransactionsViewModel(_databaseUser) };
        }

        #endregion

    }
}
