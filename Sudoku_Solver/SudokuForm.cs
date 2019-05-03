using System;
using System.Windows.Forms;

namespace Sudoku_Solver
{
    public partial class SudokuForm : Form
    {
        public SudokuForm()
        {
            InitializeComponent();
            
            dataBoard.Rows.Add(9);
            dataBoard.Rows[2].DividerHeight = 2;
            dataBoard.Rows[5].DividerHeight = 2;
        }

        private void dataBoard_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.FormattedValue.ToString().Length > 0)
            {
                char input = e.FormattedValue.ToString()[0];
                int intInput = 0;
                bool isDigit = int.TryParse(input.ToString(), out intInput);

                if (!isDigit || (isDigit && intInput < 1))
                {
                    dataBoard.CancelEdit();
                }
            }
        }

        private void btnSolve_Click(object sender, EventArgs e)
        {
            btnSolve.Enabled = false;
            SolveThread.RunWorkerAsync();
        }

        private void UpdateGridView(int[][] board)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    dataBoard.Rows[i].Cells[j].Value = board[i][j] == 0 ? " " : board[i][j].ToString();
                }
            }
        }

        private int[][] ExtractBoard()
        {
            int[][] board = new int[9][];

            for (int i = 0; i < 9; i++)
            {
                board[i] = new int[9];
                for (int j = 0; j < 9; j++)
                {
                    string cellValue = dataBoard.Rows[i].Cells[j].Value as string;
                    if (!String.IsNullOrWhiteSpace(cellValue))
                    {
                        board[i][j] = int.Parse(cellValue);
                    }
                    else
                    {
                        board[i][j] = 0;
                    }
                }
            }
            return board;
        }

        private void backgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            int[][] board = ExtractBoard();
            Sudoku.Solve(ref board);
            UpdateGridView(board);
        }

        private void SolveThread_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            btnSolve.Enabled = true;
        }
    }
}
