using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using ShadowrunCombatHelper.Models;

namespace ShadowrunCombatHelper.UserControls
{
    /// <summary>
    ///     Interaction logic for InitiativeDialog.xaml
    /// </summary>
    public partial class InitiativeDialog : Window
    {
        private readonly Character initiativeCharacter;
        public InitiativeDialog(Character charForInitiative)
        {
            InitializeComponent();
            InitiativeInput.Focus();
            InitiativeLabel.Text =
                $"Please {charForInitiative.InitiativeRollText} for initiative and put the result below: ";
            Title += $" for {charForInitiative.CharacterName}.";
            initiativeCharacter = charForInitiative;
        }


        public string InitiativeRollText { get; } = "";

        public int InitiativeRolledValue { get; private set; }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            CompleteEnteringInitiative();
        }

        private void CompleteEnteringInitiative()
        {
            try
            {
                InitiativeRolledValue = int.Parse(InitiativeInput.Text);
                if(InitiativeRolledValue < initiativeCharacter.MinInitiative || InitiativeRolledValue > initiativeCharacter.MinInitiative + initiativeCharacter.InitiativeDice * 6)
                {
                    var res = MessageBox.Show("Selected initiative is outside of the range of possible values for this character. Are you sure this is correct?", "Abnormal Initiative", MessageBoxButton.YesNo, MessageBoxImage.Warning);                        
                    if(res.Equals(MessageBoxResult.No)) {
                        return;    
                    }
                }

                DialogResult = true;
            }
            catch
            {
                FlashTitle();
                Title = $"{InitiativeInput.Text} is not a valid input - please input a number";
            }
        }

        private async void FlashTitle()
        {
            Brush bgBrush = Background;
            Background = Brushes.Red;
            await Task.Delay(200);
            Background = bgBrush;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            InitiativeRolledValue = -1;
            DialogResult = false;
        }

        private void InitiativeInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                CompleteEnteringInitiative();
            }
        }
    }
}