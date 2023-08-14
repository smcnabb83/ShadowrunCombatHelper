using ShadowrunCombatHelper.Globals;
using ShadowrunCombatHelper.Views;
using System.Windows;
using System.Windows.Navigation;
using LiteDB;

namespace ShadowrunCombatHelper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public enum GoBack { Allow, Disallow }

        public MainWindow()
        {
            InitializeComponent();
            var db = new LiteDatabase("localdata.db");
            this.WindowState = WindowState.Maximized;
            CharacterList.Instance.ReadCharacterDataFromFile();
            mainFrame.Navigated += HandleNavigatedObject;
            mainFrame.Navigate(new BlankPage());
        }

        private void BtnCharacterCreator_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new CharacterCreator_View());
        }

        private void BtnInitiativeTracker_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new InitiativeTracker_View());
        }

        private void MnuExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MnuCreateEditSkills_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new SkillsEditor_View());
        }

        private void HandleNavigatedObject(object sender, NavigationEventArgs e)
        {
            if ((e.ExtraData is GoBack canGoBack) && canGoBack == GoBack.Disallow)
            {
                mainFrame.RemoveBackEntry();
                if (mainFrame.CanGoBack)
                {
                    mainFrame.GoBack();
                    mainFrame.RemoveBackEntry();
                }
            }
        }

        private void MnuCreateEditAffiliations_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new AffiliationEditor_View());
        }

        private void MnuAbout_Click(object sender, RoutedEventArgs e)
        {
            new AboutApp().Show();
        }
    }
}