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

namespace ShadowrunCombatHelper.Views
{
    /// <summary>
    /// Interaction logic for MainMenu_View.xaml
    /// </summary>
    public partial class MainMenu_View : Page
    {
        public MainMenu_View()
        {
            InitializeComponent();
        }

        private void btnCharacterCreator_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CharacterCreator_View());
        }

        private void btnInitiativeTracker_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new InitiativeTracker_View());
        }
    }
}
