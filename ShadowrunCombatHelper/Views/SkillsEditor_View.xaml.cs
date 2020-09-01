using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ShadowrunCombatHelper.Models;
using ShadowrunCombatHelper.ViewModels;

namespace ShadowrunCombatHelper.Views
{
    /// <summary>
    ///     Interaction logic for SkillsEditor_View.xaml
    /// </summary>
    public partial class SkillsEditor_View : Page
    {
        private bool _allowDelete = true;

        public SkillsEditor_View()
        {
            InitializeComponent();
        }

        private void RelatedAttributeSelector_GotFocus(object sender, EventArgs e)
        {
            var me = (ListBox) sender;
            ObservableCollection<Skill.Attributes> itemsToSelect =
                me.BindingGroup.Items.OfType<Skill>().ToList()[0].RelatedAttributes;
            foreach (Skill.Attributes item in itemsToSelect)
            {
                me.SelectedItems.Add(item);
            }
        }

        private void RelatedAttributeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var me = (ListBox) sender;
            if (e.AddedItems != null)
            {
                ObservableCollection<Skill.Attributes> SelectedItems =
                    me.BindingGroup.Items.OfType<Skill>().ToList()[0].RelatedAttributes;
                foreach (object a in e.AddedItems)
                {
                    if (a.GetType() == typeof(Skill.Attributes) && !SelectedItems.Contains((Skill.Attributes) a))
                    {
                        SelectedItems.Add((Skill.Attributes) a);
                    }
                }
            }

            if (e.RemovedItems != null && _allowDelete)
            {
                ObservableCollection<Skill.Attributes> SelectedItems =
                    me.BindingGroup.Items.OfType<Skill>().ToList()[0].RelatedAttributes;
                foreach (object a in e.RemovedItems)
                {
                    if (a.GetType() == typeof(Skill.Attributes))
                    {
                        SelectedItems.Remove((Skill.Attributes) a);
                    }
                }
            }

            _allowDelete = true;
        }

        private void RelatedAttributeSelector_LostFocus(object sender, RoutedEventArgs e)
        {
            _allowDelete = false;
            e.Handled = true;
        }

        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as SkillsEditor_ViewModel)?.WriteToGlobalSkillsList();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
    }
}