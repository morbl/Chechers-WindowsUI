using System.Collections.Generic;

namespace Ex05.CheckersLogic
{
    public class Board
    {
        private Pawn[,] m_PlayBoard;
        private int m_SizeOfBoard;
        private List<Pawn> m_PlayerOnePawns = new List<Pawn>();
        private List<Pawn> m_PlayerTwoPawns = new List<Pawn>();
        
        public Board(int i_SizeOfBoard)
        {
            m_PlayBoard = new Pawn[i_SizeOfBoard, i_SizeOfBoard];
            initBoard(i_SizeOfBoard);
            m_SizeOfBoard = i_SizeOfBoard;
        }

        public int SizeOfBoard
        {
            get
            {
                return m_SizeOfBoard;
            }

            set
            {
                m_SizeOfBoard = value;
            }
        }

        public List<Pawn> PlayerOnePawns
        {
            get
            {
                return m_PlayerOnePawns;
            }

            set
            {
                m_PlayerOnePawns = value;
            }
        }

        public List<Pawn> PlayerTwoPawns
        {
            get
            {
                return m_PlayerTwoPawns;
            }

            set
            {
                m_PlayerTwoPawns = value;
            }
        }

        public Pawn[,] MatrixOfBoard
        {
            get
            {
                return m_PlayBoard;
            }

            set
            {
                m_PlayBoard = value;
            }
        }

        // Initialize the board when created
        private void initBoard(int i_SizeOfBoard)
        {
            int i_IndexRow;

            for(int i = 0; i < i_SizeOfBoard / 2 - 1; i++)
            {
                if(i % 2 == 0)
                {
                    i_IndexRow = 1;
                }
                else
                {
                    {
                        i_IndexRow = 0;
                    }
                }

                for(int j = i_IndexRow; j < i_SizeOfBoard; j += 2)
                {
                    m_PlayBoard[i,j] = new Pawn(Player.eLetterType.O,i,j);
                    m_PlayerTwoPawns.Add(m_PlayBoard[i, j]);
                    if(j != 0)
                    {
                        m_PlayBoard[i_SizeOfBoard - 1 - i, j - 1] = new Pawn(Player.eLetterType.X, i_SizeOfBoard - 1 - i, j - 1);
                        m_PlayerOnePawns.Add(m_PlayBoard[i_SizeOfBoard - 1 - i, j - 1]);
                    }
                }
                if (i_IndexRow == 0)
                {
                    m_PlayBoard[i_SizeOfBoard - 1 - i, i_SizeOfBoard - 1] = new Pawn(Player.eLetterType.X, i_SizeOfBoard - 1 - i, i_SizeOfBoard - 1);
                    m_PlayerOnePawns.Add(m_PlayBoard[i_SizeOfBoard - 1 - i, i_SizeOfBoard - 1]);
                }
            }
        }

        // Given the current coordinate and the possible coordinate and move the pawn there
        public void MovePawn(Coordinate i_Current, Coordinate i_ToMove)
        {
            m_PlayBoard[i_ToMove.CoordinateRow, i_ToMove.CoordinateCol] = m_PlayBoard[i_Current.CoordinateRow, i_Current.CoordinateCol];
            m_PlayBoard[i_ToMove.CoordinateRow, i_ToMove.CoordinateCol].Location = i_ToMove;
            m_PlayBoard[i_Current.CoordinateRow, i_Current.CoordinateCol] = null;
        }

        // Delets pawn from board
        public void DeletePawn(int i_Row, int i_Col, Player i_CurrentPlayer)
        {
            Pawn tempPawn = m_PlayBoard[i_Row, i_Col];
           
            if(i_CurrentPlayer.PlayerLetterType == Player.eLetterType.X)
            {
                m_PlayerTwoPawns.Remove(tempPawn);
            }
            else
            {
                m_PlayerOnePawns.Remove(tempPawn);
            }

            m_PlayBoard[i_Row, i_Col] = null;
        }
    }
}
