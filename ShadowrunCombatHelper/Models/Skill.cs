using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;
using ShadowrunCombatHelper.Globals;
using ShadowrunCombatHelper.Interfaces;

namespace ShadowrunCombatHelper.Models
{
    public class Skill : INotifyPropertyChanged, ICharacterBindable
    {
        public enum Attributes
        {
            AGI,
            BOD,
            CHA,
            EDGE,
            INT,
            LOG,
            REA,
            STR,
            WIL,
            MAG,
            ESS
        }

        private Character _assignedCharacter;

        private bool _defaultable;
        private string _description;
        private Attributes _limitBy;
        private ObservableCollection<Attributes> _relatedAttributes = new ObservableCollection<Attributes>();
        private string _skillName;
        private string _skillType;
        private int _trainingValue;

        public Skill(string skillName, int trainingValue, Character assigned, List<Attributes> attr, Attributes limit,
            string skillType = "Generic")
        {
            SkillName = skillName;
            _assignedCharacter = assigned;
            foreach (Attributes a in attr)
            {
                RelatedAttributes.Add(a);
            }

            LimitBy = limit;
            SkillType = skillType;
        }

        public Skill()
        {
            SkillName = "New Skill";
        }

        public int AdjustedTotalSkillValue =>
            (TotalSkillValue - _assignedCharacter?.CurrentDamagePenalty ?? 0).ClampLower(0);

        public Character AssignedCharacter
        {
            set => _assignedCharacter = value;
        }

        [XmlIgnore]
        public List<Attributes> AttributeList => Enum.GetValues(typeof(Attributes)).Cast<Attributes>().ToList();

        public int AttributeModifier
        {
            get
            {
                var totalValue = 0;
                foreach (Attributes attr in RelatedAttributes)
                {
                    totalValue += GetCharacterAttribute(attr);
                }

                return totalValue;
            }
        }

        public bool Defaultable
        {
            get => _defaultable;
            set
            {
                _defaultable = value;
                NotifyPropertyChanged(nameof(Defaultable));
                NotifyPropertyChanged(nameof(TotalSkillValue));
                NotifyPropertyChanged(nameof(AdjustedTotalSkillValue));
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                NotifyPropertyChanged(nameof(Description));
            }
        }

        public string GetRelatedAttributeString
        {
            get
            {
                var relAttrString = "";
                foreach (Attributes attribute in RelatedAttributes)
                {
                    relAttrString += attribute + ";";
                }

                relAttrString = relAttrString.TrimEnd(';');
                return relAttrString;
            }
        }

        public int Limit => GetCharacterAttribute(LimitBy);

        public Attributes LimitBy
        {
            get => _limitBy;
            set
            {
                _limitBy = value;
                NotifyPropertyChanged(nameof(LimitBy));
                NotifyPropertyChanged(nameof(Limit));
            }
        }

        public ObservableCollection<Attributes> RelatedAttributes
        {
            get => _relatedAttributes;
            set
            {
                _relatedAttributes = value;
                NotifyPropertyChanged(nameof(RelatedAttributes));
                NotifyPropertyChanged(nameof(TotalSkillValue));
                NotifyPropertyChanged(nameof(AttributeModifier));
                NotifyPropertyChanged(nameof(AdjustedTotalSkillValue));
            }
        }

        public string SkillName
        {
            get => _skillName;
            set
            {
                _skillName = value;
                NotifyPropertyChanged(nameof(SkillName));
            }
        }

        public string SkillType
        {
            get => _skillType;
            set
            {
                _skillType = value;
                NotifyPropertyChanged(nameof(SkillType));
            }
        }

        public int TotalSkillValue => Defaultable || TrainingValue > 0 ? TrainingValue + AttributeModifier : 0;

        public int TrainingValue
        {
            get => _trainingValue;
            set
            {
                _trainingValue = value;
                NotifyPropertyChanged(nameof(TrainingValue));
                NotifyPropertyChanged(nameof(TotalSkillValue));
                NotifyPropertyChanged(nameof(AdjustedTotalSkillValue));
            }
        }

        public void BindToCharacter(Character c)
        {
            AssignedCharacter = c;
        }

        public void CharacterPropertyChangedEventHandler(object c, PropertyChangedEventArgs e)
        {
            if (Enum.GetNames(typeof(Attributes)).Contains(e.PropertyName))
            {
                NotifyPropertyChanged(nameof(TotalSkillValue));
                NotifyPropertyChanged(nameof(AttributeModifier));
                NotifyPropertyChanged(nameof(Limit));
                NotifyPropertyChanged(nameof(AdjustedTotalSkillValue));
            }

            //TODO:Change this hard-coded value to something less brittle
            if (e.PropertyName == "CurrentDamagePenalty")
            {
                NotifyPropertyChanged(nameof(AdjustedTotalSkillValue));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public static Skill Clone(Skill oldSkill)
        {
            var clone = new Skill(oldSkill.SkillName, 0, null, oldSkill.RelatedAttributes.ToList(), oldSkill.LimitBy,
                oldSkill.SkillType);
            clone.Defaultable = oldSkill.Defaultable;
            return clone;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var compObj = (Skill) obj;
            return SkillName == compObj.SkillName;
        }

        public override int GetHashCode()
        {
            var hash = 23;
            hash = hash * 47 + SkillName.GetHashCode();
            return hash;
        }

        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public void UpdateProperties(Skill updateTo)
        {
            RelatedAttributes = updateTo.RelatedAttributes;
            LimitBy = updateTo.LimitBy;
            SkillType = updateTo.SkillType;
            Defaultable = updateTo.Defaultable;
            Description = updateTo.Description;
        }

        private int GetCharacterAttribute(Attributes a)
        {
            if (_assignedCharacter == null)
            {
                //TODO: Should I be asserting this is never null?
                return 0;
            }

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

                case Attributes.MAG:
                    return _assignedCharacter.MAGRES;

                case Attributes.ESS:
                    return _assignedCharacter.ESS;

                default:
                    return 0;
            }
        }
    }
}