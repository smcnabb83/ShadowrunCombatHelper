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
    /// Interaction logic for InitiativeTracker_View.xaml
    /// </summary>
    public partial class InitiativeTracker_View : Page
    {
        public InitiativeTracker_View()
        {
            InitializeComponent();
            CombatStates.ItemsSource = Enum.GetValues(typeof(Character.CombatState)).Cast<Character.CombatState>();
            CharacterSelectionDialog_View selectDialog = new CharacterSelectionDialog_View();
            bool? result = selectDialog.ShowDialog();
            if(result ?? false)
            {
                InitiativeTracker_ViewModel model = new InitiativeTracker_ViewModel();
                DataContext = model;
                model.AddCombatants(selectDialog.ReturnedCombatants);
            }
            else
            {
            }
        }
    }
}
