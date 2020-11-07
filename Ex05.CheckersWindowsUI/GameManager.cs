using System;
using System.Drawing;
using System.IO;
using System.Media;
using System.Text;
using System.Windows.Forms;
using Ex05.CheckersLogic;

namespace Ex05.CheckersWindowsUI
{
    public class GameManager
    {
        private Game m_GameLogic;
        private GameForm m_GameWindows;
        private const int k_SizeOfImage = 26;

        public GameManager(GameSettings i_GameSettings)
        {
            if(i_GameSettings.DialogResult == DialogResult.OK)
            {
                //Backend
                initBackend(i_GameSettings);

                //Fronted
                initFrontend(m_GameLogic.BoardMatrix, m_GameLogic);
                m_GameWindows.MoveCoordinateEntered += new EventHandler(m_FormGame_MoveEntered);
                assignBackendEvent();
                m_GameWindows.ShowDialog();
            }
        }

        private void assignBackendEvent()
        {
            m_GameLogic.AfterGameEnded += new EventHandler(m_GameLogic_GameEnded);
            m_GameLogic.GameUpdatedBoard += new EventHandler(m_GameLogic_GameUpdateBoard);
            m_GameLogic.NextPlayerTurn += new EventHandler(m_GameLogic_NextPlayerTurn);
        }

        public void m_GameLogic_NextPlayerTurn(object sender, EventArgs e)
        {
            Game currentGame = sender as Game;
            int numOfPlayer = currentGame.CurrPlayer == Game.ePlayerNum.Player1 ? 1 : 2;
            MarkNextPlayer(numOfPlayer);
        }

        public void MarkNextPlayer(int i_NumOfCurPlayer)
        {
            m_GameWindows.SetColorLabel(i_NumOfCurPlayer);
        }

        public void m_GameLogic_GameUpdateBoard(object sender, EventArgs e)
        {
            Game currentGame = sender as Game;
            UpDateFrontBoard(currentGame.BoardMatrix);
        }

