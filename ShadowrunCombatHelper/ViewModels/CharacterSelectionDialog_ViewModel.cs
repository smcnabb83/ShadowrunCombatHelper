using ShadowrunCombatHelper.Models;
using ShadowrunCombatHelper.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ShadowrunCombatHelper.ViewModels
{
    public class CharacterSelectionDialog_ViewModel : INotifyPropertyChanged
    {
        private ItemChangeObservableCollection<Character> _allCharactersList = new ItemChangeObservableCollection<Character>();

        private ItemChangeObservableCollection<Character> _combatantsList = new ItemChangeObservableCollection<Character>();

        public CharacterSelectionDialog_ViewModel()
        {
            Globals.CharacterList clist = Globals.CharacterList.Instance;
            foreach (Character c in clist.GetCharacterList())
            {
                AllCharactersList.Add(c);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ItemChangeObservableCollection<Character> AllCharactersList
        {
            get { return _allCharactersList; }
            set
            {
                _allCharactersList = value;
                NotifyPropertyChanged("AllCharactersList");
            }
        }

        public ItemChangeObservableCollection<Character> CombatantsList
        {
            get { return _combatantsList; }
            set
            {
                _combatantsList = value;
                NotifyPropertyChanged("CombatantsList");
            }
        }

        public List<Character.CombatState> CombatStates
        {
            get
            {
                return Enum.GetValues(typeof(Character.CombatState)).Cast<Character.CombatState>().ToList();
            }
        }

        public void AddCharacterToCombatantsList(Character c)
        {
            AllCharactersList.Remove(c);
            CombatantsList.Add(c);
        }

        public void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public void RemoveCharacterFromCombatantsList(Character c)
        {
            CombatantsList.Remove(c);
            AllCharactersList.Add(c);
        }
    }
}