using System.ComponentModel;
using System.Runtime.CompilerServices;
using ShadowrunCombatHelper.Annotations;
using ShadowrunCombatHelper.Globals;

namespace ShadowrunCombatHelper.Models
{
    class Action : INotifyPropertyChanged
    {

        private string _name;

        private string _relatedSkillName;

        private string _description;

        private string _category;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                NotifyPropertyChanged();
            }
        }

        public string RelatedSkillName
        {
            get => _relatedSkillName;
            set
            {
                if (SkillsList.Instance.Skills.Exists(x => x.SkillName == value))
                {
                    _relatedSkillName = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                NotifyPropertyChanged();
            }
        }

        public string Category
        {
            get => _category;
            set
            {
                _category = value;
                NotifyPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
