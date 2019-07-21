﻿using System;
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

namespace PoEMarketLookup.Views
{
    /// <summary>
    /// Interaction logic for ItemFieldView.xaml
    /// </summary>
    public partial class ItemFieldView : UserControl
    {
        public ItemFieldView()
        {
            InitializeComponent();
        }

        private void ItemField_Click(object sender, RoutedEventArgs e)
        {
            chk_Selected.IsChecked = !chk_Selected.IsChecked;
        }
    }
}
