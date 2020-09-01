using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using ShadowrunCombatHelper.Models;

namespace ShadowrunCombatHelper.UserControls
{
    /// <summary>
    ///     Interaction logic for DealDamageDialog.xaml
    /// </summary>
    public partial class DealDamageDialog : Window
    {
        public DealDamageDialog(Character c)
        {
            InitializeComponent();
            Title += $" {c.CharacterName}.";
            TxtDamage.Focus();
        }

        public int DamageDealt { get; private set; }

        public bool PhysicalDamage { get; private set; }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            Submit();
        }

        private void Submit()
        {
            try
            {
                DamageDealt = int.Parse(TxtDamage.Text);
                if (PhysDamageOption.IsChecked ?? false)
                {
                    PhysicalDamage = true;
                }
                else
                {
                    PhysicalDamage = false;
                }

                DialogResult = true;
            }
            catch
            {
                FlashOnError();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private async void FlashOnError()
        {
            string originalTitle = Title;
            Brush bgBrush = Background;
            Title = $" {TxtDamage.Text} is not a valid input. Please enter a number";
            Background = Brushes.Red;
            await Task.Delay(200);
            Background = bgBrush;
            await Task.Delay(800);
            Title = originalTitle;
        }

        private void TxtDamage_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    Submit();
                    e.Handled = true;
                    break;
                case Key.P:
                    PhysDamageOption.IsChecked = true;
                    e.Handled = true;
                    break;
                case Key.S:
                    StunDamageOption.IsChecked = true;
                    e.Handled = true;
                    break;
            }
        }
    }
}