using System;
using System.Collections.Generic;
using System.Text;
using Acceleration_WPF_MVVM.Persistance; 

namespace Acceleration_WPF_MVVM.Model
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
