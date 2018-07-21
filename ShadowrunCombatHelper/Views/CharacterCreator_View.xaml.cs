using ShadowrunCombatHelper.Models;
using ShadowrunCombatHelper.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ShadowrunCombatHelper.Views
{
    /// <summary>
    /// Interaction logic for CharacterCreator_View.xaml
    /// </summary>
    public partial class CharacterCreator_View : Page
    {
        public CharacterCreator_View()
        {
            InitializeComponent();
            AffiliationComboBox.ItemsSource = ShadowrunCombatHelper.Globals.AffiliationList.Affiliations;
            AffiliationComboBox.DisplayMemberPath = "Name";

            cboMagicalTradition.ItemsSource = ShadowrunCombatHelper.Globals.TraditionsList.Instance.TraditionList;
            cboMagicalTradition.DisplayMemberPath = "TraditionName";
        }

        private void btnAddNew_Click(object sender, RoutedEventArgs e)
        {
            CharacterCreator_ViewModel ccvm = (CharacterCreator_ViewModel)DataContext;
            ccvm.CreateNewCharacter();
        }

        private void btnDeleteSelected_Click(object sender, RoutedEventArgs e)
        {
            Character remove = (Character)CharSelectionList.SelectedItem;
            CharacterCreator_ViewModel ccvm = (CharacterCreator_ViewModel)DataContext;
            ccvm.RemoveCharacterFromList(remove);
        }

        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            CharacterCreator_ViewModel ccvm = (CharacterCreator_ViewModel)DataContext;
            ccvm.SaveCharactersToCharacterList();
        }

        private void CharSelectionList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Character selectedCharacter = (Character)CharSelectionList.SelectedItem;
            CollectionViewSource myCvs = (CollectionViewSource)this.FindResource("CharacterSkillsViewSource");
            myCvs.Source = null;
            myCvs.Source = selectedCharacter.Skills;
            myCvs.GroupDescriptions.Clear();
            myCvs.GroupDescriptions.Add(new PropertyGroupDescription("SkillType"));
        }

        private void CharAttributes_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            SkillsPanelAlt.Height = CharAttributes.ActualHeight;
        }
    }
}