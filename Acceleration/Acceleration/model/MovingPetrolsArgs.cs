using System;
using System.Collections.Generic;
using System.Text;
using Acceleration.Persistence; 

namespace Acceleration.model
{
    public class MovingPetrolsArgs : EventArgs
    {
        public List<PetrolObject> m_petrolObjects;

        public MovingPetrolsArgs(List<PetrolObject> petrolObject)
        {
            m_petrolObjects = petrolObject;
        }
    }
}
