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
    /// Interaction logic for DealDamageDialog.xaml
    /// </summary>
    public partial class DealDamageDialog : Window
    {
        private int damageDealt;

        public int DamageDealt
        {
            get { return damageDealt; }
        }

        private bool physicalDamage;

        public bool PhysicalDamage
        {
            get { return physicalDamage; }
        }


        public DealDamageDialog(Character c)
        {
            InitializeComponent();
            Title += $" {c.CharacterName}.";
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                damageDealt = int.Parse(TxtDamage.Text);
                if (PhysDamageOption.IsChecked ?? false)
                {
                    physicalDamage = true;
                }
                else
                {
                    physicalDamage = false;
                }
                this.DialogResult = true;
            }
            catch
            {
                FlashOnError();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private async void FlashOnError()
        {
            string originalTitle = this.Title;
            Brush bgBrush = this.Background;
            this.Title = $" {TxtDamage.Text} is not a valid input. Please enter a number";
            this.Background = Brushes.Red;
            await Task.Delay(200);
            this.Background = bgBrush;
            await Task.Delay(800);
            this.Title = originalTitle;            
        }
    }
}
