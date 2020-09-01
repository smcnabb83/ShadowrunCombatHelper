using System.ComponentModel;
using System.Linq;
using ShadowrunCombatHelper.Globals;
using ShadowrunCombatHelper.Models;
using ShadowrunCombatHelper.Objects;

namespace ShadowrunCombatHelper.ViewModels
{
    public class CharacterCreator_ViewModel : INotifyPropertyChanged
    {
        private ItemChangeObservableCollection<Character> _characterList =
            new ItemChangeObservableCollection<Character>();

        public CharacterCreator_ViewModel()
        {
            var clist = CharacterList.Instance;
            foreach (Character c in clist.GetCharacterList())
            {
                foreach (Skill skill in SkillsList.Instance.Skills)
                {
                    if (!c.Skills.Contains(skill))
                    {
                        c.Skills.Add(skill);
                    }
                }

                Characters.Add(c);
            }
        }

        public ItemChangeObservableCollection<Character> Characters
        {
            get => _characterList;
            set
            {
                _characterList = value;
                NotifyPropertyChanged(nameof(Characters));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void CreateNewCharacter(string charName)
        {
            var newCharacterIndexer = 0;
            var newCharacter = new Character();
            foreach (Skill skill in SkillsList.Instance.Skills)
            {
                newCharacter.Skills.Add(skill);
            }

            newCharacter.CharacterName = charName;

            while (Characters.Contains(newCharacter))
            {
                newCharacterIndexer++;
                newCharacter.CharacterName = $"{charName} {newCharacterIndexer}";
            }

            Characters.Add(newCharacter);
        }

        public void NotifyPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public void RemoveCharacterFromList(Character C)
        {
            Characters.Remove(C);
        }

        public void SaveCharactersToCharacterList()
        {
            var clist = CharacterList.Instance;
            clist.OverwriteCharacterList(Characters.ToList());
        }
    }
}