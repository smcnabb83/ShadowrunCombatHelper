using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ShadowrunCombatHelper.Models
{
    public class MagicTradition : INotifyPropertyChanged
    {
        private List<Skill.Attributes> _resistDrainAttributes;

        private string _traditionName;

        public MagicTradition()
        {
        }

        public MagicTradition(string tName, List<Skill.Attributes> attr)
        {
            TraditionName = tName;
            ResistDrainAttributes = attr;
        }

        public List<Skill.Attributes> ResistDrainAttributes
        {
            get => _resistDrainAttributes;
            set
            {
                _resistDrainAttributes = value;
                NotifyPropertyChanged(nameof(ResistDrainAttributes));
                NotifyPropertyChanged(nameof(ResistDrainAttributesString));
            }
        }

        public string ResistDrainAttributesString
        {
            get
            {
                var attributeString = "";
                foreach (Skill.Attributes attr in ResistDrainAttributes)
                {
                    attributeString += attr + " + ";
                }

                return attributeString.TrimEnd(' ').TrimEnd('+').TrimEnd();
            }
        }

        public string TraditionName
        {
            get => _traditionName;
            set
            {
                _traditionName = value;
                NotifyPropertyChanged(nameof(TraditionName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var compObj = (MagicTradition) obj;
            return TraditionName == compObj.TraditionName &&
                   ResistDrainAttributes.SequenceEqual(compObj.ResistDrainAttributes);
        }

        public override int GetHashCode()
        {
            var hash = 191;
            hash = hash * 47 + TraditionName.GetHashCode();
            hash = hash * 47 + ResistDrainAttributes.GetHashCode();
            return hash;
        }

        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}