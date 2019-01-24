using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PCBanking
{
    /// <summary>
    /// Interaction logic for ListaPlatiRecurentePage.xaml
    /// </summary>
    public partial class ListaPlatiRecurentePage : Page
    {
        public ListaPlatiRecurentePage()
        {
            InitializeComponent();
        }

        private void PlataUtiliztor_Checked(object sender, RoutedEventArgs e)
        {
            PlataFurnizor.IsChecked = false;
        }

        private void PlataFurnizor_Checked(object sender, RoutedEventArgs e)
        {
            PlataUtiliztor.IsChecked = false;
        }
    }
}
