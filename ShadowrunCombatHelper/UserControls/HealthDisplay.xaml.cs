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

namespace ShadowrunCombatHelper.UserControls
{
    /// <summary>
    /// Interaction logic for HealthDisplay.xaml
    /// </summary>
    public partial class HealthDisplay : UserControl
    {
        public static readonly DependencyProperty CurrentDamageProperty = DependencyProperty.Register("CurrentDamage", typeof(int), typeof(HealthDisplay), new FrameworkPropertyMetadata(default(int), FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(UpdateGridCallback)));
        public static readonly DependencyProperty MaxHealthProperty = DependencyProperty.Register("MaxHealth", typeof(int), typeof(HealthDisplay), new FrameworkPropertyMetadata(default(int),FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(InitializeGridCallback)));
        public static readonly DependencyProperty OverflowHealthLimitProperty = DependencyProperty.Register("OverflowHealthLimit", typeof(int) ,typeof(HealthDisplay), new FrameworkPropertyMetadata(default(int), FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback(InitializeGridCallback)));
        public static readonly DependencyProperty IsPhysicalDamageGridProperty = DependencyProperty.Register("IsPhysicalDamageGrid", typeof(bool), typeof(HealthDisplay));

        private List<TextBox> textBoxGrid = new List<TextBox>();

        public int CurrentDamage
        {
            get
            {
                return (int)GetValue(CurrentDamageProperty);
            }
            set
            {
                SetValue(CurrentDamageProperty, value);
                UpdateGrid();
            }

        }

        public int MaxHealth
        {
            get
            {
                return (int)GetValue(MaxHealthProperty);
            }
            set
            {
                SetValue(MaxHealthProperty, value);
            }
        }

        public int OverflowHealthLimit
        {
            get
            {
                return (int)GetValue(OverflowHealthLimitProperty);
            }
            set
            {
                SetValue(OverflowHealthLimitProperty, value);
            }
        }

        public bool IsPhysicalDamageGrid
        {
            get
            {
                return (bool)GetValue(IsPhysicalDamageGridProperty);
            }
            set
            {
                SetValue(IsPhysicalDamageGridProperty, value);
            }
        }

        public HealthDisplay()
        {
            InitializeComponent();
        }

        private void UpdateGrid()
        {
            foreach(var item in textBoxGrid)
            {
                if(textBoxGrid.IndexOf(item) < CurrentDamage)
                {
                    item.Background = Brushes.Red;
                }
                else if(!IsPhysicalDamageGrid && textBoxGrid.IndexOf(item) > (MaxHealth - 1))
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
            ((HealthDisplay)d).InitializeGrid();
        }

        private static void UpdateGridCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((HealthDisplay)d).UpdateGrid();
        }

        private void InitializeGrid()
        {
            int gridRows = (int)Math.Ceiling(((decimal)MaxHealth + (decimal)OverflowHealthLimit) / 3);
            int gridColumns = 3;

            Canvas.Children.Clear();
            Canvas.RowDefinitions.Clear();
            Canvas.ColumnDefinitions.Clear();
            textBoxGrid.Clear();

            for(int i = 0; i < gridRows; i++)
            {
                Canvas.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star)});
            }

            for(int j = 0; j < gridColumns; j++)
            {
                Canvas.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            }
            
            for(int row = 0; row < gridRows; row++)
            {
                for(int column = 0; column < gridColumns; column++)
                {
                    TextBox newTextBlock = new TextBox();
                    newTextBlock.BorderBrush = Brushes.Black;
                    newTextBlock.BorderThickness = new Thickness(1);
                    newTextBlock.IsReadOnly = true;

                    if(column == 2)
                    {
                        newTextBlock.Text = $"-{row+1}";
                    }

                    if (IsPhysicalDamageGrid && (CalculateHealthValueOfGridSquare(row, column) > MaxHealth))
                    {
                        newTextBlock.Text = "UNC";
                    }

                    if(IsPhysicalDamageGrid && (CalculateHealthValueOfGridSquare(row, column) > MaxHealth + OverflowHealthLimit))
                    {
                        newTextBlock.Text = "Dead";
                    }

                    Grid.SetRow(newTextBlock, row);
                    Grid.SetColumn(newTextBlock, column);
                    Canvas.Children.Add(newTextBlock);
                    textBoxGrid.Add(newTextBlock);
                }
            }
            UpdateGrid();
        }

        private int CalculateHealthValueOfGridSquare(int row, int col)
        {
            return (row * 3) + col + 1;
        }
    }
}
