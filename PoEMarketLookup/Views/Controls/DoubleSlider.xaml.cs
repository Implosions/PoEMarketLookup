using System.Windows.Controls;

namespace PoEMarketLookup.Views.Controls
{
    /// <summary>
    /// Interaction logic for DoubleSlider.xaml
    /// </summary>
    public partial class DoubleSlider : UserControl
    {
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
            get => MinValueSlider.Value;
            set
            {
                MinValueSlider.Value = value;
            }
        }
        public double MaxValue
        {
            get => MaxValueSlider.Value;
            set
            {
                MaxValueSlider.Value = value;
            }
        }

        public DoubleSlider()
        {
            InitializeComponent();
        }

        private void MinValueSlider_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            if(MinValue > MaxValue)
            {
                MinValue = MaxValue;
            }
        }

        private void MaxValueSlider_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            if(MaxValue < MinValue)
            {
                MaxValue = MinValue;
            }
        }
    }
}
