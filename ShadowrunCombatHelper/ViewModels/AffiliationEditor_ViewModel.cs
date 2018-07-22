using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShadowrunCombatHelper.Objects;
using ShadowrunCombatHelper.Models;
using ShadowrunCombatHelper.Globals;

namespace ShadowrunCombatHelper.ViewModels
{
    class AffiliationEditor_ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ItemChangeObservableCollection<Affiliation> _affiliationListToEdit = new ItemChangeObservableCollection<Affiliation>();

        public ItemChangeObservableCollection<Affiliation> AffiliationListToEdit
        {
            get
            {
                return _affiliationListToEdit;
            }
            set
            {
                _affiliationListToEdit = value;
                NotifyPropertyChanged("AffiliationListToEdit");
            }
        }

        public AffiliationEditor_ViewModel()
        {
            foreach(var affiliation in AffiliationList.Instance.Affiliations)
            {
                AffiliationListToEdit.Add(affiliation);
            }
        }

        public void WriteToGlobalAffiliationList()
        {
            AffiliationList.Instance.OverWriteAffiliations(AffiliationListToEdit.ToList());
            foreach(var c in CharacterList.Instance.GetCharacterList())
            {
                foreach(var d in AffiliationList.Instance.Affiliations)
                {
                    if(c.Affiliation.Equals(d))
                    {
                        c.Affiliation.BackgroundColor = d.BackgroundColor;
                        c.Affiliation.ForegroundColor = d.ForegroundColor;
                    }
                }
            }
        }

        public void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
