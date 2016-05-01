using System;
using System.Windows;
using System.Windows.Controls;
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
            //...
            DataContext = sudoku;
        }

        private void DifficultySlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            currentLevel = (SudokuLevel) e.NewValue;
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
                        BorderThickness = new Thickness(.5)
                    };
                    sudokuTextBlocks[i, j] = new TextBlock();
                    border.Child = sudokuTextBlocks[i, j];
                    Grid.SetColumn(border, i);
                    Grid.SetRow(border, j);
                    SudokuGrid.Children.Add(border);
                }
        }
    }
}
