using System;
using System.ComponentModel;
using System.Linq;
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
            ForegroundColor = new int[] { 255, 255, 255 };
            BackgroundColor = new int[] { 0, 0, 255 };
            Name = "New Affiliation";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public int[] BackgroundColor
        {
            get
            {
                return _backgroundColor;
            }
            set
            {
                
                _backgroundColor = ValidateColorValueArray(value, "BackgroundColor");
                NotifyPropertyChanged("BackgroundColor");
            }
        }


        public int[] ForegroundColor
        {
            get
            {
                return _foregroundColor;
            }
            set
            {                
                _foregroundColor = ValidateColorValueArray(value, "ForegroundColor");
                NotifyPropertyChanged("ForegroundColor");
            }
        }

        private static int[] ValidateColorValueArray(int[] value, string argumentName)
        {
            if (value.Length != 3)
            {
                throw new ArgumentException($"{argumentName} must be a 3 element array");
            }
            if (value[0] < 0 || value[0] > 255 || value[1] < 0 || value[1] > 255 || value[2] < 0 || value[2] > 255)
            {
                throw new ArgumentException($"All elements passed to {argumentName} must be between 0 and 255");
            }
            return value;
        }

        public Brush ForegroundColorBrush
        {
            get { return new SolidColorBrush(Color.FromArgb(255, (byte)_foregroundColor[0], (byte)_foregroundColor[1], (byte)_foregroundColor[2])); }
        }

        public Brush BackgroundColorBrush
        {
            get { return new SolidColorBrush(Color.FromArgb(128, (byte)_backgroundColor[0], (byte)_backgroundColor[1], (byte)_backgroundColor[2])); }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyPropertyChanged("Name");
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Affiliation compObj = (Affiliation)obj;
            return (compObj.Name == Name && compObj._foregroundColor.SequenceEqual(_foregroundColor) && compObj._backgroundColor.SequenceEqual(_backgroundColor));
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 47 + Name.GetHashCode();
            hash = hash * 47 + _backgroundColor.GetHashCode();
            hash = hash * 47 + _foregroundColor.GetHashCode();
            return hash;
        }

        public void NotifyPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
        public override string ToString()
        {
            return Name;
        }
    }
}