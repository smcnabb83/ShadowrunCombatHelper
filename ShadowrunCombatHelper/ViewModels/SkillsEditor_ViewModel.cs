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
    public class SkillsEditor_ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ItemChangeObservableCollection<Skill> _skillListToEdit = new ItemChangeObservableCollection<Skill>();

        public ItemChangeObservableCollection<Skill> SkillListToEdit
        {
            get { return _skillListToEdit; }
            set { _skillListToEdit = value;
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
    }
}
