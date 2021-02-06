using System;
using System.Collections.Generic;
using System.Text;

namespace Acceleration.model
{
    public class RefreshTimeOnScreen : EventArgs
    {
        public int m_time;
        public int m_petrol;
        //public int m_runTimeOfMotor;

        public RefreshTimeOnScreen(int time, int petrol)
        {
            m_time = time;
            m_petrol = petrol;
          //  m_runTimeOfMotor = runTime;
        }
    }
}
