using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ShadowrunCombatHelper.Models
{
    class Affiliation : INotifyPropertyChanged
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

        private Brush _backgroundColor;

        public Brush BackgroundColor
        {
            get { return _backgroundColor; }
            set { _backgroundColor = value;
                NotifyPropertyChanged("BackgroundColor");
            }
        }

        private Brush _foregroundColor;

        public Brush ForegroundColor
        {
            get { return _foregroundColor; }
            set { _foregroundColor = value;
                NotifyPropertyChanged("ForegroundColor");
            }
        }

        public Affiliation()
        {
            ForegroundColor = Brushes.Black;
            BackgroundColor = Brushes.Blue;
            Name = "New Affiliation";
        }




    }
}
