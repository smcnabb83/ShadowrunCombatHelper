using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ShadowrunCombatHelper.Views
{
    internal class IntArrayToColorBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value?.GetType() == typeof(int[]) && ((int[]) value).Length == 3)
            {
                var color = (int[]) value;
                return new SolidColorBrush(Color.FromArgb(255, (byte) color[0], (byte) color[1], (byte) color[2]));
            }

            if (value?.GetType() == typeof(int[]) && ((int[]) value).Length == 4)
            {
                var color = (int[]) value;
                return new SolidColorBrush(Color.FromArgb((byte) color[0], (byte) color[1], (byte) color[2],
                    (byte) color[3]));
            }

            throw new ArgumentOutOfRangeException("Value must be a byte array of 3 or 4 values");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value?.GetType() == typeof(SolidColorBrush))
            {
                var color = (SolidColorBrush) value;
                return new int[] {color.Color.A, color.Color.R, color.Color.G, color.Color.B};
            }

            switch (value)
            {
                case string _ when value.ToString().Length == 9:
                {
                    var ret = value.ToString();
                    return new[]
                    {
                        System.Convert.ToInt32(ret.Substring(1, 2), 16), System.Convert.ToInt32(ret.Substring(3, 2), 16),
                        System.Convert.ToInt32(ret.Substring(5, 2), 16), System.Convert.ToInt32(ret.Substring(7, 2), 16)
                    };
                }
                case Color col:
                    return new int[] {col.A, col.R, col.G, col.B};
                default:
                    throw new ArgumentOutOfRangeException("value must be a solidcolorbrush");
            }
        }
    }
}