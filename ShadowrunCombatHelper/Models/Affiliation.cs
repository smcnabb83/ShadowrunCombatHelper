using ShadowrunCombatHelper.Globals;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace ShadowrunCombatHelper.Models
{
    public class Affiliation : INotifyPropertyChanged
    {
        private int[] _backgroundColor;

        private int[] _foregroundColor;

        private string _name;

        public Affiliation()
        {
            ForegroundColor = new[] {255, 255, 255, 255};
            BackgroundColor = new[] {128, 0, 0, 255};
            Name = "New Affiliation";
        }

        public int[] BackgroundColor
        {
            get => _backgroundColor;
            set
            {
                _backgroundColor = NormalizeColorValueArray(value, nameof(BackgroundColor));
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(BackgroundColorBrush));
            }
        }

        public int[] ForegroundColor
        {
            get => _foregroundColor;
            set
            {
                _foregroundColor = NormalizeColorValueArray(value, nameof(ForegroundColor));
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(ForegroundColorBrush));
            }
        }

        public Brush ForegroundColorBrush => new SolidColorBrush(Color.FromArgb((byte) _foregroundColor[0],
            (byte) _foregroundColor[1], (byte) _foregroundColor[2], (byte) _foregroundColor[3]));

        public Brush BackgroundColorBrush => new SolidColorBrush(Color.FromArgb((byte) _backgroundColor[0],
            (byte) _backgroundColor[1], (byte) _backgroundColor[2], (byte) _backgroundColor[3]));

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                NotifyPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private static int[] NormalizeColorValueArray(int[] value, string argumentName)
        {
            if (value == null) {
               throw new ArgumentNullException(nameof(value));
            }
            if(value.Length < 3 || value.Length > 4)
            {
                throw new ArgumentException($"{argumentName} must be a 3 or 4 element array");
            }

            if (value.Length == 3)
            {
                value = new[] {255, value[0], value[1], value[2]};
            }
             
            value[0] = value[0].Clamp(0,255);
            value[1] = value[1].Clamp(0,255);
            value[2] = value[2].Clamp(0,255);
            value[3] = value[3].Clamp(0,255);

            return value;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var compObj = (Affiliation) obj;
            return compObj.Name == Name;
        }

        public override int GetHashCode()
        {
            var hash = 17;
            hash = hash * 47 + Name.GetHashCode();
            return hash;
        }

        public void NotifyPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public override string ToString()
        {
            return Name;
        }
    }
}