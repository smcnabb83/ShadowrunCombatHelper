using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ShadowrunCombatHelper.Models;
using ShadowrunCombatHelper.ViewModels;

namespace ShadowrunCombatHelper.Views
{
    /// <summary>
    ///     Interaction logic for CharacterSelectionDialog_View.xaml
    /// </summary>
    public partial class CharacterSelectionDialog_View : Window
    {
        public CharacterSelectionDialog_View()
        {
            InitializeComponent();
        }

        public List<Character> ReturnedCombatants { get; private set; } = new List<Character>();

        private void AddToCombatants_Click(object sender, RoutedEventArgs e)
        {
            AddCharacterToCombatants();
        }

        private void AddCharacterToCombatants()
        {
            var this_model = (CharacterSelectionDialog_ViewModel) DataContext;
            var combatant = (Character) AllCharactersList.SelectedItem;
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
            var this_model = (CharacterSelectionDialog_ViewModel) DataContext;
            var combatant = (Character) SelectedCombatantsList.SelectedItem;
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
            var this_model = (CharacterSelectionDialog_ViewModel) DataContext;
            ReturnedCombatants = this_model.CombatantsList.ToList();
            DialogResult = true;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
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