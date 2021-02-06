using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Acceleration.model;
namespace Acceleration.Persistence
{
    public interface IAccelerationDataAccess
    {

        Task<AccelerationGameModel> LoadAsync(String path);
        Task SaveAsync(String path, List<PetrolObject> petrolsList, MotorCycleObject motor, int time, Pair panelSize, int speed, int counter);
    }
}
