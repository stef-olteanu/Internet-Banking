using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PCBanking
{
    class NewCardViewModel : BaseViewModel
    {
        #region Private Properties
        ICommand _back;
        ICommand _addCard;
        ICommand _addFront;
        ICommand _addBack;

        string _name;
        string _number;
        string _frontPath;
        string _backPath;

        bool _canExecuteBack = true;
        #endregion


        #region Public Properties
        public ICommand Back { get; set; }
        public ICommand AddCard { get; set; }
        public ICommand AddFront { get; set; }
        public ICommand AddBack { get; set; }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }


        public string Number
        {
            get
            {
                return _number;
            }
            set
            {
                _number = value;
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
        public string BackPaths
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

        public bool CanExecuteBack
        {
            get
            {
                return _canExecuteBack;
            }
            set
            {
                _canExecuteBack = value;
            }
        }
        public bool CanExecuteAdd { get; set; }

        public DataService DatabaseInteraction { get; set; }
        #endregion

        #region Constructor
        public NewCardViewModel()
        {
            CanExecuteAdd = true;
            DatabaseInteraction = new DataService();
            Back = new CommandHandler(BackToCards, parameter => CanExecuteBack);
            AddFront = new CommandHandler(AddFrontPhoto, parameter => CanExecuteBack);
            AddBack = new CommandHandler(AddBackPhoto, parameter => CanExecuteBack);
            AddCard = new CommandHandler(AdaugaCard, parameter => CanExecuteAdd );
            

        }
        #endregion


        #region Public Commands
        public void BackToCards(object obj)
        {
            PageManager.Instance.CurrentPage = new FidelityPage();
        }

        public void AddFrontPhoto(object obj)
        {
            // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();

            // Launch OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = openFileDlg.ShowDialog();
            // Get the selected file name and display in a TextBox.
            // Load content of file in a TextBlock
            if (result == true)
            {
                FrontPath = openFileDlg.FileName;
                //TextBlock1.Text = System.IO.File.ReadAllText(openFileDlg.FileName);
            }
        }

        public void AddBackPhoto(object obj)
        {
            // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();

            // Launch OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = openFileDlg.ShowDialog();
            // Get the selected file name and display in a TextBox.
            // Load content of file in a TextBlock
            if (result == true)
            {
                BackPaths = openFileDlg.FileName;
                //TextBlock1.Text = System.IO.File.ReadAllText(openFileDlg.FileName);
            }

        }

        public void AdaugaCard(object obj)
        {
            DatabaseInteraction.AddFidelityCard(Name, Number, FrontPath, BackPaths);
            PageManager.Instance.CurrentPage = new FidelityPage();
        }
        #endregion

    }
}
