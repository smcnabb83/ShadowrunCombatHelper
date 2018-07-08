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
                Characters.Add(c);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public ItemChangeObservableCollection<Character> Characters
        {
            get { return _characterList; }
            set
            {
                _characterList = value;
                NotifyPropertyChanged("Characters");
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
            Characters.Add(newCharacter);
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
            Characters.Remove(C);
        }

        public void SaveCharactersToCharacterList()
        {
            CharacterList clist = Globals.CharacterList.Instance;
            clist.OverwriteCharacterList(Characters.ToList());
        }
    }
}