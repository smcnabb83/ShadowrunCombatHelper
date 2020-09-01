using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ShadowrunCombatHelper.UserControls
{
    /// <summary>
    ///     Interaction logic for HealthDisplay.xaml
    /// </summary>
    public partial class HealthDisplay : UserControl
    {
        public static readonly DependencyProperty CurrentDamageProperty = DependencyProperty.Register("CurrentDamage",
            typeof(int), typeof(HealthDisplay),
            new FrameworkPropertyMetadata(default(int), FrameworkPropertyMetadataOptions.AffectsRender,
                UpdateGridCallback));

        public static readonly DependencyProperty MaxHealthProperty = DependencyProperty.Register("MaxHealth",
            typeof(int), typeof(HealthDisplay),
            new FrameworkPropertyMetadata(default(int), FrameworkPropertyMetadataOptions.AffectsRender,
                InitializeGridCallback));

        public static readonly DependencyProperty OverflowHealthLimitProperty =
            DependencyProperty.Register("OverflowHealthLimit", typeof(int), typeof(HealthDisplay),
                new FrameworkPropertyMetadata(default(int), FrameworkPropertyMetadataOptions.AffectsRender,
                    InitializeGridCallback));

        public static readonly DependencyProperty IsPhysicalDamageGridProperty =
            DependencyProperty.Register("IsPhysicalDamageGrid", typeof(bool), typeof(HealthDisplay));

        private readonly List<TextBox> _textBoxGrid = new List<TextBox>();

        public HealthDisplay()
        {
            InitializeComponent();
        }

        public int CurrentDamage
        {
            get => (int) GetValue(CurrentDamageProperty);
            set
            {
                SetValue(CurrentDamageProperty, value);
                UpdateGrid();
            }
        }

        public int MaxHealth
        {
            get => (int) GetValue(MaxHealthProperty);
            set => SetValue(MaxHealthProperty, value);
        }

        public int OverflowHealthLimit
        {
            get => (int) GetValue(OverflowHealthLimitProperty);
            set => SetValue(OverflowHealthLimitProperty, value);
        }

        public bool IsPhysicalDamageGrid
        {
            get => (bool) GetValue(IsPhysicalDamageGridProperty);
            set => SetValue(IsPhysicalDamageGridProperty, value);
        }

        private void UpdateGrid()
        {
            foreach (TextBox item in _textBoxGrid)
            {
                if (_textBoxGrid.IndexOf(item) < CurrentDamage)
                {
                    item.Background = Brushes.Red;
                }
                else if (!IsPhysicalDamageGrid && _textBoxGrid.IndexOf(item) > MaxHealth - 1)
                {
                    item.Background = Brushes.Black;
                }
                else
                {
                    item.Background = Brushes.White;
                }
            }
        }

        private static void InitializeGridCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((HealthDisplay) d).InitializeGrid();
        }

        private static void UpdateGridCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((HealthDisplay) d).UpdateGrid();
        }

        private void InitializeGrid()
        {
            var gridRows = (int) Math.Ceiling((MaxHealth + (decimal) OverflowHealthLimit) / 3);
            var gridColumns = 3;

            Canvas.Children.Clear();
            Canvas.RowDefinitions.Clear();
            Canvas.ColumnDefinitions.Clear();
            _textBoxGrid.Clear();

            for (var i = 0; i < gridRows; i++)
            {
                Canvas.RowDefinitions.Add(new RowDefinition {Height = new GridLength(1, GridUnitType.Star)});
            }

            for (var j = 0; j < gridColumns; j++)
            {
                Canvas.ColumnDefinitions.Add(new ColumnDefinition {Width = new GridLength(1, GridUnitType.Star)});
            }

            for (var row = 0; row < gridRows; row++)
            {
                for (var column = 0; column < gridColumns; column++)
                {
                    var newTextBlock = new TextBox();
                    newTextBlock.BorderBrush = Brushes.Black;
                    newTextBlock.BorderThickness = new Thickness(1);
                    newTextBlock.IsReadOnly = true;

                    if (column == 2)
                    {
                        newTextBlock.Text = $"-{row + 1}";
                    }

                    if (IsPhysicalDamageGrid && CalculateHealthValueOfGridSquare(row, column) > MaxHealth)
                    {
                        newTextBlock.Text = "UNC";
                    }

                    if (IsPhysicalDamageGrid &&
                        CalculateHealthValueOfGridSquare(row, column) > MaxHealth + OverflowHealthLimit)
                    {
                        newTextBlock.Text = "Dead";
                    }

                    Grid.SetRow(newTextBlock, row);
                    Grid.SetColumn(newTextBlock, column);
                    Canvas.Children.Add(newTextBlock);
                    _textBoxGrid.Add(newTextBlock);
                }
            }

            UpdateGrid();
        }

        private static int CalculateHealthValueOfGridSquare(int row, int col)
        {
            return row * 3 + col + 1;
        }
    }
}