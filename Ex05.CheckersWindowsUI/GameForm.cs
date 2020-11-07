using System;
using System.Drawing;
using System.Windows.Forms;
using Ex05.CheckersLogic;

namespace Ex05.CheckersWindowsUI
{
    public partial class GameForm : Form
    {
        public EventHandler MoveCoordinateEntered;
        private const int k_SizeOfButton = 40;
        private readonly Button[,] r_GameBoard;
        private bool m_IsButtonSelected;
        private Button m_MoveButton;

        public GameForm(int i_SizeBoard)
        {
            InitializeComponent();
            this.Size = new Size(i_SizeBoard * k_SizeOfButton + 40, i_SizeBoard * k_SizeOfButton + 90);
            r_GameBoard = new Button[i_SizeBoard, i_SizeBoard];
            initialBoard(i_SizeBoard);
        }

        public string Player1Name
        {
            get
            {
                return Player1LabelBoard.Text;
            }
            set
            {
                Player1LabelBoard.Text = value;
            }
        }

        public string PlayerOneScore
        {
            get
            {
                return ScorePlayer1.Text;
            }
            set
            {
                ScorePlayer1.Text = value;
            }
        }

        public string PlayerTwoScore
        {
            get
            {
                return ScorePlayer2.Text;
            }
            set
            {
                ScorePlayer2.Text = value;
            }
        }

        public string Player2Name
        {
            get
            {
                return Player2LableBoard.Text;
            }
            set
            {
                Player2LableBoard.Text = value;
            }
        }

        public Button GetButtonByCoordinate(int i_IIndex, int i_JIndex)
        {
            return r_GameBoard[i_IIndex, i_JIndex];
        }

        private void initialBoard(int i_SizeBoard)
        {
            for(int i = 0; i < i_SizeBoard; i++)
            {
                for(int j = 0; j < i_SizeBoard; j++)
                {
                    Button buttonBoard = createButton(i,j);
                    buttonBoard.TabStop = false;
                    buttonBoard.Tag = new Coordinate(i, j);
                    Controls.Add(buttonBoard);
                    r_GameBoard[i, j] = buttonBoard;
                }
            }
        }

        private Button createButton(int i_Row, int i_Column)
        {
            Button buttonBoard = new Button();
            buttonBoard.Size = new Size(k_SizeOfButton, k_SizeOfButton);
            buttonBoard.Location = new Point(i_Column * k_SizeOfButton + 12, i_Row * k_SizeOfButton + 40);
            if(isDarkBrownButtonInBoard(i_Row, i_Column) == true)
            {
                buttonBoard.BackColor = Color.Chocolate;
                buttonBoard.Enabled = false;
            }
            else
            {
                buttonBoard.BackColor = Color.Beige;
                buttonBoard.Click += new EventHandler(m_ButtonBoard_Click);
            }

            return buttonBoard;
        }

        private bool isDarkBrownButtonInBoard(int i_Row, int i_Column)
        {
            return ((i_Row % 2 == 0 && i_Column % 2 == 0) || (i_Row % 2 == 1 && i_Column % 2 == 1));
        }

        public void SetSizeToLabelGameForm()
        {
            this.Player1LabelBoard.Location = new Point(this.Size.Width / 2 - k_SizeOfButton * 2, 18);
            this.Player2LableBoard.Left = this.ScorePlayer1.Right + k_SizeOfButton + 10;
            this.ScorePlayer1.Left = this.Player1LabelBoard.Left + this.Player1LabelBoard.Width + 3;
            this.ScorePlayer2.Left = this.Player2LableBoard.Right + 3;
        }

        public void m_ButtonBoard_Click(object sender, EventArgs e)
        {
            Button currentButton = sender as Button;
            if(currentButton != null)
            {
                if (m_IsButtonSelected == false)
                {
                    currentButton.BackColor = Color.CornflowerBlue;
                    m_IsButtonSelected = true;
                    m_MoveButton = currentButton;
                }
                else
                {
                    if(currentButton.Tag == m_MoveButton.Tag)
                    {
                        currentButton.BackColor = Color.Beige;
                        m_IsButtonSelected = false;
                        m_MoveButton = null;
                    }
                    else
                    {
                        // save the argument and send to the  move function
                        MoveEventArgs moveArguments = new MoveEventArgs((Coordinate)m_MoveButton.Tag,(Coordinate)currentButton.Tag);
                        OnMoveEntered(moveArguments);
                        m_MoveButton.BackColor = Color.Beige;
                        m_IsButtonSelected = false;
                        m_MoveButton = null;
                    }
                }
            }
        }

        protected virtual void OnMoveEntered(MoveEventArgs i_Event)
        {
            if(MoveCoordinateEntered != null)
            {
                MoveCoordinateEntered(this, i_Event);
            }
        }

        public void SetColorLabel(int i_LabelToChange)
        {
            if(i_LabelToChange == 1)
            {
                Player1LabelBoard.BackColor = Color.Khaki;
                Player2LableBoard.BackColor = Color.SeaShell;
            }
            else
            {
                Player1LabelBoard.BackColor = Color.SeaShell; 
                Player2LableBoard.BackColor = Color.Khaki;
            }
        }

        private void Damka_Load(object sender, EventArgs e)
        {
            this.Icon = Properties.Resources.package_games_board;
        }
    }
}
