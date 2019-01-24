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
using System.Security.Cryptography;

namespace PCBanking
{

    class RegisterViewModel : BaseViewModel 
    {

        #region Proprietati Private
        private ICommand _backCommand;
        private ICommand _registerCommand;
        private ICommand _OpenFaq;
        private bool _canExecuteBack = true;
        private bool _canExecuteRegister = true;
        private bool _canExecuteFaq = true;
        private User _user;
        private DataService _databaseInteraction;
        private string _introductText = "Va rugam sa introduceti datele cerute";

        /// <summary>
        /// Pentru a seta culoarea fiecarui camp, culoare ce semnalizeaza eroare
        /// </summary>
        /// 
        SolidColorBrush _userNameColor;
        SolidColorBrush _nameColor;
        SolidColorBrush _passwordColor;
        SolidColorBrush _serieColor;
        SolidColorBrush _adresaColor;
        SolidColorBrush _taraColor;
        SolidColorBrush _statusColor;

        /// <summary>
        /// Pentru a seta tooltips
        /// </summary>
        string _userNameTip = "Introdu un nume de utilizator cu mai mult de 4 caractere";
        string _passwordTip = "Parola trebuie sa contina cel putin un caracter A-Z, un caracter a-z si un caracter 0-9. Lungime minima 6";
        string _serieTip = "Introdu seria si numarul din buletin. Trebuie sa aiba fix 8 caractere";
        string _adresaTip = "Introdu adresa din buletin. Nu poate ramane necompletat";
        string _taraTip = "Introdu tara de origine. Nu poate ramane necompletat";
        string _statusTip = "Alege un status financiar. Nu poate ramane neales";
        string _nameTip = "Introdu numele complet";
        string _passwordHash = string.Empty;

        private BackgroundWorker _worker;

        /// <summary>
        /// Statuses
        /// </summary>
        /// 
        private ObservableCollection<string> _statuses;
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

