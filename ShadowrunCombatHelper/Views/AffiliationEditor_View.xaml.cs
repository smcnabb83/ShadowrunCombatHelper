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

namespace ShadowrunCombatHelper.Views
{
    /// <summary>
    /// Interaction logic for AffiliationEditor_View.xaml
    /// </summary>
    public partial class AffiliationEditor_View : Page
    {
        public AffiliationEditor_View()
        {
            InitializeComponent();
        }

        private void SaveAffiliationChanges_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as AffiliationEditor_ViewModel).WriteToGlobalAffiliationList();
            NavigationService.GoBack();
        }
    }
}
