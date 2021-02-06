using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;


namespace Acceleration_WPF_MVVM.Persistance
{


    public class MotorCycleObject : ScreenObject
    {
        public int m_consumption;
        public int m_petrolQuantity;
        //public int m_runTime;
        
        public MotorCycleObject(Pair coordinates, Pair dimensions) : base(coordinates, dimensions)
        {
            m_petrolQuantity = 1000;
            m_consumption = 1;
            //m_runTime = 0; 
        }
        public Pair getCoordinatesOfMotorCycle()
        {
            return m_coordinates;
        }

    }
}
