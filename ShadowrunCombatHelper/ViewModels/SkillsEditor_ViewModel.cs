using System.ComponentModel;
using System.Linq;
using ShadowrunCombatHelper.Globals;
using ShadowrunCombatHelper.Models;
using ShadowrunCombatHelper.Objects;

namespace ShadowrunCombatHelper.ViewModels
{
    public class SkillsEditor_ViewModel : INotifyPropertyChanged
    {
        private ItemChangeObservableCollection<Skill> _skillListToEdit = new ItemChangeObservableCollection<Skill>();

        public SkillsEditor_ViewModel()
        {
            foreach (Skill skill in SkillsList.Instance.Skills)
            {
                SkillListToEdit.Add(skill);
            }
        }

        public ItemChangeObservableCollection<Skill> SkillListToEdit
        {
            get => _skillListToEdit;
            set
            {
                _skillListToEdit = value;
                NotifyPropertyChanged(nameof(SkillListToEdit));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public void WriteToGlobalSkillsList()
        {
            SkillsList.Instance.OverwriteSkills(SkillListToEdit.ToList());
            foreach (Character c in CharacterList.Instance.GetCharacterList())
            {
                foreach (Skill d in SkillsList.Instance.Skills)
                {
                    int index = c.Skills.IndexOf(d);
                    if (index >= 0)
                    {
                        c.Skills[index].UpdateProperties(d);
                    }
                    else
                    {
                        c.Skills.Add(Skill.Clone(d));
                    }
                }
            }

            CharacterList.Instance.ForceCharacterDataSave();
        }
    }
}