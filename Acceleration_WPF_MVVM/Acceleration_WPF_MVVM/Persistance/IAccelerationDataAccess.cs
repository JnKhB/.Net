using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Acceleration_WPF_MVVM.Model;
namespace Acceleration_WPF_MVVM.Persistance
{
    public interface IAccelerationDataAccess
    {
        Task<AccelerationGameModel> LoadAsync(String path);
        Task SaveAsync(String path, List<PetrolObject> petrolsList, MotorCycleObject motor, int time, Pair panelSize, int speed, int counter);
    }
}
