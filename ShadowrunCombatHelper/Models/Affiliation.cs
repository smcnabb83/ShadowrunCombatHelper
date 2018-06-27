using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xml.Serialization;

namespace ShadowrunCombatHelper.Models
{
    public class Affiliation : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value;
                NotifyPropertyChanged("Name");
            }
        }



        private int [] _backgroundColor;

        public int[] BackgroundColor
        {
            get
            {
                return _backgroundColor;
            }
            set
            {
                if(value.Length != 3)
                {
                    throw new ArgumentException("BackgroundColor must be a 3 element array");
                }
                if(value[0] < 0 || value[0] > 255 || value[1] < 0 || value[1] > 255 || value[2] < 0 || value[2] > 255)
                {
                    throw new ArgumentException("All elements passed to BackgroundColor must be between 0 and 255");
                }
                _backgroundColor = value;
                NotifyPropertyChanged("ForegroundColor");

            }
        }

        public Brush BackgroundColorBrush
        {
            get { return new SolidColorBrush(Color.FromArgb(255, (byte)_backgroundColor[0], (byte)_backgroundColor[1], (byte)_backgroundColor[2])); }

        }

        private int[] _foregroundColor;

        public int[] ForegroundColor
        {
            get
            {
                return _foregroundColor;
            }
            set
            {
                if (value.Length != 3)
                {
                    throw new ArgumentException("BackgroundColor must be a 3 element array");
                }
                if (value[0] < 0 || value[0] > 255 || value[1] < 0 || value[1] > 255 || value[2] < 0 || value[2] > 255)
                {
                    throw new ArgumentException("All elements passed to BackgroundColor must be between 0 and 255");
                }
                _foregroundColor = value;
                NotifyPropertyChanged("ForegroundColor");

            }
        }

        public Brush ForegroundColorBrush
        {
            get { return new SolidColorBrush(Color.FromArgb(255, (byte)_foregroundColor[0], (byte)_foregroundColor[1], (byte)_foregroundColor[2])); }

        }

        public Affiliation()
        {
            ForegroundColor = new int[] { 255, 255, 255 };
            BackgroundColor = new int[] { 0, 0, 255 };
            Name = "New Affiliation";
        }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            if(obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Affiliation compObj = (Affiliation)obj;
            return (compObj.Name == Name && compObj._foregroundColor.SequenceEqual(_foregroundColor) && compObj._backgroundColor.SequenceEqual(_backgroundColor));
        }

        public override int GetHashCode()
        {
            int hash = 17 ;
            hash = hash * 47 + Name.GetHashCode();
            hash = hash * 47 + _backgroundColor.GetHashCode();
            hash = hash * 47 + _foregroundColor.GetHashCode();
            return hash;

        }




    }
}
