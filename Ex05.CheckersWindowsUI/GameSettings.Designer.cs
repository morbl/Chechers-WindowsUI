namespace Ex05.CheckersWindowsUI
{
    partial class GameSettings
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
            this.BoardSizeLable = new System.Windows.Forms.Label();
            this.Size6RadioButton = new System.Windows.Forms.RadioButton();
            this.Size8RadioButton = new System.Windows.Forms.RadioButton();
            this.Size10RadioButton = new System.Windows.Forms.RadioButton();
            this.PlayersLabel = new System.Windows.Forms.Label();
            this.Player1Label = new System.Windows.Forms.Label();
            this.Player1TextBox = new System.Windows.Forms.TextBox();
            this.Player2CheckBox = new System.Windows.Forms.CheckBox();
            this.Player2TextBox = new System.Windows.Forms.TextBox();
            this.DoneButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BoardSizeLable
            // 
            this.BoardSizeLable.AutoSize = true;
            this.BoardSizeLable.Location = new System.Drawing.Point(28, 22);
            this.BoardSizeLable.Name = "BoardSizeLable";
            this.BoardSizeLable.Size = new System.Drawing.Size(91, 20);
            this.BoardSizeLable.TabIndex = 0;
            this.BoardSizeLable.Text = "Board Size:";
            // 
            // Size6RadioButton
            // 
            this.Size6RadioButton.AutoSize = true;
            this.Size6RadioButton.Location = new System.Drawing.Point(52, 62);
            this.Size6RadioButton.Name = "Size6RadioButton";
            this.Size6RadioButton.Size = new System.Drawing.Size(67, 24);
            this.Size6RadioButton.TabIndex = 1;
            this.Size6RadioButton.TabStop = true;
            this.Size6RadioButton.Text = "6 x 6";
            this.Size6RadioButton.UseVisualStyleBackColor = true;
            // 
            // Size8RadioButton
            // 
            this.Size8RadioButton.AutoSize = true;
            this.Size8RadioButton.Location = new System.Drawing.Point(148, 62);
            this.Size8RadioButton.Name = "Size8RadioButton";
            this.Size8RadioButton.Size = new System.Drawing.Size(67, 24);
            this.Size8RadioButton.TabIndex = 2;
            this.Size8RadioButton.TabStop = true;
            this.Size8RadioButton.Text = "8 x 8";
            this.Size8RadioButton.UseVisualStyleBackColor = true;
            // 
            // Size10RadioButton
            // 
            this.Size10RadioButton.AutoSize = true;
            this.Size10RadioButton.Location = new System.Drawing.Point(243, 62);
            this.Size10RadioButton.Name = "Size10RadioButton";
            this.Size10RadioButton.Size = new System.Drawing.Size(85, 24);
            this.Size10RadioButton.TabIndex = 3;
            this.Size10RadioButton.TabStop = true;
            this.Size10RadioButton.Text = "10 x 10";
            this.Size10RadioButton.UseVisualStyleBackColor = true;
            // 
            // PlayersLabel
            // 
            this.PlayersLabel.AutoSize = true;
            this.PlayersLabel.Location = new System.Drawing.Point(28, 103);
            this.PlayersLabel.Name = "PlayersLabel";
            this.PlayersLabel.Size = new System.Drawing.Size(64, 20);
            this.PlayersLabel.TabIndex = 4;
            this.PlayersLabel.Text = "Players:";
            // 
            // Player1Label
            // 
            this.Player1Label.AutoSize = true;
            this.Player1Label.Location = new System.Drawing.Point(48, 138);
            this.Player1Label.Name = "Player1Label";
            this.Player1Label.Size = new System.Drawing.Size(65, 20);
            this.Player1Label.TabIndex = 5;
            this.Player1Label.Text = "Player1:";
            // 
            // Player1TextBox
            // 
            this.Player1TextBox.Location = new System.Drawing.Point(162, 132);
            this.Player1TextBox.Name = "Player1TextBox";
            this.Player1TextBox.Size = new System.Drawing.Size(166, 26);
            this.Player1TextBox.TabIndex = 6;
            // 
            // Player2CheckBox
            // 
            this.Player2CheckBox.AutoSize = true;
            this.Player2CheckBox.Location = new System.Drawing.Point(42, 182);
            this.Player2CheckBox.Name = "Player2CheckBox";
            this.Player2CheckBox.Size = new System.Drawing.Size(91, 24);
            this.Player2CheckBox.TabIndex = 7;
            this.Player2CheckBox.Text = "Player2:";
            this.Player2CheckBox.UseVisualStyleBackColor = true;
            this.Player2CheckBox.CheckedChanged += new System.EventHandler(this.Player2CheckBox_CheckedChanged);
            // 
            // Player2TextBox
            // 
            this.Player2TextBox.BackColor = System.Drawing.SystemColors.Control;
            this.Player2TextBox.Enabled = false;
            this.Player2TextBox.Location = new System.Drawing.Point(162, 180);
            this.Player2TextBox.Name = "Player2TextBox";
            this.Player2TextBox.Size = new System.Drawing.Size(166, 26);
            this.Player2TextBox.TabIndex = 8;
            this.Player2TextBox.Text = "[Computer]";
            // 
            // DoneButton
            // 
            this.DoneButton.BackColor = System.Drawing.SystemColors.ControlLight;
            this.DoneButton.Location = new System.Drawing.Point(208, 230);
            this.DoneButton.Name = "DoneButton";
            this.DoneButton.Size = new System.Drawing.Size(120, 35);
            this.DoneButton.TabIndex = 9;
            this.DoneButton.Text = "Done";
            this.DoneButton.UseVisualStyleBackColor = false;
            this.DoneButton.Click += new System.EventHandler(this.DoneButton_Click);
            // 
            // GameSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 286);
            this.Controls.Add(this.DoneButton);
            this.Controls.Add(this.Player2TextBox);
            this.Controls.Add(this.Player2CheckBox);
            this.Controls.Add(this.Player1TextBox);
            this.Controls.Add(this.Player1Label);
            this.Controls.Add(this.PlayersLabel);
            this.Controls.Add(this.Size10RadioButton);
            this.Controls.Add(this.Size8RadioButton);
            this.Controls.Add(this.Size6RadioButton);
            this.Controls.Add(this.BoardSizeLable);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GameSettings";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GameSettings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label BoardSizeLable;
        private System.Windows.Forms.RadioButton Size6RadioButton;
        private System.Windows.Forms.RadioButton Size8RadioButton;
        private System.Windows.Forms.RadioButton Size10RadioButton;
        private System.Windows.Forms.Label PlayersLabel;
        private System.Windows.Forms.Label Player1Label;
        private System.Windows.Forms.TextBox Player1TextBox;
        private System.Windows.Forms.CheckBox Player2CheckBox;
        private System.Windows.Forms.TextBox Player2TextBox;
        private System.Windows.Forms.Button DoneButton;
    }
}