        public bool CanExecuteBack
        {
            get
            {
                return this._canExecuteBack;
            }
            set
            {
                if(this._canExecuteBack == value)
                {
                    return;
                }
                this._canExecuteBack = value;
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

        public bool CanExecuteRegister
        {
            get
            {
                return this._canExecuteRegister;
            }
            set
            {
                if(this._canExecuteRegister == value)
                {
                    return;
                }
                this._canExecuteRegister = value;
            }
        }

        public ICommand BackCommand
        {
            get
            {
                return _backCommand;
            }
            set
            {
                _backCommand = value;
            }
        }

        public ICommand RegisterCommand
        {
            get
            {
                return _registerCommand;
            }
            set
            {
                _registerCommand = value;
            }
        }

        public User UserA
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
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

        public string IntroductText
        {
            get
            {
                return _introductText;
            }
            set
            {
                _introductText = value;
            }
        }



        /// <summary>
        /// Proprietati publice ce vor tine evidenta culorilor
        /// </summary>
        public SolidColorBrush UserNameColor
        {
            get
            {
                return _userNameColor;
            }
            set
            {
                _userNameColor = value;
            }
        }

        

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

        public SolidColorBrush SerieColor
        {
            get
            {
                return _serieColor;
            }
            set
            {
                _serieColor = value;
            }
        }

        public SolidColorBrush AdresaColor
        {
            get
            {
                return _adresaColor;
            }
            set
            {
                _adresaColor = value;
            }
        }

        public SolidColorBrush TaraColor
        {
            get
            {
                return _taraColor;
            }
            set
            {
                _taraColor = value;
            }
        }

        public SolidColorBrush StatusColor
        {
            get
            {
                return _statusColor;
            }
            set
            {
                _statusColor = value;
            }
        }

        /// <summary>
        /// Proprietati ce vor tine evidenta tips-urilor
        /// </summary>
        /// 
        public string UserNameTip
        {
            get
            {
                return _userNameTip;
            }
            set
            {
                _userNameTip = value;
            }
        }

        public string NameTip
        {
            get
            {
                return _nameTip;
            }
            set
            {
                _nameTip = value;
            }
        }

        public string PasswordTip
        {
            get
            {
                return _passwordTip;
            }
            set
            {
                _passwordTip = value;
            }
        }



        public string SerieTip
        {
            get
            {
                return _serieTip;
            }
            set
            {
                _serieTip = value;
            }
        }

        public string AdresaTip
        {
            get
            {
                return _adresaTip;
            }
            set
            {
                _adresaTip = value;
            }
        }

        public string TaraTip
        {
            get
            {
                return _taraTip;
            }
            set
            {
                _taraTip = value;
            }
        }

        public string StatusTip
        {
            get
            {
                return _statusTip;
            }
            set
            {
                _statusTip = value;
            }
        }


        public ObservableCollection<string> Statuses
        {
            get
            {
                return _statuses;
            }
            set
            {
                _statuses = value;
            }
        }
        #endregion



        #region Helpers




        public void ChangeToLogin(object obj)
        {
            Worker.CancelAsync();
            Worker.Dispose();
            PageManager.Instance.CurrentPage = new LoginPage();
            
        }

        public void OpenFaqPage(object obj)
        {
            PageManager.Instance.CurrentPage = new FAQ();
        }



        public void RegisterUser(object obj)
        {
            string Pass = UserA.Password;
            string EncPass = string.Empty;

            byte[] data = Encoding.UTF8.GetBytes(Pass);
            using (HashAlgorithm sha = new SHA256Managed())
            {
                byte[] encryptedBytes = sha.TransformFinalBlock(data, 0, data.Length);
                EncPass = Convert.ToBase64String(sha.Hash);
            }

                _databaseInteraction.AddPerson
                (
                    UserA.UserName,
                    EncPass,
                    UserA.SerieNumar,
                    UserA.Adresa,
                    UserA.Tara,
                    _databaseInteraction.getStatusId(UserA.StatusFinanciar),
                    UserA.FullName
                );
            Worker.CancelAsync();
            Worker.Dispose();
            PageManager.Instance.CurrentPage = new LoginPage();

        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            _userNameTip = "Introdu un nume de utilizator cu mai mult de 4 caractere";
            _passwordTip = "Parola trebuie sa contina cel putin un caracter A-Z, un caracter a-z si un caracter 0-9. Lungime minima 6";
            _serieTip = "Introdu seria si numarul din buletin. Trebuie sa aiba fix 8 caractere";
            _adresaTip = "Introdu adresa din buletin. Nu poate ramane necompletat";
            _taraTip = "Introdu tara de origine. Nu poate ramane necompletat";
            _statusTip = "Alege un status financiar. Nu poate ramane neales";
            _nameTip = "Introdu numele complet";


            while (true)
            {
                if (Worker.CancellationPending == true)
                {

                    Worker.Dispose();
                    Worker.CancelAsync();
                    break;
                }

                bool[] verifCanExecute = new bool[7];
                UserA.Password = PageManager.Instance.RegisterPassword;
                //Numele nu trebuie sa fie gol sau sa contina mai putin de 4 caractere
               

                if (UserA.UserName.Length < 4)
                {
                    verifCanExecute[0] = false;
                    UserNameColor = Brushes.Red;
                }
                else
                {
                    bool verif = false; 
                    ObservableCollection<User> _usersInDb = new ObservableCollection<User>();
                    _usersInDb = _databaseInteraction.GetAllUsers();
                    foreach (User user in _usersInDb)
                    {
                        if (user.UserName == UserA.UserName)
                        {
                            
                            verif = true;
                        }
                    }

                    if(verif == false)
                    {
                        verifCanExecute[0] = true;
                        UserNameColor = Brushes.Green;
                        UserNameTip = "Introdu un nume de utilizator cu mai mult de 4 caractere";
                    }
                    else
                    {
                        verifCanExecute[0] = false;
                        UserNameColor = Brushes.Red;
                        UserNameTip = "Exista deja un utilizator cu acelasi nume!";
                    }

                  
                }
                var regexp = new Regex("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.{6,})");

                if(UserA.FullName != null)
                {
                    if(UserA.FullName != string.Empty)
                    {
                        NameColor = Brushes.Green;
                        NameTip = "Introdu numele complet";
                        verifCanExecute[6] = true;
                    }
                    else
                    {
                        verifCanExecute[6] = false;
                        NameColor = Brushes.Red;
                        NameTip = "Trebuie sa introduceti numele si prenumele";
                    }

                }

                if (UserA.Password != null)
                {
                    if (!regexp.IsMatch(UserA.Password))
                    {
                        verifCanExecute[1] = false;
                        PasswordColor = Brushes.Red;
                    }
                    else
                    {
                        PasswordColor = Brushes.Green;
                        verifCanExecute[1] = true;
                    }
                }
                regexp = new Regex("^[A-Z]{2}[0-9]{6}");
                if (UserA.SerieNumar != string.Empty)
                {
                    if (!regexp.IsMatch(UserA.SerieNumar))
                    {
                        verifCanExecute[2] = false;
                        SerieColor = Brushes.Red;
                    }
                    else
                    {
                        verifCanExecute[2] = true;
                        SerieColor = Brushes.Green;
                    }

                }
                else
                {
                    verifCanExecute[2] = false;
                    SerieColor = Brushes.Red;
                }
                if (UserA.Adresa == string.Empty)
                {
                    verifCanExecute[3] = false;
                    AdresaColor = Brushes.Red;
                }
                else
                {
                    verifCanExecute[3] = true;
                    AdresaColor = Brushes.Green;
                }
                if (UserA.Tara == string.Empty)
                {
                    verifCanExecute[4] = false;
                    TaraColor = Brushes.Red;
                }
                else
                {
                    verifCanExecute[4] = true;
                    TaraColor = Brushes.Green;
                }

                if (UserA.StatusFinanciar == string.Empty)
                {
                    verifCanExecute[5] = false;
                    StatusColor = Brushes.Red;
                }
                else
                {
                    verifCanExecute[5] = true;
                    StatusColor = Brushes.Green;
                }

                if (!verifCanExecute.Contains(false))
                {
                    this._canExecuteRegister = true;
                }
                else
                {
                    this._canExecuteRegister = false;
                }

            }
        }

        #endregion

        #region Constructor
        public RegisterViewModel()
        {
            
            _databaseInteraction = new DataService();
            Statuses = _databaseInteraction.GetAllStatus();

            

            UserA = new User();
            BackCommand = new CommandHandler(ChangeToLogin, param => this._canExecuteBack);
            RegisterCommand = new CommandHandler(RegisterUser, param => this._canExecuteRegister);
           





            UserNameColor = Brushes.Black;
            NameColor = Brushes.Red;
            PasswordColor = Brushes.Black;
            SerieColor = Brushes.Black;
            AdresaColor = Brushes.Black;
            TaraColor = Brushes.Black;
            StatusColor = Brushes.Black;

            _canExecuteRegister = false;

            Worker = new BackgroundWorker();
            Worker.WorkerSupportsCancellation = true;
            Worker.DoWork += worker_DoWork;
            
 
           Worker.RunWorkerAsync();


        }
        #endregion
    }
}
