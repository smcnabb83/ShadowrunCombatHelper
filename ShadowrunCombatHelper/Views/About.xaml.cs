using System.Diagnostics;
using System.Windows;

namespace ShadowrunCombatHelper.Views
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class AboutApp : Window
    {
        public AboutApp()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }
}