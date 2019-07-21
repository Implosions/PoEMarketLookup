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

namespace PoEMarketLookup.Views
{
    public partial class ItemView : UserControl
    {
        public ItemView()
        {
            InitializeComponent();
        }

        private void Control_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            switch (sender)
            {
                case TextBlock tb:
                    tb.Visibility = string.IsNullOrEmpty(tb.Text) ? Visibility.Collapsed : Visibility.Visible;
                    break;
                case ItemsControl lb:
                    lb.Visibility = lb.Items.Count == 0 ? Visibility.Collapsed : Visibility.Visible;
                    break;
                case ContentControl cc:
                    cc.Visibility = cc.Content == null ? Visibility.Collapsed : Visibility.Visible;
                    break;
            }
        }
    }
}
