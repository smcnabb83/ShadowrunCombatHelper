using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace ShadowrunCombatHelper.Views
{
    class IntArrayToColorBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if( value.GetType() == typeof(int[]) && (value as int[]).Length == 3)
            {
                int[] color = (int[])value;
                return new SolidColorBrush(Color.FromArgb((byte)255, (byte)color[0], (byte)color[1], (byte)color[2]));
            }
            else if (value.GetType() == typeof(int[]) && (value as int[]).Length == 4)
            {
                int[] color = (int[])value;
                return new SolidColorBrush(Color.FromArgb((byte)color[0], (byte)color[1], (byte)color[2], (byte)color[3]));
            }
            else
            {
                throw new ArgumentOutOfRangeException("Value must be a byte array of 3 or 4 values");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value.GetType() == typeof(SolidColorBrush))
            {
                SolidColorBrush color = (SolidColorBrush)value;
                return new int[] { color.Color.R, color.Color.G, color.Color.B };
            } else if (value.GetType() == typeof(string) && value.ToString().Length == 9)
            {
                string ret = value.ToString();
                return new int[] { System.Convert.ToInt32(ret.Substring(1, 2), 16), System.Convert.ToInt32(ret.Substring(3, 2),16), System.Convert.ToInt32(ret.Substring(5, 2), 16), System.Convert.ToInt32(ret.Substring(7, 2), 16) };
            }
            throw new ArgumentOutOfRangeException("value must be a solidcolorbrush");
        }
    }
}