        public void m_GameLogic_GameEnded(object sender, EventArgs e)
        {
            Game currentGame = sender as Game;
            StringBuilder message = new StringBuilder();

            if(currentGame.Tie == true)
            {
                message.Append(@"Tie! ");
            }
            else
            {
                string name = currentGame.OWin == true ? currentGame.PlayerTwo.NameOfPlayer : currentGame.PlayerOne.NameOfPlayer;
                message.Append($@"{name} Won! ");
                makeWinSound();
            }

            message.Append(@"
Another Round?");
            
            DialogResult dialog = MessageBox.Show(message.ToString(), "Damka", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(dialog == DialogResult.Yes)
            {
                AnotherGame();
            }
            else
            {
               m_GameWindows.Close();
            }
        }

        private void makeWinSound()
        {
            Stream str = Properties.Resources.Ta_Da_SoundBible_com_1884170640;
            SoundPlayer soundOfWin = new SoundPlayer(str);
            soundOfWin.Play();
        }

        public void AnotherGame()
        {
            m_GameLogic = new Game(m_GameLogic.PlayerOne, m_GameLogic.PlayerTwo,new Board(m_GameLogic.BoardMatrix.SizeOfBoard), 1);
            UpDateFrontBoard(m_GameLogic.BoardMatrix);
            m_GameWindows.PlayerOneScore = m_GameLogic.PlayerOne.PlayerScore.ToString();
            m_GameWindows.PlayerTwoScore = m_GameLogic.PlayerTwo.PlayerScore.ToString();
            assignBackendEvent();
            MarkNextPlayer(1);
        }

        private void initBackend(GameSettings i_GameSettings)
        {
            string nameOfPlayerOne = i_GameSettings.Player1Name;
            int gameType = i_GameSettings.Player2Name == "[Computer]" ? 1 : 2;
            string nameOfPlayerTwo = (gameType == 1) ? "Computer" : i_GameSettings.Player2Name;

            Player player1 = new Player(nameOfPlayerOne, 2, 1);
            Player player2 = new Player(nameOfPlayerTwo, gameType, 2);
            Board checkersBoard = new Board(i_GameSettings.BoardSize);

            m_GameLogic = new Game(player1, player2, checkersBoard, 1);
        }

        private void initFrontend(Board i_BoardMatrix, Game i_GameLogic)
        {
            m_GameWindows = new GameForm(i_BoardMatrix.SizeOfBoard);
            UpDateFrontBoard(i_BoardMatrix);
            UpdateLabels(i_GameLogic);
        }

        public void UpdateLabels(Game i_GameLogic)
        {
            m_GameWindows.Player1Name = string.Format($@"{i_GameLogic.PlayerOne.NameOfPlayer}:");
            m_GameWindows.Player2Name = string.Format($@"{i_GameLogic.PlayerTwo.NameOfPlayer}:");
            m_GameWindows.SetSizeToLabelGameForm();
        }

        public void UpDateFrontBoard(Board i_BoardMatrix)
        {
            for (int i = 0; i < i_BoardMatrix.SizeOfBoard; i++)
            {
                for (int j = 0; j < i_BoardMatrix.SizeOfBoard; j++)
                {
                    setPawnInBoard(i, j, i_BoardMatrix);
                }
            }
        }

        private void setPawnInBoard(int i_IIndex, int i_JIndex, Board i_BoardMatrix)
        {
            if (i_BoardMatrix.MatrixOfBoard[i_IIndex, i_JIndex] != null)
            {
                if (i_BoardMatrix.MatrixOfBoard[i_IIndex, i_JIndex].LetterOfPawn == Pawn.ePawnType.X)
                {
                    m_GameWindows.GetButtonByCoordinate(i_IIndex, i_JIndex).Image = new Bitmap(Properties.Resources.blackPwan, new Size(k_SizeOfImage, k_SizeOfImage));
                    m_GameWindows.GetButtonByCoordinate(i_IIndex, i_JIndex).BackgroundImageLayout = ImageLayout.Stretch;
                }
                else if (i_BoardMatrix.MatrixOfBoard[i_IIndex, i_JIndex].LetterOfPawn == Pawn.ePawnType.O)
                {
                    m_GameWindows.GetButtonByCoordinate(i_IIndex, i_JIndex).Image = new Bitmap(Properties.Resources.whitePwanO, new Size(k_SizeOfImage, k_SizeOfImage));
                    m_GameWindows.GetButtonByCoordinate(i_IIndex, i_JIndex).BackgroundImageLayout = ImageLayout.Stretch;
                }
                else if (i_BoardMatrix.MatrixOfBoard[i_IIndex, i_JIndex].LetterOfPawn == Pawn.ePawnType.K)
                {
                    m_GameWindows.GetButtonByCoordinate(i_IIndex, i_JIndex).Image = new Bitmap(Properties.Resources.kingBlackPwan, new Size(k_SizeOfImage, k_SizeOfImage));
                    m_GameWindows.GetButtonByCoordinate(i_IIndex, i_JIndex).BackgroundImageLayout = ImageLayout.Stretch;
                }
                else
                {
                    m_GameWindows.GetButtonByCoordinate(i_IIndex, i_JIndex).Image = new Bitmap(Properties.Resources.kingWhitePwan, new Size(k_SizeOfImage, k_SizeOfImage));
                    m_GameWindows.GetButtonByCoordinate(i_IIndex, i_JIndex).BackgroundImageLayout = ImageLayout.Stretch;
                }

            }
            else
            {
                m_GameWindows.GetButtonByCoordinate(i_IIndex, i_JIndex).Image = null;
            }
        }

        private void m_FormGame_MoveEntered(object sender, EventArgs e)
        {
            MoveEventArgs eventArgs = e as MoveEventArgs;
           
            bool isLegalMove = m_GameLogic.IsLegalMove(eventArgs.Start,eventArgs.Finish);
            
            if(isLegalMove == false)
            {
                MessageBox.Show(@"Illegal movement! Please try again.");
            }
        }
    }
}
