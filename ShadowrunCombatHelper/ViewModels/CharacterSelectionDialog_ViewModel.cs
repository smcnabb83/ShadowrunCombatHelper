﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShadowrunCombatHelper.Models;
using ShadowrunCombatHelper.Globals;

namespace ShadowrunCombatHelper.ViewModels
{
    public class CharacterSelectionDialog_ViewModel : INotifyPropertyChanged
    {
        private ItemChangeObservableCollection<Character> _allCharactersList = new ItemChangeObservableCollection<Character>();

        public ItemChangeObservableCollection<Character> AllCharactersList
        {
            get { return _allCharactersList; }
            set { _allCharactersList = value;
                NotifyPropertyChanged("AllCharactersList");
            }
        }

        public CharacterSelectionDialog_ViewModel()
        {
            Globals.CharacterList clist = Globals.CharacterList.Instance;
            foreach(Character c in clist.GetCharacterList())
            {
                AllCharactersList.Add(c);
            }
        }

        private ItemChangeObservableCollection<Character> _combatantsList = new ItemChangeObservableCollection<Character>();

        public event PropertyChangedEventHandler PropertyChanged;

        public ItemChangeObservableCollection<Character> CombatantsList
        {
            get { return _combatantsList; }
            set { _combatantsList = value;
                NotifyPropertyChanged("CombatantsList");
            }
        }

        public void AddCharacterToCombatantsList(Character c)
        {
            CombatantsList.Add(c);
        }

        public void RemoveCharacterFromCombatantsList(Character c)
        {
            CombatantsList.Remove(c);
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
