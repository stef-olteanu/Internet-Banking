﻿using PCBanking;
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
    /// Interaction logic for CursValutarPage.xaml
    /// </summary>
    public partial class CursValutarPage : PageFunction<String>
    {
        public CursValutarPage()
        {
            InitializeComponent();
            DataContext = new CursViewModel();
            
          
        }
    }
}
