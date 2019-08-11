using System;
using System.Windows;
using System.Windows.Controls;

namespace PoEMarketLookup.Views.Controls
{
    /// <summary>
    /// Interaction logic for DoubleSlider.xaml
    /// </summary>
    public partial class DoubleSlider : UserControl
    {
        public static readonly DependencyProperty MinValueProperty =
            DependencyProperty.Register("MinValue", typeof(double), typeof(DoubleSlider));

        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register("MaxValue", typeof(double), typeof(DoubleSlider));

        

        private double _minimum;
        public double Minimum
        {
            get => _minimum;
            set
            {
                _minimum = value;
                MinValueSlider.Minimum = value;
                MaxValueSlider.Minimum = value;
            }
        }

        private double _maximum;
        public double Maximum
        {
            get => _maximum;
            set
            {
                _maximum = value;
                MinValueSlider.Maximum = value;
                MaxValueSlider.Maximum = value;
            }
        }
        
        public double MinValue
        {
            get => (double)GetValue(MinValueProperty);
            set => SetValue(MinValueProperty, value);
        }
        public double MaxValue
        {
            get => (double)GetValue(MaxValueProperty);
            set => SetValue(MaxValueProperty, value);
        }

        public DoubleSlider()
        {
            InitializeComponent();
        }

        private void MaxValueSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MaxValue = Math.Max(MaxValue, MinValue);
        }

        private void MinValueSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MinValue = Math.Min(MinValue, MaxValue);
        }
    }
}
