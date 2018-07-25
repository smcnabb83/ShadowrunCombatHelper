using ShadowrunCombatHelper.Models;
using ShadowrunCombatHelper.Objects;
using System.ComponentModel;
using System.Linq;

namespace ShadowrunCombatHelper.ViewModels
{
    public class SkillsEditor_ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ItemChangeObservableCollection<Skill> _skillListToEdit = new ItemChangeObservableCollection<Skill>();

        public ItemChangeObservableCollection<Skill> SkillListToEdit
        {
            get { return _skillListToEdit; }
            set
            {
                _skillListToEdit = value;
                NotifyPropertyChanged("SkillListToEdit");
            }
        }

        public void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public SkillsEditor_ViewModel()
        {
            foreach (var skill in Globals.SkillsList.Instance.Skills)
            {
                SkillListToEdit.Add(skill);
            }
        }

        public void WriteToGlobalSkillsList()
        {
            Globals.SkillsList.Instance.OverwriteSkills(SkillListToEdit.ToList());
            foreach (var c in Globals.CharacterList.Instance.GetCharacterList())
            {
                foreach (var d in Globals.SkillsList.Instance.Skills)
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
            Globals.CharacterList.Instance.ForceCharacterDataSave();
        }
    }
}