using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace PoEMarketLookup.Views.Controls
{
    public class CustomSlider : Slider
    {
        public enum Direction
        {
            Down,
            Up
        }

        public static readonly DependencyProperty ThumbDirectionProperty =
            DependencyProperty.Register("ThumbDirection", typeof(Direction), typeof(CustomSlider));

        public Direction ThumbDirection
        {
            get => (Direction)GetValue(ThumbDirectionProperty);
            set => SetValue(ThumbDirectionProperty, value);
        }

        static CustomSlider()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomSlider),
                new FrameworkPropertyMetadata(typeof(CustomSlider)));
        }
    }
}
