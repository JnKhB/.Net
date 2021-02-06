using System;
using System.Collections.Generic;
using System.Text;
using Acceleration_WPF_MVVM.Persistance;
namespace Acceleration_WPF_MVVM.Model
{
    public class RemovePetrolObject : EventArgs
    {
        public PetrolObject m_petrolObject;
        public RemovePetrolObject(PetrolObject petrolObject )
        {
            m_petrolObject = petrolObject; 
        }
    }
}
