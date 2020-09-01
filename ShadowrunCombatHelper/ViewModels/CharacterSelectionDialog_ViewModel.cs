using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using ShadowrunCombatHelper.Globals;
using ShadowrunCombatHelper.Models;
using ShadowrunCombatHelper.Objects;

namespace ShadowrunCombatHelper.ViewModels
{
    public class CharacterSelectionDialog_ViewModel : INotifyPropertyChanged
    {
        private ItemChangeObservableCollection<Character> _allCharactersList =
            new ItemChangeObservableCollection<Character>();

        private ItemChangeObservableCollection<Character> _combatantsList =
            new ItemChangeObservableCollection<Character>();

        public CharacterSelectionDialog_ViewModel()
        {
            var clist = CharacterList.Instance;
            foreach (Character c in clist.GetCharacterList())
            {
                AllCharactersList.Add(c);
            }
        }

        public ItemChangeObservableCollection<Character> AllCharactersList
        {
            get => _allCharactersList;
            set
            {
                _allCharactersList = value;
                NotifyPropertyChanged(nameof(AllCharactersList));
            }
        }

        public ItemChangeObservableCollection<Character> CombatantsList
        {
            get => _combatantsList;
            set
            {
                _combatantsList = value;
                NotifyPropertyChanged(nameof(CombatantsList));
            }
        }

        public List<Character.CombatState> CombatStates =>
            Enum.GetValues(typeof(Character.CombatState)).Cast<Character.CombatState>().ToList();

        public event PropertyChangedEventHandler PropertyChanged;

        public void AddCharacterToCombatantsList(Character c)
        {
            AllCharactersList.Remove(c);
            CombatantsList.Add(c);
        }

        public void NotifyPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public void RemoveCharacterFromCombatantsList(Character c)
        {
            CombatantsList.Remove(c);
            AllCharactersList.Add(c);
        }
    }
}