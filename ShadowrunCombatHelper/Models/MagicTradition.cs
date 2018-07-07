﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShadowrunCombatHelper.Models;

namespace ShadowrunCombatHelper.Models
{
    public class MagicTradition : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        private string _traditionName;

        public string TraditionName
        {
            get { return _traditionName; }
            set { _traditionName = value;
                NotifyPropertyChanged("TraditionName");
            }
        }

        public MagicTradition()
        {

        }

        public MagicTradition(string tName, List<Skill.Attributes> attr)
        {
            TraditionName = tName;
            ResistDrainAttributes = attr;
        }

        private List<Skill.Attributes> _resistDrainAttributes;

        public List<Skill.Attributes> ResistDrainAttributes
        {
            get { return _resistDrainAttributes; }
            set { _resistDrainAttributes = value;
                NotifyPropertyChanged("ResistDrainAttributes");
                NotifyPropertyChanged("ResistDrainAttributesString");
            }
        }

        public string ResistDrainAttributesString
        {
            get
            {
                string attributeString = "";
                foreach(var attr in ResistDrainAttributes)
                {
                    attributeString += attr.ToString() + " + ";
                }

                return attributeString.TrimEnd(' ').TrimEnd('+');

            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            MagicTradition compObj = (MagicTradition)obj;
            return TraditionName == compObj.TraditionName && ResistDrainAttributes.SequenceEqual(compObj.ResistDrainAttributes);
        }

        public override int GetHashCode()
        {
            int hash = 191;
            hash = hash * 47 + TraditionName.GetHashCode();
            hash = hash * 47 + ResistDrainAttributes.GetHashCode();
            return hash;

        }
    }
}