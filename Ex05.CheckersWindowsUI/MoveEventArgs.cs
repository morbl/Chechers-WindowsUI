using System;
using Ex05.CheckersLogic;

namespace Ex05.CheckersWindowsUI
{
    public class MoveEventArgs : EventArgs
    {
        private Coordinate m_Start;
        private Coordinate m_Finish;

        public MoveEventArgs(Coordinate i_Start, Coordinate i_Finish)
        {
            m_Start = i_Start;
            m_Finish = i_Finish;
        }

        public Coordinate Start
        {
            get
            {
                return m_Start;
            }
            set
            {
                m_Start = value;
            }
        }

        public Coordinate Finish
        {
            get
            {
                return m_Finish;
            }
            set
            {
                m_Finish = value;
            }
        }
    }
}
