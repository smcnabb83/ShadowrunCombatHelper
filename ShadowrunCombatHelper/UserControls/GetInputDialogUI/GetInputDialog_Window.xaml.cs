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

namespace ShadowrunCombatHelper.UserControls.GetInputDialogUI
{
    /// <summary>
    /// Interaction logic for GetInputDialog_Window.xaml
    /// </summary>
    public partial class GetInputDialog_Window : Window
    {
        public string Result;
        
        public GetInputDialog_Window(string messageString, string titleString)
        {
            InitializeComponent();
            this.Title = titleString;
            MessageLabel.Text = messageString;
            txtInput.Focus();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            Submit();
        }

        private void Submit()
        {
            Result = txtInput.Text;
            DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Result = null;
            DialogResult = false;
        }

        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                Submit();
                e.Handled = true;
            }
        }
    }
}
