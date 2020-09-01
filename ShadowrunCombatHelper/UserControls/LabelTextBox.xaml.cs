using System.Windows;
using System.Windows.Controls;

namespace ShadowrunCombatHelper.UserControls
{
    /// <summary>
    ///     Interaction logic for LabelTextBox.xaml
    /// </summary>
    public partial class LabelTextBox : UserControl
    {
        public enum Orient
        {
            Horizontal,
            Vertical
        }

        public static readonly DependencyProperty LabelTextProperty = DependencyProperty.Register("LabelText",
            typeof(string), typeof(LabelTextBox),
            new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.AffectsRender,
                UpdateLabelTextPropertyCallBack));

        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register("Orientation",
            typeof(Orient), typeof(LabelTextBox),
            new FrameworkPropertyMetadata(default(Orient), FrameworkPropertyMetadataOptions.AffectsRender,
                UpdateOrientationPropertyCallBack));

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string),
            typeof(LabelTextBox),
            new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.AffectsRender,
                UpdateTextPropertyCallBack));


        public LabelTextBox()
        {
            InitializeComponent();
        }

        public string LabelText
        {
            get => GetValue(LabelTextProperty).ToString();
            set => SetValue(LabelTextProperty, value);
        }

        public Orient Orientation
        {
            get => (Orient) GetValue(OrientationProperty);
            set => SetValue(OrientationProperty, value);
        }

        public string Text
        {
            get => GetValue(TextProperty).ToString();
            set => SetValue(TextProperty, value);
        }

        private static void UpdateOrientationPropertyCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((LabelTextBox) d).UpdateOrientationProperty();
        }

        private static void UpdateLabelTextPropertyCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((LabelTextBox) d).UpdateLabelTextProperty();
        }

        private static void UpdateTextPropertyCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((LabelTextBox) d).UpdateTextProperty();
        }

        private void UpdateOrientationProperty()
        {
            Canvas.RowDefinitions.Clear();
            Canvas.ColumnDefinitions.Clear();
            Canvas.Children.Clear();
            if (Orientation == Orient.Vertical)
            {
                Canvas.RowDefinitions.Add(new RowDefinition {Height = new GridLength(1, GridUnitType.Auto)});
                Canvas.RowDefinitions.Add(new RowDefinition());
                labelText.TextAlignment = TextAlignment.Right;
                Grid.SetRow(labelText, 0);
                Grid.SetRow(textBox, 1);
                Canvas.Children.Add(labelText);
                Canvas.Children.Add(textBox);
            }
            else
            {
                Canvas.ColumnDefinitions.Add(new ColumnDefinition {Width = new GridLength(1, GridUnitType.Auto)});
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