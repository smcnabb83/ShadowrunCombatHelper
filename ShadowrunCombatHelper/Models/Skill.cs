using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ShadowrunCombatHelper.Models
{
    internal class Skill : INotifyPropertyChanged
    {
        private Character _assignedCharacter;

        private ObservableCollection<Attributes> _relatedAttributes;

        private string _skillName;

        private int _trainingValue;

        public Skill(string skillName, int trainingValue, Character assigned, List<Attributes> attr)
        {
            SkillName = skillName;
            _assignedCharacter = assigned;
            foreach (var a in attr)
            {
                RelatedAttributes.Add(a);
            }
        }

        public Skill()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public enum Attributes { AGI, BOD, CHA, EDGE, INT, LOG, REA, STR, WIL }
        public Character AssignedCharacter
        {
            set
            {
                _assignedCharacter = value;
            }
        }

        public ObservableCollection<Attributes> RelatedAttributes
        {
            get { return _relatedAttributes; }
            set
            {
                _relatedAttributes = value;
                NotifyPropertyChanged("RelatedAttributes");
                NotifyPropertyChanged("TotalSkillValue");
            }
        }

        public string SkillName
        {
            get { return _skillName; }
            set
            {
                _skillName = value;
                NotifyPropertyChanged("SkillName");
            }
        }

        public int TrainingValue
        {
            get { return _trainingValue; }
            set
            {
                _trainingValue = value;
                NotifyPropertyChanged("TrainingValue");
                NotifyPropertyChanged("TotalSkillValue");
            }
        }

        public void NotifyPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        public int TotalSkillValue
        {
            get
            {
                int totValue = TrainingValue;
                if(_assignedCharacter != null)
                {
                    foreach(Attributes attr in RelatedAttributes)
                    {
                        totValue += GetCharacterAttribute(attr);
                    }
                }
                return totValue;
            }
        }

        private int GetCharacterAttribute(Attributes a)
        {
            switch (a)
            {
                case Attributes.AGI:
                    return _assignedCharacter.AGI;
                case Attributes.BOD:
                    return _assignedCharacter.BOD;
                case Attributes.CHA:
                    return _assignedCharacter.CHA;
                case Attributes.EDGE:
                    return _assignedCharacter.EDGE;
                case Attributes.INT:
                    return _assignedCharacter.INTU;
                case Attributes.LOG:
                    return _assignedCharacter.LOG;
                case Attributes.REA:
                    return _assignedCharacter.REA;
                case Attributes.STR:
                    return _assignedCharacter.STR;
                case Attributes.WIL:
                    return _assignedCharacter.WIL;
                default:
                    return 0;
            }
        }
    }
}