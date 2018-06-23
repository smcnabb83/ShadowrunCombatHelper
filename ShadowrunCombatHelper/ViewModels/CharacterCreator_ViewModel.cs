using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShadowrunCombatHelper.Globals;
using ShadowrunCombatHelper.Models;

namespace ShadowrunCombatHelper.ViewModels
{
    public class CharacterCreator_ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private List<Character> _characterList;

        public List<Character> CharacterList
        {
            get { return _characterList; }
            set { _characterList = value;
                NotifyPropertyChanged("CharacterList");
            }
        }

        private Character _selectedCharacter;

        public Character SelectedCharacter
        {
            get { return _selectedCharacter; }
            set { _selectedCharacter = value;
                NotifyPropertyChanged("SelectedCharacter");
            }
        }

        public void NotifyPropertyChanged(string property)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }



    }
}
