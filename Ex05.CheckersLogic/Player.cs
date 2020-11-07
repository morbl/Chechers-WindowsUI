
namespace Ex05.CheckersLogic
{
    public class Player
    {
        // Enum for player type computer ot Person
        public enum ePlayerType
        {
            Computer = 1,
            Person = 2
        }

        // Enum For Which letter is player - given hard codedly 
        public enum eLetterType
        {
            X,
            O
        }

        // private members
        private string m_UserName;
        private ePlayerType m_PlayerType;
        private int m_ScoreOfPlayer;
        private eLetterType m_LetterType;

        // ctor - gets username player type and the players numbers the letter is set automatically
        public  Player(string i_UserName, int i_PlayerType, int i_NumberOfPlayer)
        {
            m_PlayerType = (ePlayerType)i_PlayerType;
            m_UserName = i_UserName;
            m_ScoreOfPlayer = 0;
            m_LetterType = i_NumberOfPlayer == 1 ? eLetterType.X : eLetterType.O;
        }

        public string NameOfPlayer
        {
            get
            {
                return m_UserName;
            }

            set
            {
                m_UserName = value;
            }
        }

        public eLetterType PlayerLetterType
        {
            get
            {
                return m_LetterType;
            }

            set
            {
                m_LetterType = value;
            }
        }

        public int PlayerScore
        {
            get
            {
                return m_ScoreOfPlayer;
            }

            set
            {
                m_ScoreOfPlayer = value;
            }
        }

        public ePlayerType PlayerType
        {
            get
            {
                return m_PlayerType;
            }

            set
            {
                m_PlayerType = value;
            }
        }
    }
}
