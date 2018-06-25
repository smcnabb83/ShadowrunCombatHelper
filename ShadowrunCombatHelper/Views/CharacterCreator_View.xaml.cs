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
using ShadowrunCombatHelper.ViewModels;
using ShadowrunCombatHelper.Models;

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
    }
}
