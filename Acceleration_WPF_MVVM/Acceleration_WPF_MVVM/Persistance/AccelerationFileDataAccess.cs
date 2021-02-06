using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.IO;
using Acceleration_WPF_MVVM.Model;

namespace Acceleration_WPF_MVVM.Persistance
{
    public class AccelerationFileDataAccess : IAccelerationDataAccess
    {
        public async Task<AccelerationGameModel> LoadAsync(String path)
        {
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    IAccelerationDataAccess dataAccess = new AccelerationFileDataAccess(); 
                    String panel = await reader.ReadLineAsync();
                    String[] panelCoords = panel.Split(' ');
                    Pair panelCoordinates = new Pair(Int32.Parse(panelCoords[0]), Int32.Parse(panelCoords[1])); 
                    String line = await reader.ReadLineAsync();
                    String[] numbers = line.Split(' ');
                    Int32 coordinate_x= Int32.Parse(numbers[0]);
                    Int32 coordinate_y = Int32.Parse(numbers[1]);
                    Pair coordinates = new Pair(coordinate_x, coordinate_y);
                    Int32 dimension_x = Int32.Parse(numbers[2]);
                    Int32 dimension_y = Int32.Parse(numbers[3]);
                    Pair dimensions = new Pair(dimension_x, dimension_y);
                    AccelerationGameModel model = new AccelerationGameModel(dataAccess, panelCoordinates, coordinates, dimensions);
                    model.m_motorCycle = new MotorCycleObject(coordinates, dimensions);
                    model.m_motorCycle.m_consumption = Int32.Parse(numbers[4]);
                    model.m_motorCycle.m_petrolQuantity = Int32.Parse(numbers[5]);
                    String numOfPetrols = await reader.ReadLineAsync();

                    for (Int32 i = 0; i < Int32.Parse(numOfPetrols); i++)
                    {
                        String line3 = await reader.ReadLineAsync();
                        String[] numbers2 = line3.Split(' ');
                        Int32 p_coordinate_x = Int32.Parse(numbers2[0]);
                        Int32 p_coordinate_y = Int32.Parse(numbers2[1]);
                        Pair p_coordinates = new Pair(p_coordinate_x, p_coordinate_y);
                        Int32 p_dimension_x = Int32.Parse(numbers2[2]);
                        Int32 p_dimension_y = Int32.Parse(numbers2[3]);
                        Pair p_dimensions = new Pair(p_dimension_x, p_dimension_y);
                        PetrolObject petrol = new PetrolObject(p_coordinates, p_dimensions);
                        petrol.m_id = Int32.Parse(numbers2[4]);
                        model.m_petrolsList.Add(petrol); 
                    }
                    String time = await reader.ReadLineAsync();
                    model.m_time = Int32.Parse(time);
                    String speed = await reader.ReadLineAsync();
                    model.m_speed = Int32.Parse(speed);
                    String counter = await reader.ReadLineAsync();
                    model.m_counter = Int32.Parse(counter);
                    return model;
                }
            }

            catch
            {
                throw new AccelerationException();
            }

        }
        public async Task SaveAsync(String path, List<PetrolObject> petrolsList, MotorCycleObject motor, int time, Pair panelSize, int speed, int counter)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(path)) // fájl megnyitása
                {
                    writer.Write(panelSize.m_x + " " + panelSize.m_y);
                    //új sor
                    await writer.WriteLineAsync();
                    //Motor
                    writer.Write(motor.m_coordinates.m_x + " ");
                    writer.Write(motor.m_coordinates.m_y + " ");
                    writer.Write(motor.m_dimensions.m_x + " ");
                    writer.Write(motor.m_dimensions.m_y + " ");
                    writer.Write(motor.m_consumption + " ");
                    writer.Write(motor.m_petrolQuantity);
                    //új sor
                    await writer.WriteLineAsync();
                    //petrolList
                    writer.Write(petrolsList.Count);
                    await writer.WriteLineAsync();
                    for (Int32 i = 0; i < petrolsList.Count; i++)
                    {
                        await writer.WriteAsync(petrolsList[i].m_coordinates.m_x + " ");
                        await writer.WriteAsync(petrolsList[i].m_coordinates.m_y + " ");
                        await writer.WriteAsync(petrolsList[i].m_dimensions.m_x + " ");
                        await writer.WriteAsync(petrolsList[i].m_dimensions.m_y + " ");
                        await writer.WriteAsync(petrolsList[i].m_id.ToString());
                        await writer.WriteLineAsync();
                    }
                    await writer.WriteAsync(time.ToString());
                    await writer.WriteLineAsync();
                    await writer.WriteAsync(speed.ToString());
                    await writer.WriteLineAsync();
                    await writer.WriteAsync(counter.ToString());
                }
            }
            catch
            { 
                throw new AccelerationException();
            }
        }
    }
}
