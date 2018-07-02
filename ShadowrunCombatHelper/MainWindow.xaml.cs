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
using ShadowrunCombatHelper.Views;
using ShadowrunCombatHelper.Globals;

namespace ShadowrunCombatHelper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.WindowState = System.Windows.WindowState.Maximized;
            CharacterList.Instance.ReadCharacterDataFromFile();
        }

                private void btnCharacterCreator_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new CharacterCreator_View());
        }

        private void btnInitiativeTracker_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new InitiativeTracker_View());
        }

        private void mnuExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void mnuCreateEditSkills_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new SkillsEditor_View());
        }
    }
}
