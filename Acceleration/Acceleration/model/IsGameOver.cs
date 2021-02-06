using System;
using System.Collections.Generic;
using System.Text;

namespace Acceleration.model
{
    public class IsGameOver : EventArgs
    {
        private int m_time;

        public IsGameOver(int time)
        {
            m_time = time; 
        }
    }
}
