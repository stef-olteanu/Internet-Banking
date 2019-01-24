using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace PCBanking
{
    class FidelityViewModel : BaseViewModel
    {
        #region Private Properties
        ObservableCollection<CardFidelitate> _cards = new ObservableCollection<CardFidelitate>();
        ObservableCollection<string> _cardImages = new ObservableCollection<string>();

        ICommand _newCard;
        ICommand _backLogin;
        private bool _canExecute = true;
        string _welcomeText;

        #endregion

        #region Public Properties
        DataService DatabaseInteraction { get; set; }

        public bool CanExecute
        {
            get
            {
                return _canExecute;
            }
            set
            {
                _canExecute = value;
            }
        }

        public ICommand NewCard
        {
            get
            {
                return _newCard;
            }
            set
            {
                _newCard = value;
            }
        }

        public ICommand BackLogin
        {
            get
            {
                return _backLogin;
            }
            set
            {
                _backLogin = value;
            }
        }

        public ICommand ChangePhoto { get; set; }

        public string ClickItem { get; set; }

        public ObservableCollection<string> CardImages
        {
            get
            {
                return _cardImages;
            }
            set
            {
                _cardImages = value;
            }
        }

        ObservableCollection<CardFidelitate> Cards
        {
            get
            {
                return _cards;
            }
            set
            {
                _cards = value;
            }
        }
        /// <summary>
        /// WelcomeText se va schimba in functie de numarul de carduri, se va seta in constructor
        /// </summary>
        public string WelcomeText
        {
            get
            {
                return _welcomeText;
            }
            set
            {
                _welcomeText = value;
            }
        }
        #endregion

        #region Constructor
        public FidelityViewModel()
        {
            NewCard = new CommandHandler(CreateCard, param => CanExecute);
            BackLogin = new CommandHandler(BackToLogin, param => CanExecute);
            ChangePhoto = new CommandHandler(ItemClick, param => CanExecute);

            DatabaseInteraction = new DataService();
            Cards = DatabaseInteraction.GetFidelityCards();
            if (Cards.Count > 0)
            {
                WelcomeText = "Alegeti cardul dorit!";
            }
            else
            {
                WelcomeText = "Nu aveti nici un card in aplicatie!";
            }
            foreach(CardFidelitate card in Cards)
            {
                CardImages.Add(card.FrontPath);
            }

            
        }


        #endregion

        #region Private Commands
        private void CreateCard(object obj)
        {
            PageManager.Instance.CurrentPage = new NewCardPage();
        }

        private void BackToLogin(object obj)
        {
            PageManager.Instance.CurrentPage = new LoginPage();
        }

        private void ItemClick(object obj)
        {
            string path = null;
            int index = 0;
            if (ClickItem != null)
            {
                foreach (var card in Cards)
                {
                    if (card.FrontPath == ClickItem.ToString())
                    {
                        path = card.BackPath;
                    }
                    else if (card.BackPath == ClickItem.ToString())
                    {
                        path = card.FrontPath;
                    }
                }

                foreach (var image in CardImages)
                {
                    if (image == ClickItem)
                    {
                        break;
                    }
                    index++;
                }
                CardImages[index] = path;
            }
        }

        #endregion

        #region Helpers




        #endregion
    }
}
