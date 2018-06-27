﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ShadowrunCombatHelper
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            EventManager.RegisterClassHandler(typeof(TextBox), TextBox.PreviewMouseLeftButtonDownEvent, new MouseButtonEventHandler(SelectivelyIgnoreMouseButton));
            EventManager.RegisterClassHandler(typeof(TextBox), TextBox.GotKeyboardFocusEvent, new RoutedEventHandler(SelectAllText));
            EventManager.RegisterClassHandler(typeof(TextBox), TextBox.MouseDoubleClickEvent, new RoutedEventHandler(SelectAllText));
            base.OnStartup(e);
        }
        
        void SelectivelyIgnoreMouseButton(object sender, MouseButtonEventArgs e)
        {
            DependencyObject parent = e.OriginalSource as UIElement;
            while(parent != null && !(parent is TextBox))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            if(parent != null)
            {
                var textBox = (TextBox)parent;
                if (!textBox.IsKeyboardFocusWithin)
                {
                    textBox.Focus();
                    e.Handled = true;
                }
            }
        }

        void SelectAllText(object sender, RoutedEventArgs e)
        {
            var textBox = e.OriginalSource as TextBox;
            if (textBox != null)
                textBox.SelectAll();
        }
    }
}
