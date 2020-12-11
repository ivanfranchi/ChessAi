
namespace ChessAI
{
    partial class Form1
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
            this.btnPopulate = new System.Windows.Forms.Button();
            this.txtBoard = new System.Windows.Forms.TextBox();
            this.txtMove = new System.Windows.Forms.TextBox();
            this.btnMove = new System.Windows.Forms.Button();
            this.lblError = new System.Windows.Forms.Label();
            this.lblScoreText = new System.Windows.Forms.Label();
            this.lblScore = new System.Windows.Forms.Label();
            this.lblMovesCounter = new System.Windows.Forms.Label();
            this.lblMovesText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnPopulate
            // 
            this.btnPopulate.Location = new System.Drawing.Point(13, 13);
            this.btnPopulate.Name = "btnPopulate";
            this.btnPopulate.Size = new System.Drawing.Size(75, 23);
            this.btnPopulate.TabIndex = 0;
            this.btnPopulate.Text = "Start";
            this.btnPopulate.UseVisualStyleBackColor = true;
            this.btnPopulate.Click += new System.EventHandler(this.btnPopulate_Click);
            // 
            // txtBoard
            // 
            this.txtBoard.Location = new System.Drawing.Point(13, 92);
            this.txtBoard.Multiline = true;
            this.txtBoard.Name = "txtBoard";
            this.txtBoard.Size = new System.Drawing.Size(357, 260);
            this.txtBoard.TabIndex = 1;
            // 
            // txtMove
            // 
            this.txtMove.Location = new System.Drawing.Point(175, 12);
            this.txtMove.Name = "txtMove";
            this.txtMove.Size = new System.Drawing.Size(100, 20);
            this.txtMove.TabIndex = 2;
            this.txtMove.Text = "0.1-0.2";
            // 
            // btnMove
            // 
            this.btnMove.Location = new System.Drawing.Point(94, 12);
            this.btnMove.Name = "btnMove";
            this.btnMove.Size = new System.Drawing.Size(75, 23);
            this.btnMove.TabIndex = 3;
            this.btnMove.Text = "Move";
            this.btnMove.UseVisualStyleBackColor = true;
            this.btnMove.Click += new System.EventHandler(this.btnMove_Click);
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Location = new System.Drawing.Point(282, 18);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(0, 13);
            this.lblError.TabIndex = 4;
            // 
            // lblScoreText
            // 
            this.lblScoreText.AutoSize = true;
            this.lblScoreText.Location = new System.Drawing.Point(377, 92);
            this.lblScoreText.Name = "lblScoreText";
            this.lblScoreText.Size = new System.Drawing.Size(38, 13);
            this.lblScoreText.TabIndex = 5;
            this.lblScoreText.Text = "Score:";
            // 
            // lblScore
            // 
            this.lblScore.AutoSize = true;
            this.lblScore.Location = new System.Drawing.Point(422, 92);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(0, 13);
            this.lblScore.TabIndex = 6;
            // 
            // lblMovesCounter
            // 
            this.lblMovesCounter.AutoSize = true;
            this.lblMovesCounter.Location = new System.Drawing.Point(422, 114);
            this.lblMovesCounter.Name = "lblMovesCounter";
            this.lblMovesCounter.Size = new System.Drawing.Size(13, 13);
            this.lblMovesCounter.TabIndex = 8;
            this.lblMovesCounter.Text = "0";
            // 
            // lblMovesText
            // 
            this.lblMovesText.AutoSize = true;
            this.lblMovesText.Location = new System.Drawing.Point(377, 114);
            this.lblMovesText.Name = "lblMovesText";
            this.lblMovesText.Size = new System.Drawing.Size(42, 13);
            this.lblMovesText.TabIndex = 7;
            this.lblMovesText.Text = "Moves:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblMovesCounter);
            this.Controls.Add(this.lblMovesText);
            this.Controls.Add(this.lblScore);
            this.Controls.Add(this.lblScoreText);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.btnMove);
            this.Controls.Add(this.txtMove);
            this.Controls.Add(this.txtBoard);
            this.Controls.Add(this.btnPopulate);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPopulate;
        private System.Windows.Forms.TextBox txtBoard;
        private System.Windows.Forms.TextBox txtMove;
        private System.Windows.Forms.Button btnMove;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.Label lblScoreText;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.Label lblMovesCounter;
        private System.Windows.Forms.Label lblMovesText;
    }
}

