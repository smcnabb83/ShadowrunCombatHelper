using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ShadowrunCombatHelper.ViewModels;
using ShadowrunCombatHelper.Models;

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
            CharacterSelectionDialog_ViewModel this_model = (CharacterSelectionDialog_ViewModel)DataContext;
            Character combatant = (Character)AllCharactersList.SelectedItem;
            this_model.AddCharacterToCombatantsList(combatant);
        }

        private void RemoveFromCombatants_Click(object sender, RoutedEventArgs e)
        {
            CharacterSelectionDialog_ViewModel this_model = (CharacterSelectionDialog_ViewModel)DataContext;
            Character combatant = (Character)SelectedCombatantsList.SelectedItem;
            this_model.RemoveCharacterFromCombatantsList(combatant);
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
    }
}
