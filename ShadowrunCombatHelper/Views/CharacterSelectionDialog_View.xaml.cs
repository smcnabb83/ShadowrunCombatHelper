using ShadowrunCombatHelper.Models;
using ShadowrunCombatHelper.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace ShadowrunCombatHelper.Views
{
    /// <summary>
    /// Interaction logic for CharacterSelectionDialog_View.xaml
    /// </summary>
    public partial class CharacterSelectionDialog_View : Window
    {
        public CharacterSelectionDialog_View()
        {
            InitializeComponent();
        }

        private List<Character> _returnedCombatants = new List<Character>();

        public List<Character> ReturnedCombatants
        {
            get
            {
                return _returnedCombatants;
            }
        }

        private void AddToCombatants_Click(object sender, RoutedEventArgs e)
        {
            AddCharacterToCombatants();
        }

        private void AddCharacterToCombatants()
        {
            CharacterSelectionDialog_ViewModel this_model = (CharacterSelectionDialog_ViewModel)DataContext;
            Character combatant = (Character)AllCharactersList.SelectedItem;
            if (combatant != null)
            {
                this_model.AddCharacterToCombatantsList(combatant);
            }
            AllCharactersList.Focus();
            if (AllCharactersList.Items.Count > 0)
            {
                AllCharactersList.SelectedIndex = 0;
            }
        }

        private void RemoveFromCombatants_Click(object sender, RoutedEventArgs e)
        {
            RemoveCharacterFromCombatants();
        }

        private void RemoveCharacterFromCombatants()
        {
            CharacterSelectionDialog_ViewModel this_model = (CharacterSelectionDialog_ViewModel)DataContext;
            Character combatant = (Character)SelectedCombatantsList.SelectedItem;
            if (combatant != null)
            {
                this_model.RemoveCharacterFromCombatantsList(combatant);
            }
            AllCharactersList.Focus();
            if (AllCharactersList.Items.Count > 0)
            {
                AllCharactersList.SelectedIndex = 0;
            }
        }

        private void ContinueToCombat_Click(object sender, RoutedEventArgs e)
        {
            CharacterSelectionDialog_ViewModel this_model = (CharacterSelectionDialog_ViewModel)DataContext;
            _returnedCombatants = this_model.CombatantsList.ToList();
            this.DialogResult = true;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void AllCharactersList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Right)
            {
                AddCharacterToCombatants();
                e.Handled = true;
            }
        }

        private void SelectedCombatantsList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                RemoveCharacterFromCombatants();
                e.Handled = true;
            }
        }
    }
}