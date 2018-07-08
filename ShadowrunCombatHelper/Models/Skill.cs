﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ShadowrunCombatHelper.Interfaces;
using System;
using System.Linq;
using ShadowrunCombatHelper.Globals;

namespace ShadowrunCombatHelper.Models
{
    public class Skill : INotifyPropertyChanged, ICharacterBindable
    {
        private Character _assignedCharacter;

        private ObservableCollection<Attributes> _relatedAttributes = new ObservableCollection<Attributes>();

        private string _skillName;

        private int _trainingValue;

        public Skill(string skillName, int trainingValue, Character assigned, List<Attributes> attr, Attributes limit, string skillType = "Generic")
        {
            SkillName = skillName;
            _assignedCharacter = assigned;
            foreach (var a in attr)
            {
                RelatedAttributes.Add(a);
            }
            LimitBy = limit;
            SkillType = SkillType;
        }

        private Attributes _limitBy;

        public Attributes LimitBy
        {
            get { return _limitBy; }
            set { _limitBy = value;
                NotifyPropertyChanged("LimitBy");
                NotifyPropertyChanged("Limit");
            }
        }

        public int Limit
        {
            get
            {
                return GetCharacterAttribute(LimitBy);
            }
        }

        private String _skillType;

        public String SkillType
        {
            get { return _skillType; }
            set {
                _skillType = value;
                NotifyPropertyChanged("SkillType");
             }
        }



        public Skill()
        {
            SkillName = "New Skill";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public enum Attributes { AGI, BOD, CHA, EDGE, INT, LOG, REA, STR, WIL, MAG, ESS }
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
                NotifyPropertyChanged("AttributeModifier");
                NotifyPropertyChanged("AdjustedTotalSkillValue");
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
                NotifyPropertyChanged("AdjustedTotalSkillValue");
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
                return TrainingValue + AttributeModifier;
            }
        }

        public int AttributeModifier
        {
            get
            {
                int totalValue = 0;
                foreach(Attributes attr in RelatedAttributes)
                {
                    totalValue += GetCharacterAttribute(attr);
                }
                return totalValue;
            }
        }

        private int GetCharacterAttribute(Attributes a)
        {
            if(_assignedCharacter == null)
            {

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

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Skill compObj = (Skill)obj;
            return SkillName == compObj.SkillName;
        }

        public override int GetHashCode()
        {
            int hash = 23;
            hash = hash * 47 + SkillName.GetHashCode();
            return hash;
        }

        public void BindToCharacter(Character c)
        {
            AssignedCharacter = c;
        }

        public void CharacterPropertyChangedEventHandler(object c, PropertyChangedEventArgs e)
        {
            if (Enum.GetNames(typeof(Attributes)).Contains(e.PropertyName))
            {
                NotifyPropertyChanged("TotalSkillValue");
                NotifyPropertyChanged("AttributeModifier");
                NotifyPropertyChanged("Limit");
                NotifyPropertyChanged("AdjustedTotalSkillValue");
            }

            if (e.PropertyName == "CurrentDamagePenalty")
            {
                NotifyPropertyChanged("AdjustedTotalSkillValue");
            }

        }

        public List<Attributes> AttributeList
        {
            get
            {
                return Enum.GetValues(typeof(Attributes)).Cast<Attributes>().ToList();
            }
        }

        public int AdjustedTotalSkillValue
        {
            get
            {
                return (TotalSkillValue - _assignedCharacter?.CurrentDamagePenalty ?? 0).ClampLower(0);
            }
        }

        public string GetRelatedAttributeString
        {
         get
            {
                string relAttrString = "";
                foreach (var attribute in RelatedAttributes)
                {
                    relAttrString += attribute.ToString() + ";";
                }
                relAttrString = relAttrString.TrimEnd(';');
                return relAttrString;
            }                    
        }

        public void UpdateProperties(Skill updateTo)
        {
            this.RelatedAttributes = updateTo.RelatedAttributes;
            this.LimitBy = updateTo.LimitBy;
            this.SkillType = updateTo.SkillType;
        }
    }
}