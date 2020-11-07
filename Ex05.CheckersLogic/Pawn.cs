
namespace Ex05.CheckersLogic
{
    public class Pawn
    {
        // Enum for pawn type
        public enum ePawnType
        {
            X,
            O,
            U,  //king of O
            K   //King of X
        }

        private ePawnType m_PawnType;
        private Player.eLetterType m_PlayerLetter;
        private bool m_CanEat;
        private Coordinate m_Location;

        public Pawn(Player.eLetterType i_PlayerLetter,int i_Row,int i_Col)
        {
            m_PlayerLetter = i_PlayerLetter;
            m_PawnType = (ePawnType)i_PlayerLetter;
            m_Location = new Coordinate(i_Row,i_Col);
        }

        public Player.eLetterType LetterOfPlayer
        {
            get
            {
                return m_PlayerLetter;
            }

            set
            {
                m_PlayerLetter = value;
            }
        }

        public ePawnType LetterOfPawn
        {
            get
            {
                return m_PawnType;
            }

            set
            {
                m_PawnType = value;
            }
        }

        public bool CanEat
        {
            get
            {
                return m_CanEat;
            }

            set
            {
                m_CanEat = value;
            }
        }

        public Coordinate Location
        {
            get
            {
                return m_Location;
            }

            set
            {
                m_Location = value;
            }
        }
    }
}
