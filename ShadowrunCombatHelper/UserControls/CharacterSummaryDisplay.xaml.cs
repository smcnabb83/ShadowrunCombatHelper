using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ShadowrunCombatHelper.Models;

namespace ShadowrunCombatHelper.UserControls
{
    /// <summary>
    ///     Interaction logic for CharacterSummaryDisplay.xaml
    /// </summary>
    public partial class CharacterSummaryDisplay : UserControl
    {
        public static readonly DependencyProperty BoundCharacterProperty =
            DependencyProperty.Register("BoundCharacter", typeof(Character), typeof(CharacterSummaryDisplay));


        public CharacterSummaryDisplay()
        {
            InitializeComponent();
        }

        public Character BoundCharacter
        {
            get => (Character) GetValue(BoundCharacterProperty);
            set => SetValue(BoundCharacterProperty, value);
        }

        private void CharacterDisplay_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void mnuEndRound_Click(object sender, RoutedEventArgs e)
        {
            BoundCharacter.EndTurn();
        }

        private void CharacterDisplay_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is Character)
            {
                BoundCharacter = DataContext as Character;
            }
        }

        private void btnDealOnePhysicalDamage_Click(object sender, RoutedEventArgs e)
        {
            BoundCharacter.CurrentPhysicalDamage++;
        }

        private void btnDealDamage_Click(object sender, RoutedEventArgs e)
        {
            var dealDamage = new DealDamageDialog(BoundCharacter);
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
            BoundCharacter.FullDefense();
        }

        private void mnuBlock_Click(object sender, RoutedEventArgs e)
        {
            BoundCharacter.Block();
        }

        private void mnuDodge_Click(object sender, RoutedEventArgs e)
        {
            BoundCharacter.Dodge();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            BoundCharacter.HitTheDirt();
        }

        private void mnuIntercept_Click(object sender, RoutedEventArgs e)
        {
            BoundCharacter.Intercept();
        }

        private void mnuParry_Click(object sender, RoutedEventArgs e)
        {
            BoundCharacter.Parry();
        }

        private void mnuRunningToggle_Click(object sender, RoutedEventArgs e)
        {
            BoundCharacter.ToggleRunning();
        }

        private void mnuSimpleAction_Click(object sender, RoutedEventArgs e)
        {
            BoundCharacter.ConsumeSimpleAction();
        }

        private void mnuComplexAction_Click(object sender, RoutedEventArgs e)
        {
            BoundCharacter.ConsumeComplexAction();
        }

        private void mnuFreeAction_Click(object sender, RoutedEventArgs e)
        {
            BoundCharacter.ConsumeFreeAction();
        }

        private void mnuMoveCharacter_Click(object sender, RoutedEventArgs e)
        {
            int amountMoved =
                GetInputDialog<int>.Show($"Input {BoundCharacter.CharacterName} Movement", "Enter Movement");
            BoundCharacter.DistanceMoved += amountMoved;
        }
    }
}