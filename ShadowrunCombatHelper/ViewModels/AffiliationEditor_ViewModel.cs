using System.ComponentModel;
using System.Linq;
using ShadowrunCombatHelper.Globals;
using ShadowrunCombatHelper.Models;
using ShadowrunCombatHelper.Objects;

namespace ShadowrunCombatHelper.ViewModels
{
    internal class AffiliationEditor_ViewModel : INotifyPropertyChanged
    {
        private ItemChangeObservableCollection<Affiliation> _affiliationListToEdit =
            new ItemChangeObservableCollection<Affiliation>();

        public AffiliationEditor_ViewModel()
        {
            foreach (Affiliation affiliation in AffiliationList.Instance.Affiliations)
            {
                AffiliationListToEdit.Add(affiliation);
            }
        }

        public ItemChangeObservableCollection<Affiliation> AffiliationListToEdit
        {
            get => _affiliationListToEdit;
            set
            {
                _affiliationListToEdit = value;
                NotifyPropertyChanged(nameof(AffiliationListToEdit));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void WriteToGlobalAffiliationList()
        {
            AffiliationList.Instance.OverWriteAffiliations(AffiliationListToEdit.ToList());
            foreach (Character c in CharacterList.Instance.GetCharacterList())
            {
                foreach (Affiliation d in AffiliationList.Instance.Affiliations)
                {
                    if (c?.Affiliation?.Equals(d) ?? false)
                    {
                        c.Affiliation.BackgroundColor = d.BackgroundColor;
                        c.Affiliation.ForegroundColor = d.ForegroundColor;
                    }
                }
            }

            CharacterList.Instance.ForceCharacterDataSave();
        }

        public void NotifyPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}