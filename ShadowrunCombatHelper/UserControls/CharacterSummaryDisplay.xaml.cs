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

        private void MnuEndRound_Click(object sender, RoutedEventArgs e)
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

        private void BtnDealOnePhysicalDamage_Click(object sender, RoutedEventArgs e)
        {
            BoundCharacter.CurrentPhysicalDamage++;
        }

        private void BtnDealDamage_Click(object sender, RoutedEventArgs e)
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

        private void MnuFullDefense_Click(object sender, RoutedEventArgs e)
        {
            BoundCharacter.FullDefense();
        }

        private void MnuBlock_Click(object sender, RoutedEventArgs e)
        {
            BoundCharacter.Block();
        }

        private void MnuDodge_Click(object sender, RoutedEventArgs e)
        {
            BoundCharacter.Dodge();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            BoundCharacter.HitTheDirt();
        }

        private void MnuIntercept_Click(object sender, RoutedEventArgs e)
        {
            BoundCharacter.Intercept();
        }

        private void MnuParry_Click(object sender, RoutedEventArgs e)
        {
            BoundCharacter.Parry();
        }

        private void MnuRunningToggle_Click(object sender, RoutedEventArgs e)
        {
            BoundCharacter.ToggleRunning();
        }

        private void MnuSimpleAction_Click(object sender, RoutedEventArgs e)
        {
            BoundCharacter.ConsumeSimpleAction();
        }

        private void MnuComplexAction_Click(object sender, RoutedEventArgs e)
        {
            BoundCharacter.ConsumeComplexAction();
        }

        private void MnuFreeAction_Click(object sender, RoutedEventArgs e)
        {
            BoundCharacter.ConsumeFreeAction();
        }

        private void MnuMoveCharacter_Click(object sender, RoutedEventArgs e)
        {
            int amountMoved =
                GetInputDialog<int>.Show($"Input {BoundCharacter.CharacterName} Movement", "Enter Movement");
            BoundCharacter.DistanceMoved += amountMoved;
        }
    }
}