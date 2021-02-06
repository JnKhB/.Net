using System;
using System.Collections.Generic;
using System.Text;
using Acceleration.Persistence;

namespace Acceleration.model
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
