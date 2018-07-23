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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ShadowrunCombatHelper.ViewModels;
using ShadowrunCombatHelper.Models;
using ShadowrunCombatHelper.UserControls;

namespace ShadowrunCombatHelper.Views
{
    /// <summary>
    /// Interaction logic for InitiativeTracker_View.xaml
    /// </summary>
    public partial class InitiativeTracker_View : Page
    {
        bool enteredCombatSuccess = false;

        public InitiativeTracker_View()
        {
            InitializeComponent();
            CombatStates.ItemsSource = Enum.GetValues(typeof(Character.CombatState)).Cast<Character.CombatState>();
            CharacterSelectionDialog_View selectDialog = new CharacterSelectionDialog_View();
            bool? result = selectDialog.ShowDialog();
            if(result ?? false)
            {
                enteredCombatSuccess = true;
                InitiativeTracker_ViewModel model = new InitiativeTracker_ViewModel();
                DataContext = model;
                model.AddCombatants(selectDialog.ReturnedCombatants);
            }
            else
            {
                this.Visibility = Visibility.Hidden;
                enteredCombatSuccess = false;
            }
        }

        private void mnuEndRound_Click(object sender, RoutedEventArgs e)
        {
            Character BoundCharacter = ((this.DataContext) as InitiativeTracker_ViewModel).CurrentCharacter;
            BoundCharacter.EndTurn();
        }

        private void NavigateBackOnCombatEntryFail()
        {
            NavigationService.Navigate(new BlankPage(), MainWindow.allowBack.DISALLOWBACK);
        }

        private void btnDealOnePhysicalDamage_Click(object sender, RoutedEventArgs e)
        {
            Character BoundCharacter = ((this.DataContext) as InitiativeTracker_ViewModel).CurrentCharacter;
            BoundCharacter.CurrentPhysicalDamage++;
        }

        private void btnDealDamage_Click(object sender, RoutedEventArgs e)
        {
            Character BoundCharacter = ((this.DataContext) as InitiativeTracker_ViewModel).CurrentCharacter;
            DealDamageDialog dealDamage = new DealDamageDialog(BoundCharacter);
            bool? result = dealDamage.ShowDialog();
            if (result ?? false)
            {
                if (dealDamage.PhysicalDamage)
                {
                    BoundCharacter.CurrentPhysicalDamage += dealDamage.DamageDealt;
                }
                else
                {
                    BoundCharacter.CurrentStunDamage += dealDamage.DamageDealt;
                }
            }
        }

        private void mnuFullDefense_Click(object sender, RoutedEventArgs e)
        {
            Character BoundCharacter = ((this.DataContext) as InitiativeTracker_ViewModel).CurrentCharacter;
            BoundCharacter.FullDefense();
        }

        private void mnuBlock_Click(object sender, RoutedEventArgs e)
        {
            Character BoundCharacter = ((this.DataContext) as InitiativeTracker_ViewModel).CurrentCharacter;
            BoundCharacter.Block();
        }

        private void mnuDodge_Click(object sender, RoutedEventArgs e)
        {
            Character BoundCharacter = ((this.DataContext) as InitiativeTracker_ViewModel).CurrentCharacter;
            BoundCharacter.Dodge();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Character BoundCharacter = ((this.DataContext) as InitiativeTracker_ViewModel).CurrentCharacter;
            BoundCharacter.HitTheDirt();
        }

        private void mnuIntercept_Click(object sender, RoutedEventArgs e)
        {
            Character BoundCharacter = ((this.DataContext) as InitiativeTracker_ViewModel).CurrentCharacter;
            BoundCharacter.Intercept();
        }

        private void mnuParry_Click(object sender, RoutedEventArgs e)
        {
            Character BoundCharacter = ((this.DataContext) as InitiativeTracker_ViewModel).CurrentCharacter;
            BoundCharacter.Parry();
        }

        private void mnuRunningToggle_Click(object sender, RoutedEventArgs e)
        {
            Character BoundCharacter = ((this.DataContext) as InitiativeTracker_ViewModel).CurrentCharacter;
            BoundCharacter.ToggleRunning();
        }

        private void mnuSimpleAction_Click(object sender, RoutedEventArgs e)
        {
            Character BoundCharacter = ((this.DataContext) as InitiativeTracker_ViewModel).CurrentCharacter;
            BoundCharacter.ConsumeSimpleAction();
        }

        private void mnuComplexAction_Click(object sender, RoutedEventArgs e)
        {
            Character BoundCharacter = ((this.DataContext) as InitiativeTracker_ViewModel).CurrentCharacter;
            BoundCharacter.ConsumeComplexAction();
        }

        private void mnuFreeAction_Click(object sender, RoutedEventArgs e)
        {
            Character BoundCharacter = ((this.DataContext) as InitiativeTracker_ViewModel).CurrentCharacter;
            BoundCharacter.ConsumeFreeAction();
        }

        private void mnuMoveCharacter_Click(object sender, RoutedEventArgs e)
        {
            Character BoundCharacter = ((this.DataContext) as InitiativeTracker_ViewModel).CurrentCharacter;
            int amountMoved = GetInputDialog<int>.Show($"Input {BoundCharacter.CharacterName} Movement", "Enter Movement");
            BoundCharacter.DistanceMoved += amountMoved;
        }

        private void ActionButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(sender as Button).ContextMenu.IsOpen)
            {
                (sender as Button).ContextMenu.IsEnabled = true;
                (sender as Button).ContextMenu.PlacementTarget = (sender as Button);
                (sender as Button).ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
                (sender as Button).ContextMenu.IsOpen = true;
            }
            else
            {
                (sender as Button).ContextMenu.IsEnabled = false;
                (sender as Button).ContextMenu.IsOpen = false;
            }
        }

        private void EndCombatButton_Click(object sender, RoutedEventArgs e)
        {
            (this.DataContext as InitiativeTracker_ViewModel).EndCombat();
            NavigationService.Navigate(new BlankPage(), MainWindow.allowBack.DISALLOWBACK);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!enteredCombatSuccess)
            {
                NavigationService.Navigate(new BlankPage(), MainWindow.allowBack.DISALLOWBACK);
            }
        }
    }
}