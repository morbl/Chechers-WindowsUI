namespace Ex05.CheckersWindowsUI
{
    partial class GameForm
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
            this.components = new System.ComponentModel.Container();
            this.Player1LabelBoard = new System.Windows.Forms.Label();
            this.ScorePlayer1 = new System.Windows.Forms.Label();
            this.Player2LableBoard = new System.Windows.Forms.Label();
            this.ScorePlayer2 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // Player1LabelBoard
            // 
            this.Player1LabelBoard.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Player1LabelBoard.AutoSize = true;
            this.Player1LabelBoard.BackColor = System.Drawing.Color.Khaki;
            this.Player1LabelBoard.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.Player1LabelBoard.Location = new System.Drawing.Point(136, 27);
            this.Player1LabelBoard.Name = "Player1LabelBoard";
            this.Player1LabelBoard.Size = new System.Drawing.Size(73, 20);
            this.Player1LabelBoard.TabIndex = 0;
            this.Player1LabelBoard.Text = "Player1:";
            this.Player1LabelBoard.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ScorePlayer1
            // 
            this.ScorePlayer1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ScorePlayer1.AutoSize = true;
            this.ScorePlayer1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ScorePlayer1.Location = new System.Drawing.Point(224, 27);
            this.ScorePlayer1.Name = "ScorePlayer1";
            this.ScorePlayer1.Size = new System.Drawing.Size(19, 20);
            this.ScorePlayer1.TabIndex = 1;
            this.ScorePlayer1.Text = "0";
            // 
            // Player2LableBoard
            // 
            this.Player2LableBoard.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Player2LableBoard.AutoSize = true;
            this.Player2LableBoard.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.Player2LableBoard.Location = new System.Drawing.Point(271, 27);
            this.Player2LableBoard.Name = "Player2LableBoard";
            this.Player2LableBoard.Size = new System.Drawing.Size(73, 20);
            this.Player2LableBoard.TabIndex = 2;
            this.Player2LableBoard.Text = "Player2:";
            this.Player2LableBoard.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ScorePlayer2
            // 
            this.ScorePlayer2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ScorePlayer2.AutoSize = true;
            this.ScorePlayer2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.ScorePlayer2.Location = new System.Drawing.Point(350, 27);
            this.ScorePlayer2.Name = "ScorePlayer2";
            this.ScorePlayer2.Size = new System.Drawing.Size(19, 20);
            this.ScorePlayer2.TabIndex = 3;
            this.ScorePlayer2.Text = "0";
            this.ScorePlayer2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // Damka
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SeaShell;
            this.ClientSize = new System.Drawing.Size(523, 279);
            this.Controls.Add(this.ScorePlayer2);
            this.Controls.Add(this.Player2LableBoard);
            this.Controls.Add(this.ScorePlayer1);
            this.Controls.Add(this.Player1LabelBoard);
            this.Name = "GameForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Damka";
            this.Load += new System.EventHandler(this.Damka_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Player1LabelBoard;
        private System.Windows.Forms.Label ScorePlayer1;
        private System.Windows.Forms.Label Player2LableBoard;
        private System.Windows.Forms.Label ScorePlayer2;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}