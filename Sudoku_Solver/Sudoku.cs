using System;
using System.Linq;

namespace Sudoku_Solver
{
    public static class Sudoku
    {
        public static bool Solve(ref int[][] board)
        {
            if (!CheckBoard(ref board))
                return false;

            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    if (board[x][y] == 0)
                    {
                        for (int i = 1; i <= 9; i++)
                        {
                            board[x][y] = i;

                            if (Solve(ref board))
                                return true;

                            board[x][y] = 0;
                        }

                        return false;
                    }
                }
            }

            return true;
        }

        private static bool CheckBoard(ref int[][] board)
        {
            for (int pivot = 0, subCornerX = 0, subCornerY = 0; pivot < 9; pivot++, subCornerX += 3)
            {
                var xRow = board[pivot];
                var numbersX = xRow.Where(x => x != 0).ToArray();
                if (numbersX.Distinct().Count() != numbersX.Length)
                    return false;

                var yRow = board.Select(x => x[pivot]).ToArray();
                var numbersY = yRow.Where(x => x != 0).ToArray();
                if (numbersY.Distinct().Count() != numbersY.Length)
                    return false;

                var sub = board.Select(x => x.Skip(subCornerX).Take(3).ToArray()).Skip(subCornerY).Take(3).ToArray();
                var numbersSub = sub.SelectMany(x => x).Where(y => y != 0).ToArray();
                if (numbersSub.Distinct().Count() != numbersSub.Length)
                    return false;

                if (numbersX.Length == 8)
                {
                    var indexOfEmptyCell = Array.IndexOf(xRow, 0);
                    board[pivot][indexOfEmptyCell] = 45 - numbersX.Sum();
                    if (!Solve(ref board))
                    {
                        board[pivot][indexOfEmptyCell] = 0;
                        return false;
                    }
                }

                if (numbersY.Length == 8)
                {
                    var indexOfEmptyCell = Array.IndexOf(yRow, 0);
                    board[indexOfEmptyCell][pivot] = 45 - numbersY.Sum();
                    if (!Solve(ref board))
                    {
                        board[indexOfEmptyCell][pivot] = 0;
                        return false;
                    }
                }

                if (numbersSub.Length == 8)
                {
                    var yCoordinate = sub.Select((x, index) => new { x, index }).Single(y => y.x.Contains(0)).index;
                    var xCoordinate = Array.IndexOf(sub[yCoordinate], 0);
                    yCoordinate += subCornerY;
                    xCoordinate += subCornerX;

                    board[yCoordinate][xCoordinate] = 45 - numbersSub.Sum();

                    if (!Solve(ref board))
                    {
                        board[yCoordinate][xCoordinate] = 0;
                        return false;
                    }
                }

                if (subCornerX == 6)
                {
                    subCornerX = -3;
                    subCornerY += 3;
                }
            }

            return true;
        }
    }
}
