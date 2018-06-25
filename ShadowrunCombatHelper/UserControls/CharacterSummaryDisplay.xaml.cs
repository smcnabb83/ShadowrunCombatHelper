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
using ShadowrunCombatHelper.Models;

namespace ShadowrunCombatHelper.UserControls
{
    /// <summary>
    /// Interaction logic for CharacterSummaryDisplay.xaml
    /// </summary>
    public partial class CharacterSummaryDisplay : UserControl
    {

        public static readonly DependencyProperty BoundCharacterProperty = DependencyProperty.Register("BoundCharacter", typeof(Character), typeof(CharacterSummaryDisplay));

        public Character BoundCharacter
        {
            get
            {
                return (Character)GetValue(BoundCharacterProperty);
            }
            set
            {
                SetValue(BoundCharacterProperty, value);
            }
        }


        public CharacterSummaryDisplay()
        {
            InitializeComponent();
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
            if(DataContext is Character)
            {
                BoundCharacter = (DataContext as Character);
            }
        }

        private void btnDealOnePhysicalDamage_Click(object sender, RoutedEventArgs e)
        {
            BoundCharacter.CurrentPhysicalDamage++;
        }
    }
}
