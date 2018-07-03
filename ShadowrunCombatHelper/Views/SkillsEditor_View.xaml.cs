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
using ShadowrunCombatHelper.Models;
using ShadowrunCombatHelper.ViewModels;

namespace ShadowrunCombatHelper.Views
{
    /// <summary>
    /// Interaction logic for SkillsEditor_View.xaml
    /// </summary>
    public partial class SkillsEditor_View : Page
    {
        public SkillsEditor_View()
        {
            InitializeComponent();
        }

        private bool allowDelete = true;

        private void RelatedAttributeSelector_GotFocus(object sender, EventArgs e)
        {
            ListBox me = (ListBox)sender;
            var itemsToSelect = me.BindingGroup.Items.OfType<Skill>().ToList()[0].RelatedAttributes;
            foreach(var item in itemsToSelect)
            {
                me.SelectedItems.Add(item);
            }
        }

        private void RelatedAttributeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox me = (ListBox)sender;
            if (e.AddedItems != null)
            {
                var SelectedItems = me.BindingGroup.Items.OfType<Skill>().ToList()[0].RelatedAttributes;
                foreach(var a in e.AddedItems)
                {
                    if (a.GetType() == typeof(Skill.Attributes) && !SelectedItems.Contains((Skill.Attributes)a)) 
                    {
                        SelectedItems.Add((Skill.Attributes)a);
                    }
                }
            }

            if(e.RemovedItems != null && allowDelete)
            {
                var SelectedItems = me.BindingGroup.Items.OfType<Skill>().ToList()[0].RelatedAttributes;
                foreach (var a in e.RemovedItems)
                {
                    if (a.GetType() == typeof(Skill.Attributes))
                    {
                        SelectedItems.Remove((Skill.Attributes)a);
                    }
                }
            }
            allowDelete = true;
        }

        private void RelatedAttributeSelector_LostFocus(object sender, RoutedEventArgs e)
        {
            Console.Write("Called here");
            allowDelete = false;
            e.Handled = true;
        }

        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as SkillsEditor_ViewModel).WriteToGlobalSkillsList();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }
    }
}
