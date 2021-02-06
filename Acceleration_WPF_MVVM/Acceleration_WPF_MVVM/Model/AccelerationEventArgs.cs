using System;
using System.Collections.Generic;
using System.Text;
using Acceleration_WPF_MVVM.Persistance;
namespace Acceleration_WPF_MVVM.Model
{
    public class AccelerationEventArgs : EventArgs
    {
        public Pair m_coordinates;
        public Pair m_dimensions;
        
        //SETTEREKRE ÍRJUK

        public AccelerationEventArgs(Pair coordinates, Pair dimensions)
        {
            m_coordinates = coordinates;
            m_dimensions = dimensions;
        }
    }
}
