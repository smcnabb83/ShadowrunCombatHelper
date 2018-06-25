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

namespace ShadowrunCombatHelper.UserControls
{
    /// <summary>
    /// Interaction logic for LabelTextBox.xaml
    /// </summary>
    public partial class LabelTextBox : UserControl
    {
        public enum Orient { HORIZONTAL, VERTICAL };
        public static readonly DependencyProperty LabelTextProperty = DependencyProperty.Register("LabelText", typeof(string), typeof(LabelTextBox), new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(UpdateLabelTextPropertyCallBack)));
        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register("Orientation", typeof(Orient), typeof(LabelTextBox), new FrameworkPropertyMetadata(default(Orient), FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(UpdateOrientationPropertyCallBack)));
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(LabelTextBox), new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(UpdateTextPropertyCallBack)));


        public LabelTextBox()
        {
            InitializeComponent();
        }

        public string LabelText
        {
            get
            {
                return GetValue(LabelTextProperty).ToString();
            }
            set
            {
                SetValue(LabelTextProperty, value);
            }
        }

        public Orient Orientation
        {
            get
            {
                return (Orient)GetValue(OrientationProperty);
            }
            set
            {
                SetValue(OrientationProperty, value);
            }
        }

        public string Text
        {
            get
            {
                return GetValue(TextProperty).ToString();
            }
            set
            {
                SetValue(TextProperty, value);
            }
        }

        private static void UpdateOrientationPropertyCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((LabelTextBox)d).UpdateOrientationProperty();
        }

        private static void UpdateLabelTextPropertyCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((LabelTextBox)d).UpdateLabelTextProperty();
        }

        private static void UpdateTextPropertyCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((LabelTextBox)d).UpdateTextProperty();
        }

        private void UpdateOrientationProperty()
        {
            Canvas.RowDefinitions.Clear();
            Canvas.ColumnDefinitions.Clear();
            Canvas.Children.Clear();
            if (Orientation == Orient.VERTICAL)
            {
                Canvas.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) });
                Canvas.RowDefinitions.Add(new RowDefinition());
                labelText.TextAlignment = TextAlignment.Right;
                Grid.SetRow(labelText, 0);
                Grid.SetRow(textBox, 1);
                Canvas.Children.Add(labelText);
                Canvas.Children.Add(textBox);

            }
            else
            {
                Canvas.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Auto) });
                Canvas.ColumnDefinitions.Add(new ColumnDefinition());
                labelText.TextAlignment = TextAlignment.Right;
                Grid.SetColumn(labelText, 0);
                Grid.SetColumn(textBox, 1);
                Canvas.Children.Add(labelText);
                Canvas.Children.Add(textBox);
            }
        }

        private void UpdateLabelTextProperty()
        {
            labelText.Text = LabelText;
        }

        private void UpdateTextProperty()
        {
            textBox.Text = Text;
        }
    }
}
