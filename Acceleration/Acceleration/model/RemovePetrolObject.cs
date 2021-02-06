using System;
using System.Collections.Generic;
using System.Text;
using Acceleration.Persistence;
namespace Acceleration.model
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
