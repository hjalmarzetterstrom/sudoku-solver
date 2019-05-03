using System;
using System.Windows.Forms;

namespace Sudoku_Solver
{
    public partial class SudokuForm : Form
    {
        public SudokuForm()
        {
            InitializeComponent();

            //Empty
            dataBoard.Rows.Add(9);

            //Easy Game (4,446,217,226)
            //Easy();

            //Medium Game (5,240,803,271)
            //Medium();

            //Hard Game (7,872,053,175)
            //Hard();

            //Evil Game (8,030,553,668)
            //Evil();

            //Evil Game (1,733,782,577)
            //Evil2();

            dataBoard.Rows[2].DividerHeight = 2;
            dataBoard.Rows[5].DividerHeight = 2;
        }

        #region Games
        private void Evil2()
        {
            dataBoard.Rows.Add(" ", "2", " ", " ", " ", " ", " ", " ", "9");
            dataBoard.Rows.Add(" ", " ", " ", " ", "7", "4", " ", " ", "3");
            dataBoard.Rows.Add(" ", " ", "4", " ", "2", "8", " ", " ", " ");
            dataBoard.Rows.Add(" ", " ", "9", " ", " ", " ", "1", "5", " ");
            dataBoard.Rows.Add("2", " ", " ", " ", "1", " ", " ", " ", "7");
            dataBoard.Rows.Add(" ", "1", "3", " ", " ", " ", "4", " ", " ");
            dataBoard.Rows.Add(" ", " ", " ", "1", "3", " ", "7", " ", " ");
            dataBoard.Rows.Add("4", " ", " ", "6", "5", " ", " ", " ", " ");
            dataBoard.Rows.Add("1", " ", " ", " ", " ", " ", " ", "2", " ");
        }

        private void Evil()
        {
            dataBoard.Rows.Add(" ", " ", " ", "4", " ", " ", " ", " ", "3");
            dataBoard.Rows.Add(" ", " ", "8", " ", " ", "9", " ", " ", "5");
            dataBoard.Rows.Add(" ", " ", "1", " ", "8", " ", " ", " ", "2");
            dataBoard.Rows.Add("8", " ", "4", " ", " ", "7", "2", " ", " ");
            dataBoard.Rows.Add(" ", "6", " ", " ", " ", " ", " ", "9", " ");
            dataBoard.Rows.Add(" ", " ", "5", "6", " ", " ", "8", " ", "7");
            dataBoard.Rows.Add("2", " ", " ", " ", "9", " ", "1", " ", " ");
            dataBoard.Rows.Add("4", " ", " ", "1", " ", " ", "3", " ", " ");
            dataBoard.Rows.Add("6", " ", " ", " ", " ", "2", " ", " ", " ");
        }

        private void Hard()
        {
            dataBoard.Rows.Add(" ", " ", "4", " ", "9", " ", " ", "2", " ");
            dataBoard.Rows.Add(" ", "7", "6", " ", "3", " ", " ", " ", "9");
            dataBoard.Rows.Add("1", " ", " ", " ", "6", " ", " ", " ", "7");
            dataBoard.Rows.Add(" ", " ", "9", "6", " ", " ", " ", " ", " ");
            dataBoard.Rows.Add("5", " ", " ", "9", " ", "4", " ", " ", "1");
            dataBoard.Rows.Add(" ", " ", " ", " ", " ", "5", "9", " ", " ");
            dataBoard.Rows.Add("9", " ", " ", " ", "5", " ", " ", " ", "6");
            dataBoard.Rows.Add("3", " ", " ", " ", "8", " ", "4", "9", " ");
            dataBoard.Rows.Add(" ", "6", " ", " ", "4", " ", "1", " ", " ");
        }

        private void Medium()
        {
            dataBoard.Rows.Add(" ", "2", "3", "7", " ", "4", " ", " ", " ");
            dataBoard.Rows.Add("7", " ", " ", " ", " ", " ", "4", "3", " ");
            dataBoard.Rows.Add("6", " ", " ", " ", "8", "1", "2", " ", " ");
            dataBoard.Rows.Add(" ", " ", "8", " ", "1", " ", "6", " ", " ");
            dataBoard.Rows.Add("1", " ", " ", "4", " ", "7", " ", " ", "3");
            dataBoard.Rows.Add(" ", " ", "2", " ", "3", " ", "5", " ", " ");
            dataBoard.Rows.Add(" ", " ", "7", "9", "2", " ", " ", " ", "5");
            dataBoard.Rows.Add(" ", "9", "5", " ", " ", " ", " ", " ", "2");
            dataBoard.Rows.Add(" ", " ", " ", "8", " ", "5", "3", "6", " ");
        }

        private void Easy()
        {
            dataBoard.Rows.Add("1", " ", " ", "9", "5", " ", "4", " ", " ");
            dataBoard.Rows.Add(" ", " ", "9", " ", " ", " ", " ", "7", " ");
            dataBoard.Rows.Add(" ", "2", "8", "6", "7", " ", " ", " ", " ");
            dataBoard.Rows.Add("9", " ", " ", " ", "6", "8", " ", "5", "7");
            dataBoard.Rows.Add(" ", "5", "6", " ", "3", " ", "2", "1", " ");
            dataBoard.Rows.Add("3", "7", " ", "5", "1", " ", " ", " ", "6");
            dataBoard.Rows.Add(" ", " ", " ", " ", "8", "7", "5", "2", " ");
            dataBoard.Rows.Add(" ", "4", " ", " ", " ", " ", "9", " ", " ");
            dataBoard.Rows.Add(" ", " ", "5", " ", "9", "6", " ", " ", "4");
        } 
        #endregion

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
            backgroundWorker.RunWorkerAsync();
        }

        private void UpdateGridView(Point[] board)
        {
            for (int i = 0, k = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++, k++)
                {
                    dataBoard.Rows[i].Cells[j].Value = board[k].Value == -1 ? " " : board[k].Value.ToString();
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
                        board[i][j] = -1;
                    }
                }
            }
            return board;
        }

        private void backgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            int[][] board = ExtractBoard();

            Sudoku newBoard = new Sudoku(board);
            newBoard.ShowProgress += UpdateGridView;
            newBoard.Solve();
            dataBoard.DefaultCellStyle.BackColor = newBoard.Correct ? System.Drawing.Color.LightGreen : System.Drawing.Color.LightPink;

            UpdateGridView(newBoard.Board);
        }
    }
}
