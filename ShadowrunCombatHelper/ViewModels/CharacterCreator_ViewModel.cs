using ShadowrunCombatHelper.Globals;
using ShadowrunCombatHelper.Models;
using ShadowrunCombatHelper.Objects;
using System.ComponentModel;
using System.Linq;

namespace ShadowrunCombatHelper.ViewModels
{
    public class CharacterCreator_ViewModel : INotifyPropertyChanged
    {
        private ItemChangeObservableCollection<Character> _characterList = new ItemChangeObservableCollection<Character>();

        public CharacterCreator_ViewModel()
        {
            CharacterList clist = Globals.CharacterList.Instance;
            foreach (Character c in clist.GetCharacterList())
            {
                foreach (var skill in Globals.SkillsList.Instance.Skills)
                {
                    if (!c.Skills.Contains(skill))
                    {
                        c.Skills.Add(skill);
                    }
                }
                CharacterList.Add(c);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public ItemChangeObservableCollection<Character> CharacterList
        {
            get { return _characterList; }
            set
            {
                _characterList = value;
                NotifyPropertyChanged("CharacterList");
            }
        }
        public void CreateNewCharacter()
        {
            Character newCharacter = new Character();
            foreach (var skill in Globals.SkillsList.Instance.Skills)
            {
                newCharacter.Skills.Add(skill);
            }
            newCharacter.CharacterName = "New Character";
            CharacterList.Add(newCharacter);
        }

        public void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public void RemoveCharacterFromList(Character C)
        {
            CharacterList.Remove(C);
        }

        public void SaveCharactersToCharacterList()
        {
            CharacterList clist = Globals.CharacterList.Instance;
            clist.OverwriteCharacterList(CharacterList.ToList());
        }
    }
}