

namespace Ex05.CheckersLogic
{
    public struct Coordinate
    {
        private int m_Row;
        private int m_Col;

        public Coordinate(int i_Row, int i_Col)
        {
            m_Row = i_Row;
            m_Col = i_Col;
        }

        public int CoordinateRow
        {
            get
            {
                return m_Row;
            }

            set
            {
                m_Row = value;
            }
        }

        public int CoordinateCol
        {
            get
            {
                return m_Col;
            }

            set
            {
                m_Col = value;
            }
        }
    }
}
