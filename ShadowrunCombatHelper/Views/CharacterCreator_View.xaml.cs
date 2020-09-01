using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using ShadowrunCombatHelper.Globals;
using ShadowrunCombatHelper.Models;
using ShadowrunCombatHelper.UserControls;
using ShadowrunCombatHelper.ViewModels;

namespace ShadowrunCombatHelper.Views
{
    /// <summary>
    ///     Interaction logic for CharacterCreator_View.xaml
    /// </summary>
    public partial class CharacterCreator_View : Page
    {
        public CharacterCreator_View()
        {
            InitializeComponent();
            AffiliationComboBox.ItemsSource = AffiliationList.Instance.Affiliations;
            AffiliationComboBox.DisplayMemberPath = "Name";

            cboMagicalTradition.ItemsSource = TraditionsList.Instance.TraditionList;
            cboMagicalTradition.DisplayMemberPath = "TraditionName";

            if (CharSelectionList.Items.Count > 0)
            {
                CharSelectionList.SelectedIndex = 0;
            }
        }

        private void btnAddNew_Click(object sender, RoutedEventArgs e)
        {
            var ccvm = (CharacterCreator_ViewModel) DataContext;
            string CharName = GetInputDialog<string>.Show("Character Name ", "Name your character");
            ccvm.CreateNewCharacter(CharName);
        }

        private void btnDeleteSelected_Click(object sender, RoutedEventArgs e)
        {
            var remove = (Character) CharSelectionList.SelectedItem;
            int indexToRemove = CharSelectionList.SelectedIndex;
            CharSelectionList.SelectedIndex = indexToRemove <= 0 ? 1 : indexToRemove - 1;
            var ccvm = (CharacterCreator_ViewModel) DataContext;
            ccvm.RemoveCharacterFromList(remove);
        }

        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            var ccvm = (CharacterCreator_ViewModel) DataContext;
            ccvm.SaveCharactersToCharacterList();
        }

        private void CharSelectionList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedCharacter = (Character) CharSelectionList.SelectedItem;
            var myCvs = (CollectionViewSource) FindResource("CharacterSkillsViewSource");
            myCvs.Source = null;
            myCvs.Source = selectedCharacter.Skills;
            myCvs.GroupDescriptions.Clear();
            var desc = new PropertyGroupDescription("SkillType");
            if (!myCvs.GroupDescriptions.Contains(desc))
            {
                myCvs.GroupDescriptions.Add(desc);
            }
        }

        private void CharAttributes_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            SkillsPanelAlt.Height = CharAttributes.ActualHeight;
        }

        private void HealPhysicalDamage_Click(object sender, RoutedEventArgs e)
        {
            int getHealing = GetInputDialog<int>.Show("Enter physical damage to be healed", "Enter Damage Healed");
            var selectedCharacter = (Character) CharSelectionList.SelectedItem;
            selectedCharacter.CurrentPhysicalDamage -= getHealing;
        }

        private void HealStunDamage_Click(object sender, RoutedEventArgs e)
        {
            int getHealing = GetInputDialog<int>.Show("Enter stun damage to be healed", "Enter Damage Healed");
            var selectedCharacter = (Character) CharSelectionList.SelectedItem;
            selectedCharacter.CurrentStunDamage -= getHealing;
        }

        private void btnRename_Click(object sender, RoutedEventArgs e)
        {
            var selected = (Character) CharSelectionList.SelectedItem;
            var vm = (CharacterCreator_ViewModel) DataContext;
            string newName = GetInputDialog<string>.Show("Enter new name", "New Character Name");

            if (newName != null)
            {
                CharSelectionList.SelectionChanged -= CharSelectionList_SelectionChanged;
                vm.Characters.Remove(selected);
                selected.CharacterName = newName;
                vm.Characters.Add(selected);
                CharSelectionList.SelectionChanged += CharSelectionList_SelectionChanged;
                CharSelectionList.SelectedIndex = CharSelectionList.Items.Count - 1;
            }
        }
    }
}