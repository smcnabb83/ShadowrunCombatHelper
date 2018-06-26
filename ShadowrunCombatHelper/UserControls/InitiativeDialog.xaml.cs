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
using ShadowrunCombatHelper.Models;

namespace ShadowrunCombatHelper.UserControls
{
    /// <summary>
    /// Interaction logic for InitiativeDialog.xaml
    /// </summary>
    public partial class InitiativeDialog : Window
    {
        private string initiativeRollText = "";
        private int initiativeRolledValue = 0;

        public string InitiativeRollText
        {
            get
            {
                return initiativeRollText;
            }
        }

        public int InitiativeRolledValue
        {
            get
            {
                return initiativeRolledValue;
            }
        }


        public InitiativeDialog(Character CharForInitiative)
        {
            InitializeComponent();
            InitiativeLabel.Text = $"Please {CharForInitiative.InitiativeRollText} for initiative and put the result below: ";        
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.initiativeRolledValue = int.Parse(InitiativeInput.Text);
                this.DialogResult = true;
            }
            catch
            {
                FlashTitle();
                this.Title = $"{InitiativeInput.Text} is not a valid input - please input a number";
                
            }
        }

        private async void FlashTitle()
        {
            Brush bgBrush = this.Background;
            this.Background = Brushes.Red;
            await Task.Delay(200);
            this.Background = bgBrush;
            
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.initiativeRolledValue = -1;
            this.DialogResult = false;
        }
    }
}
