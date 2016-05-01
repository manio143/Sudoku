using System;
using System.Windows;

namespace Sudoku
{
    public partial class MainWindow
    {
        private SudokuViewModel sudoku;
        private SudokuLevel currentLevel;
        
        public MainWindow()
        {
            InitializeComponent();
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
    }
}
