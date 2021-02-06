using System;
using System.Collections.Generic;
using System.Text;
using Acceleration.Persistence;

namespace Acceleration.model
{
    public class AppearNewPetrolsArgs : EventArgs
    {
        public Pair m_coordinates;
        public Pair m_dimensions;
        public int m_id; 
        //SETTEREKRE ÍRJUK

        public AppearNewPetrolsArgs(Pair coordinates, Pair dimensions, int id)
        {
            m_coordinates = coordinates;
            m_dimensions = dimensions;
            m_id = id; 
        }
    }
}
