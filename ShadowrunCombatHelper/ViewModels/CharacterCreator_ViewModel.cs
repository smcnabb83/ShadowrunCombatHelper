using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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

        private ItemChangeObservableCollection<Character> _characterList = new ItemChangeObservableCollection<Character>();

        public ItemChangeObservableCollection<Character> CharacterList
        {
            get { return _characterList; }
            set { _characterList = value;
                NotifyPropertyChanged("CharacterList");
            }
        }

        public CharacterCreator_ViewModel()
        {
            CharacterList clist = Globals.CharacterList.Instance;
            foreach(Character c in clist.GetCharacterList())
            {
                foreach(var skill in Globals.SkillsList.Instance.Skills)
                {
                    if (!c.Skills.Contains(skill))
                    {
                        c.Skills.Add(skill);
                    }
                }
                CharacterList.Add(c);
                
            }
        }
        
        public void SaveCharactersToCharacterList()
        {
            CharacterList clist = Globals.CharacterList.Instance;
            clist.OverwriteCharacterList(CharacterList.ToList());
        }

        public void CreateNewCharacter()
        {
            Character newCharacter = new Character();
            newCharacter.CharacterName = "New Character";
            CharacterList.Add(newCharacter);
        }

        public void RemoveCharacterFromList(Character C)
        {
            CharacterList.Remove(C);
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
