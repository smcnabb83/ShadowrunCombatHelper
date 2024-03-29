﻿using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using ShadowrunCombatHelper.Models;
using ShadowrunCombatHelper.UserControls;
using ShadowrunCombatHelper.ViewModels;

namespace ShadowrunCombatHelper.Views
{
    /// <summary>
    ///     Interaction logic for InitiativeTracker_View.xaml
    /// </summary>
    public partial class InitiativeTracker_View : Page
    {
        private readonly bool enteredCombatSuccess;

        public InitiativeTracker_View()
        {
            InitializeComponent();
            CombatStates.ItemsSource = Enum.GetValues(typeof(Character.CombatState)).Cast<Character.CombatState>();
            var selectDialog = new CharacterSelectionDialog_View();
            bool? result = selectDialog.ShowDialog();
            if (result ?? false)
            {
                enteredCombatSuccess = true;
                var model = new InitiativeTracker_ViewModel();
                DataContext = model;
                model.AddCombatants(selectDialog.ReturnedCombatants);
            }
            else
            {
                Visibility = Visibility.Hidden;
                enteredCombatSuccess = false;
            }
        }

        private void MnuEndRound_Click(object sender, RoutedEventArgs e)
        {
            Character boundCharacter = (DataContext as InitiativeTracker_ViewModel)?.CurrentCharacter;
            boundCharacter?.EndTurn();
        }

        private void BtnDealOnePhysicalDamage_Click(object sender, RoutedEventArgs e)
        {
            Character boundCharacter = (DataContext as InitiativeTracker_ViewModel)?.CurrentCharacter;
            if (boundCharacter != null)
            {
                boundCharacter.CurrentPhysicalDamage++;
            }
        }

        private void BtnDealDamage_Click(object sender, RoutedEventArgs e)
        {
            Character boundCharacter = (DataContext as InitiativeTracker_ViewModel)?.CurrentCharacter;
            var dealDamage = new DealDamageDialog(boundCharacter);
            bool? result = dealDamage.ShowDialog();
            if ((result ?? false ) && boundCharacter != null)
            {
                if (dealDamage.PhysicalDamage)
                {
                    boundCharacter.CurrentPhysicalDamage += dealDamage.DamageDealt;
                }
                else
                {
                    boundCharacter.CurrentStunDamage += dealDamage.DamageDealt;
                }
            }
        }

        private void MnuFullDefense_Click(object sender, RoutedEventArgs e)
        {
            Character boundCharacter = (DataContext as InitiativeTracker_ViewModel)?.CurrentCharacter;
            boundCharacter?.FullDefense();
        }

        private void MnuBlock_Click(object sender, RoutedEventArgs e)
        {
            Character boundCharacter = (DataContext as InitiativeTracker_ViewModel)?.CurrentCharacter;
            boundCharacter?.Block();
        }

        private void MnuDodge_Click(object sender, RoutedEventArgs e)
        {
            Character boundCharacter = (DataContext as InitiativeTracker_ViewModel)?.CurrentCharacter;
            boundCharacter?.Dodge();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Character boundCharacter = (DataContext as InitiativeTracker_ViewModel)?.CurrentCharacter;
            boundCharacter?.HitTheDirt();
        }

        private void MnuIntercept_Click(object sender, RoutedEventArgs e)
        {
            Character boundCharacter = (DataContext as InitiativeTracker_ViewModel)?.CurrentCharacter;
            boundCharacter?.Intercept();
        }

        private void MnuParry_Click(object sender, RoutedEventArgs e)
        {
            Character boundCharacter = (DataContext as InitiativeTracker_ViewModel)?.CurrentCharacter;
            boundCharacter?.Parry();
        }

        private void MnuRunningToggle_Click(object sender, RoutedEventArgs e)
        {
            Character boundCharacter = (DataContext as InitiativeTracker_ViewModel)?.CurrentCharacter;
            boundCharacter?.ToggleRunning();
        }

        private void MnuSimpleAction_Click(object sender, RoutedEventArgs e)
        {
            Character boundCharacter = (DataContext as InitiativeTracker_ViewModel)?.CurrentCharacter;
            boundCharacter?.ConsumeSimpleAction();
        }

        private void MnuComplexAction_Click(object sender, RoutedEventArgs e)
        {
            Character boundCharacter = (DataContext as InitiativeTracker_ViewModel)?.CurrentCharacter;
            boundCharacter?.ConsumeComplexAction();
        }

        private void MnuFreeAction_Click(object sender, RoutedEventArgs e)
        {
            Character boundCharacter = (DataContext as InitiativeTracker_ViewModel)?.CurrentCharacter;
            boundCharacter?.ConsumeFreeAction();
        }

        private void MnuMoveCharacter_Click(object sender, RoutedEventArgs e)
        {
            Character boundCharacter = (DataContext as InitiativeTracker_ViewModel)?.CurrentCharacter;
            if (boundCharacter != null)
            {
                int amountMoved =
                    GetInputDialog<int>.Show($"Input {boundCharacter.CharacterName} Movement", "Enter Movement");
                boundCharacter.DistanceMoved += amountMoved;
            }
        }


        private void ActionButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(sender is Button senderButton) || senderButton.ContextMenu == null)
            {
                return;
            }

            if (!senderButton.ContextMenu.IsOpen)
            {
                senderButton.ContextMenu.IsEnabled = true;
                senderButton.ContextMenu.PlacementTarget = sender as Button;
                senderButton.ContextMenu.Placement = PlacementMode.Bottom;
                senderButton.ContextMenu.IsOpen = true;
            }
            else
            {
               senderButton.ContextMenu.IsEnabled = false;
               senderButton.ContextMenu.IsOpen = false;
            }
        }

        private void EndCombatButton_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as InitiativeTracker_ViewModel)?.EndCombat();
            NavigationService?.Navigate(new BlankPage(), MainWindow.GoBack.Disallow);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!enteredCombatSuccess)
            {
                NavigationService?.Navigate(new BlankPage(), MainWindow.GoBack.Disallow);
            }
        }
    }
}