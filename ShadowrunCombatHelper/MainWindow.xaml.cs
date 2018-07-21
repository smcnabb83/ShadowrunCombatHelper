﻿using ShadowrunCombatHelper.Globals;
using ShadowrunCombatHelper.Views;
using System.Windows;
using System.Windows.Navigation;

namespace ShadowrunCombatHelper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public enum allowBack { ALLOWBACK, DISALLOWBACK }

        public MainWindow()
        {
            InitializeComponent();
            this.WindowState = System.Windows.WindowState.Maximized;
            CharacterList.Instance.ReadCharacterDataFromFile();
            mainFrame.Navigated += handleNavigatedObject;
            mainFrame.Navigate(new BlankPage());
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

        private void handleNavigatedObject(object sender, NavigationEventArgs e)
        {
            if ((e.ExtraData is allowBack) && ((allowBack)e.ExtraData) == allowBack.DISALLOWBACK)
            {
                mainFrame.RemoveBackEntry();
                if (mainFrame.CanGoBack)
                {
                    mainFrame.GoBack();
                    mainFrame.RemoveBackEntry();
                }
            }
        }
    }
}