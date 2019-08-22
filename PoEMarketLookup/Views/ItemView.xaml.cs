using PoEMarketLookup.PoE.Items.Components;
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
        public static readonly DependencyProperty ItemRarityProperty =
            DependencyProperty.Register("ItemRarity", typeof(Rarity), typeof(ItemView));

        public static readonly DependencyProperty ItemTypeProperty =
            DependencyProperty.Register("ItemType", typeof(PoEItemType), typeof(ItemView));

        public static readonly DependencyProperty ItemColorProperty =
            DependencyProperty.Register("ItemColor", typeof(SolidColorBrush), typeof(ItemView));

        public Rarity ItemRarity
        {
            get => (Rarity)GetValue(ItemRarityProperty);
            set => SetValue(ItemRarityProperty, value);
        }

        public PoEItemType ItemType
        {
            get => (PoEItemType)GetValue(ItemTypeProperty);
            set => SetValue(ItemTypeProperty, value);
        }

        public SolidColorBrush ItemColor
        {
            get => (SolidColorBrush)GetValue(ItemColorProperty);
            set => SetValue(ItemColorProperty, value);
        }

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
