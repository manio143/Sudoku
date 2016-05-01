using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Media.Media3D;

namespace Sudoku
{
    public class SudokuViewModel
    {
        private long moves;
        private TimeSpan time;

        private int[,] board;
        private int[,] solution;

        public long Moves => moves;
        public TimeSpan Time => time;

        public int[,] Board => board;
        public int[,] Solution => solution;

        public SudokuViewModel()
        {
            time = new TimeSpan();
            board = new int[9, 9];
        }

        private static Random random = new Random();
        public void GenerateSudoku(SudokuLevel level)
        {
            GenerateSolution();

            SetupBoard(level);
        }

        /// <summary>
        /// Removes fields from the full solution.
        /// </summary>
        /// <param name="level"></param>
        private void SetupBoard(SudokuLevel level)
        {
            board = (int[,])solution.Clone();
            int remainingItems = ((int)level + 1) * 20;
            while (remainingItems > 0)
            {
                int posX = random.Next(9);
                int posY = random.Next(9);
                if (board[posX, posY] == 0)
                    continue;
                board[posX, posY] = 0;
                remainingItems--;
            }
        }

        private void GenerateSolution()
        {
            solution = new int[9, 9];
            var availableNumbers = new List<int>[9 * 9];

            int currentIndex = 0;

            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            for (int x = 0; x < availableNumbers.Length; x++)
                availableNumbers[x] = new List<int>(numbers);

            while (currentIndex < solution.Length)
            {
                if (availableNumbers[currentIndex].Count > 0)
                {
                    int randomIndex = random.Next(availableNumbers[currentIndex].Count);
                    int randomAvailableNumber = availableNumbers[currentIndex][randomIndex];
                    if (!Conflicts(randomAvailableNumber, currentIndex))
                    {
                        solution[currentIndex / 9, currentIndex % 9] = randomAvailableNumber;
                        currentIndex++;
                    }
                    availableNumbers[currentIndex].RemoveAt(randomIndex);
                }
                else
                {
                    availableNumbers[currentIndex].AddRange(numbers);
                    currentIndex--;
                    solution[currentIndex / 9, currentIndex % 9] = 0;
                }
            }
        }

        private bool Conflicts(int number, int currentIndex)
        {
            int i = currentIndex / 9;
            int j = currentIndex % 9;
            for (int ii = 0; ii < i; ii++)
                if (solution[ii, j] == number)
                    return true;
            for (int jj = 0; jj < j; jj++)
                if (solution[i, jj] == number)
                    return true;
            foreach (var pair in GetNeighbours(i, j))
                if (solution[pair.Item1, pair.Item2] == number)
                    return true;
            return false;
        }

        private IEnumerable<Tuple<int, int>> GetNeighbours(int i, int j)
        {
            int col = i / 3;
            int row = j / 3;
            var positions = new Tuple<int, int>[9];
            for (int x = 0; x < 3; x++)
                for (int y = 0; y < 3; y++)
                    positions[x * 3 + y] = new Tuple<int, int>(col * 3 + x, row * 3 + y);
            return positions;
        }
    }
}