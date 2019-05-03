namespace Sudoku_Solver
{
    partial class SudokuForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataBoard = new System.Windows.Forms.DataGridView();
            this.btnSolve = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.x1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.x2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.x3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.x4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.x5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.x6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.x7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.x8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.x9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataBoard)).BeginInit();
            this.SuspendLayout();
            // 
            // dataBoard
            // 
            this.dataBoard.AllowUserToAddRows = false;
            this.dataBoard.AllowUserToDeleteRows = false;
            this.dataBoard.AllowUserToResizeColumns = false;
            this.dataBoard.AllowUserToResizeRows = false;
            this.dataBoard.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataBoard.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataBoard.ColumnHeadersVisible = false;
            this.dataBoard.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.x1,
            this.x2,
            this.x3,
            this.x4,
            this.x5,
            this.x6,
            this.x7,
            this.x8,
            this.x9});
            this.dataBoard.GridColor = System.Drawing.Color.Black;
            this.dataBoard.Location = new System.Drawing.Point(12, 12);
            this.dataBoard.MultiSelect = false;
            this.dataBoard.Name = "dataBoard";
            this.dataBoard.RowHeadersVisible = false;
            this.dataBoard.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle10.NullValue = " ";
            this.dataBoard.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dataBoard.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataBoard.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("MS Reference Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataBoard.RowTemplate.Height = 40;
            this.dataBoard.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataBoard.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataBoard.Size = new System.Drawing.Size(363, 363);
            this.dataBoard.TabIndex = 0;
            this.dataBoard.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataBoard_CellValidating);
            // 
            // btnSolve
            // 
            this.btnSolve.FlatAppearance.BorderSize = 2;
            this.btnSolve.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnSolve.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnSolve.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSolve.Font = new System.Drawing.Font("MS Reference Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSolve.Location = new System.Drawing.Point(12, 378);
            this.btnSolve.Name = "btnSolve";
            this.btnSolve.Size = new System.Drawing.Size(178, 32);
            this.btnSolve.TabIndex = 1;
            this.btnSolve.Text = "S O L V E";
            this.btnSolve.UseVisualStyleBackColor = true;
            this.btnSolve.Click += new System.EventHandler(this.btnSolve_Click);
            // 
            // btnNext
            // 
            this.btnNext.FlatAppearance.BorderSize = 2;
            this.btnNext.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnNext.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Font = new System.Drawing.Font("MS Reference Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.Location = new System.Drawing.Point(197, 378);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(178, 32);
            this.btnNext.TabIndex = 2;
            this.btnNext.Text = "F I N D  N E X T";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // x1
            // 
            dataGridViewCellStyle1.NullValue = " ";
            this.x1.DefaultCellStyle = dataGridViewCellStyle1;
            this.x1.HeaderText = "x1";
            this.x1.MaxInputLength = 1;
            this.x1.Name = "x1";
            this.x1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.x1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.x1.Width = 40;
            // 
            // x2
            // 
            dataGridViewCellStyle2.NullValue = " ";
            this.x2.DefaultCellStyle = dataGridViewCellStyle2;
            this.x2.HeaderText = "x2";
            this.x2.MaxInputLength = 1;
            this.x2.Name = "x2";
            this.x2.Width = 40;
            // 
            // x3
            // 
            dataGridViewCellStyle3.NullValue = " ";
            this.x3.DefaultCellStyle = dataGridViewCellStyle3;
            this.x3.DividerWidth = 2;
            this.x3.HeaderText = "x3";
            this.x3.MaxInputLength = 1;
            this.x3.Name = "x3";
            this.x3.Width = 40;
            // 
            // x4
            // 
            dataGridViewCellStyle4.NullValue = " ";
            this.x4.DefaultCellStyle = dataGridViewCellStyle4;
            this.x4.HeaderText = "x4";
            this.x4.MaxInputLength = 1;
            this.x4.Name = "x4";
            this.x4.Width = 40;
            // 
            // x5
            // 
            dataGridViewCellStyle5.NullValue = " ";
            this.x5.DefaultCellStyle = dataGridViewCellStyle5;
            this.x5.HeaderText = "x5";
            this.x5.MaxInputLength = 1;
            this.x5.Name = "x5";
            this.x5.Width = 40;
            // 
            // x6
            // 
            dataGridViewCellStyle6.NullValue = " ";
            this.x6.DefaultCellStyle = dataGridViewCellStyle6;
            this.x6.DividerWidth = 2;
            this.x6.HeaderText = "x6";
            this.x6.MaxInputLength = 1;
            this.x6.Name = "x6";
            this.x6.Width = 40;
            // 
            // x7
            // 
            dataGridViewCellStyle7.NullValue = " ";
            this.x7.DefaultCellStyle = dataGridViewCellStyle7;
            this.x7.HeaderText = "x7";
            this.x7.MaxInputLength = 1;
            this.x7.Name = "x7";
            this.x7.Width = 40;
            // 
            // x8
            // 
            dataGridViewCellStyle8.NullValue = " ";
            this.x8.DefaultCellStyle = dataGridViewCellStyle8;
            this.x8.HeaderText = "x8";
            this.x8.MaxInputLength = 1;
            this.x8.Name = "x8";
            this.x8.Width = 40;
            // 
            // x9
            // 
            dataGridViewCellStyle9.NullValue = " ";
            this.x9.DefaultCellStyle = dataGridViewCellStyle9;
            this.x9.HeaderText = "x9";
            this.x9.MaxInputLength = 1;
            this.x9.Name = "x9";
            this.x9.Width = 40;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 416);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnSolve);
            this.Controls.Add(this.dataBoard);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "Form1";
            this.Text = "Sulvoku";
            ((System.ComponentModel.ISupportInitialize)(this.dataBoard)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataBoard;
        private System.Windows.Forms.Button btnSolve;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.DataGridViewTextBoxColumn x1;
        private System.Windows.Forms.DataGridViewTextBoxColumn x2;
        private System.Windows.Forms.DataGridViewTextBoxColumn x3;
        private System.Windows.Forms.DataGridViewTextBoxColumn x4;
        private System.Windows.Forms.DataGridViewTextBoxColumn x5;
        private System.Windows.Forms.DataGridViewTextBoxColumn x6;
        private System.Windows.Forms.DataGridViewTextBoxColumn x7;
        private System.Windows.Forms.DataGridViewTextBoxColumn x8;
        private System.Windows.Forms.DataGridViewTextBoxColumn x9;
    }
}

