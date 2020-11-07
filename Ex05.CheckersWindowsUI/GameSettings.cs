using System;
using System.Drawing;
using System.Windows.Forms;

namespace Ex05.CheckersWindowsUI
{
    public partial class GameSettings : Form
    {
        private const int k_BigBoard = 10;
        private const int k_MediumBoard = 8;
        private const int k_SmallBoard = 6;

        public GameSettings()
        {
            InitializeComponent();
        }

        public string Player1Name
        {
            get
            {
                return Player1TextBox.Text;
            }
            set
            {
                Player1TextBox.Text = value;
            }
        }

        public string Player2Name
        {
            get
            {
                return Player2TextBox.Text;
            }
            set
            {
                Player2TextBox.Text = value;
            }
        }

        public int BoardSize
        {
            get
            {
                if(Size6RadioButton.Checked)
                {
                    return k_SmallBoard;
                }
                else if(Size8RadioButton.Checked)
                {
                    return k_MediumBoard;
                }
                else
                {
                    return k_BigBoard;
                }
            }
        }

        private void Player2CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Player2TextBox.Enabled = Player2TextBox.Enabled == true ? false : true;
            if(Player2TextBox.Enabled == true)
            {
                this.Player2TextBox.BackColor = Color.White;
                this.Player2TextBox.Text = String.Empty;
            }
            else
            {
                this.Player2TextBox.BackColor = System.Drawing.SystemColors.Control;
                this.Player2TextBox.Text = @"[Computer]";
            }
        }

        private void DoneButton_Click(object sender, EventArgs e)
        {
            if((string.IsNullOrEmpty(Player1TextBox.Text)) || (string.IsNullOrEmpty(Player2TextBox.Text)))
            {
                MessageBox.Show(@"Please fill all the fields");
            }
            else
            {
                this.Close();
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}

