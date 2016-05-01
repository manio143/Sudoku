using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Sudoku
{
    public partial class MainWindow
    {
        private SudokuViewModel sudoku;
        private SudokuLevel currentLevel;
        private TextBlock[,] sudokuTextBlocks;

        public MainWindow()
        {
            InitializeComponent();
            GenerateSudokuGrid();
        }

        private void NewGame(object sender, EventArgs args)
        {
            sudoku = new SudokuViewModel();
            sudoku.GenerateSudoku(currentLevel);
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    sudokuTextBlocks[i, j].SetBinding(TextBlock.TextProperty, new Binding($"Board[{i}][{j}]"));
            DataContext = sudoku;
        }

        private void DifficultySlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            currentLevel = (SudokuLevel)e.NewValue;
            DifficultyTextBlock.Text = currentLevel.ToString().ToUpper();
        }

        private void GenerateSudokuGrid()
        {
            sudokuTextBlocks = new TextBlock[9, 9];
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                {

                    var border = new Border
                    {
                        BorderBrush = Brushes.Black,
                        BorderThickness = GetThickness(i, j, 0.1, 0.3)
                    };
                    sudokuTextBlocks[i, j] = new TextBlock
                    {
                        FontSize = 5,
                        VerticalAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center
                    };
                    border.Child = sudokuTextBlocks[i, j];
                    Grid.SetRow(border, i);
                    Grid.SetColumn(border, j);
                    SudokuGrid.Children.Add(border);
                }
        }

        private Thickness GetThickness(int i, int j, double thin, double thick)
        {
            var top = i % 3 == 0 ? thick : thin;
            var bottom = i % 3 == 2 ? thick : thin;
            var left = j % 3 == 0 ? thick : thin;
            var right = j % 3 == 2 ? thick : thin;
            return new Thickness(left, top, right, bottom);
        }
    }
}
