using System;
using System.Collections.Generic;
using System.Text;
using Acceleration_WPF_MVVM.Persistance;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Threading; 

namespace Acceleration_WPF_MVVM.Model
{
    public enum Direction { Right, Left }
    public class AccelerationGameModel
    {
        public IAccelerationDataAccess m_dataAccess;
        public MotorCycleObject m_motorCycle;
        public Pair m_motorDimension;
        public Pair m_petrolDimension;
        public List<PetrolObject> m_petrolsList = new List<PetrolObject>();
        public event EventHandler<AccelerationEventArgs> RefreshMotorPosition;
        public event EventHandler<AppearNewPetrolsArgs> AppearNewPetrols;
        public event EventHandler<MovingPetrolsArgs> RefreshPetrolsMoving;
        public event EventHandler<RefreshTimeOnScreen> RefreshTimeAndPetrol;
        public event EventHandler<RemovePetrolObject> RemovePetrolObject;
        public event EventHandler<IsGameOver> GameOver;
        public int stepQuantity; 
        private int m_petrolId = 1; 
        public Pair m_panelSize;
        public int m_time;
        public DispatcherTimer m_timerOfMoving;
        public DispatcherTimer m_timer; 
        public DispatcherTimer m_ticker;
        public DispatcherTimer m_accelerationTime;
        public bool m_isPaused;
        public bool m_loading;
        public int m_speed;
        public int m_counter;
        public AccelerationGameModel(IAccelerationDataAccess dataAccess, Pair panelSize, Pair motorDimension, Pair petrolDimension)
        {
            m_dataAccess = dataAccess; 
            m_panelSize = panelSize;
            m_motorDimension = motorDimension;
            m_petrolDimension = petrolDimension;
            m_timer = new DispatcherTimer();
            m_timerOfMoving = new DispatcherTimer();
            m_ticker = new DispatcherTimer();
            m_accelerationTime = new DispatcherTimer();
            m_ticker.Tick += On_SecTicking;
            m_timer.Tick += On_NewPetrol;
            m_accelerationTime.Tick += On_PetrolMoving;
            m_ticker.Tick += On_DecreaseFuel; 
            m_ticker.Interval = TimeSpan.FromSeconds(1);
            m_timer.Interval = TimeSpan.FromSeconds(3);
            m_timerOfMoving.Interval = TimeSpan.FromSeconds(1);
            m_accelerationTime.Interval = TimeSpan.FromMilliseconds(100);
            Pair coordinates = new Pair(m_panelSize.m_x - m_motorDimension.m_x, m_panelSize.m_y / 2 - m_motorDimension.m_y / 2);
            m_motorCycle = new MotorCycleObject(coordinates, m_motorDimension);
            stepQuantity = 10;
            m_loading = false; 
        }
        public void NewGame()
        {
            Pair coordinates = new Pair(m_panelSize.m_x - m_motorDimension.m_x, m_panelSize.m_y / 2 - m_motorDimension.m_y / 2);
            m_motorCycle = new MotorCycleObject(coordinates, m_motorDimension);
            if (this.RefreshMotorPosition != null)
            {
                this.RefreshMotorPosition(this, new AccelerationEventArgs(m_motorCycle.m_coordinates, m_motorCycle.m_dimensions));
            }
            
            m_motorCycle.m_petrolQuantity = 1000;
            m_motorCycle.m_consumption = 1; 
            m_speed = 10;
            m_counter = 0;
            m_petrolsList.Clear();
            m_time = 0;
            if (m_ticker != null)
                m_ticker.Stop();

            if (m_timer != null)
                m_timer.Stop();

            if (m_timerOfMoving != null)
                m_timerOfMoving.Stop();

            if (m_accelerationTime != null)
                m_accelerationTime.Stop();
            m_ticker.Interval = TimeSpan.FromSeconds(1);
            m_timer.Interval = TimeSpan.FromSeconds(3);
            m_timerOfMoving.Interval = TimeSpan.FromSeconds(1);
            m_accelerationTime.Interval = TimeSpan.FromMilliseconds(100);

            m_accelerationTime.Stop();
            m_accelerationTime.Start();

            m_ticker.Stop();
            m_ticker.Start();

            m_timerOfMoving.Stop();
            m_timerOfMoving.Start();

            m_timer.Stop();
            m_timer.Start();
            On_RefreshTimeAndPetrol();
        }
        public void Accelaration()
        {   
            if (m_time % 3 == 0)
            {
                if(m_speed > 1)
                {
                    m_speed--;
                }
                GetMotorCycleObject().m_consumption += 2;
            }
        }
        public Boolean IsGameOver { get { return m_motorCycle.m_petrolQuantity <= 0; } }
        public void StartLoadGame()
        {
            m_accelerationTime.Stop();
            m_accelerationTime.Start();

            m_ticker.Stop();
            m_ticker.Start();

            m_timerOfMoving.Stop();
            m_timerOfMoving.Start();

            m_timer.Stop();
            m_timer.Start();
        }
        private void On_SecTicking(object sender, EventArgs eventArgs)
        {
            m_time++;
            Accelaration(); 
            On_RefreshTimeAndPetrol();
            if(IsGameOver)
            {
                OnGameOver(); 
                return; 
            }
        }
        private void OnGameOver()
        {
            if (GameOver != null)
                GameOver(this, new IsGameOver(m_time));
        }
        public void PauseOrResume()
        {
            
            if (!m_isPaused)
            {
                m_ticker.Stop();
                m_accelerationTime.Stop();
                m_timerOfMoving.Stop();
                m_timer.Stop();
                m_isPaused = true;

            }
            else
            {
                m_isPaused = false;
                m_ticker.Start();
                m_accelerationTime.Start();
                m_timerOfMoving.Start();
                m_timer.Start();
            }
        }

        public void On_DecreaseFuel(object sender, EventArgs eventArgs)
        {
            DecreasePetrol();
        }
        private void On_PetrolMoving(object sender, EventArgs eventArgs)
        {
            MovePetrols();
        }
        private void On_NewPetrol(object sender, EventArgs eventArgs)
        {
            NewPetrol(m_petrolDimension, m_panelSize.m_y);
        }
        public void On_RefreshTimeAndPetrol()
        {
            if(RefreshTimeAndPetrol != null)
            {
                RefreshTimeAndPetrol(this, new RefreshTimeOnScreen(m_time, m_motorCycle.m_petrolQuantity));
            }
            if (IsGameOver)
            {
                return;
            }
        }
        public void On_RefreshMotorPosition()
        {
            if(RefreshMotorPosition != null)
            {
                RefreshMotorPosition(this, new AccelerationEventArgs(m_motorCycle.m_coordinates, m_motorCycle.m_dimensions));
            }
        }
        public void On_AppearNewPetrol()
        {
            Int32 size = m_petrolsList.Count - 1;
            if (AppearNewPetrols != null)
            {
                AppearNewPetrols(this, new AppearNewPetrolsArgs(m_petrolsList[size].m_coordinates, m_petrolsList[size].m_dimensions, m_petrolsList[size].m_id));
            }
        }

        public void On_MovingPetrol()
        {
            if (RefreshPetrolsMoving != null)
            {
                RefreshPetrolsMoving(this, new MovingPetrolsArgs(m_petrolsList));
            }
        }
        public void NewPetrol(Pair petrolDimension, int maxValue)
        {
            
            Random rnd = new Random();
            int range = rnd.Next(maxValue);
            Pair coordinates = new Pair(0, range);
            PetrolObject petrolObject = new PetrolObject(coordinates, petrolDimension);
            petrolObject.m_id = m_petrolId;
            m_petrolId++; 
            m_petrolsList.Add(petrolObject);
            On_AppearNewPetrol();
        }
        public void MovePetrols()
        {
            m_counter++;
            if(m_counter < m_speed)
            {
                return;
            }
            m_counter = 0; 
            for(int i = 0; i < m_petrolsList.Count;)
            {
                //Debug.WriteLine(m_motorCycle.m_coordinates.m_x + " " + m_motorCycle.m_coordinates.m_y); 
                if(m_motorCycle.m_coordinates.m_x == m_petrolsList[i].m_coordinates.m_x)
                {
                   // Debug.WriteLine(m_motorCycle.m_coordinates.m_x + " " + m_petrolsList[i].m_coordinates.m_x);
                }
                if (m_petrolsList[i].m_coordinates.m_x + m_petrolsList[i].m_dimensions.m_x == m_panelSize.m_x)
                {
                    if (RemovePetrolObject != null)
                    {
                        RemovePetrolObject(this, new RemovePetrolObject(m_petrolsList[i]));
                    }
                    m_petrolsList.RemoveAt(i);
                }
                else
                {
                    i++; 
                }
            }
            for(int i = 0; i < m_petrolsList.Count; i++)
            {
                m_petrolsList[i].MoveDown(stepQuantity, m_panelSize.m_x);
            }
            On_MovingPetrol();
            CheckStatus();
        }
        public MotorCycleObject GetMotorCycleObject()
        {
            return m_motorCycle; 
        }
        public void CheckStatus()
        {
            for(int i = 0; i < m_petrolsList.Count; i++)
            {
                if(m_motorCycle.isOverlapped(m_petrolsList[i]))
                {
                    IncreasePetrol();
                    if (RemovePetrolObject != null)
                    {
                        RemovePetrolObject(this, new RemovePetrolObject(m_petrolsList[i]));
                    }
                    m_petrolsList.RemoveAt(i);
                    On_RefreshTimeAndPetrol();
                    return;
                }
            }
        }
        public void Move(Direction direction)
        {
            if (IsGameOver)
            {
                //Debug.WriteLine("IN"); 
                return;
            }
            else if (!m_isPaused)
            {
                if (direction == Direction.Right)
                {
                    GetMotorCycleObject().MoveRight(5, m_panelSize.m_y);
                }
                else if (direction == Direction.Left)
                {
                    GetMotorCycleObject().MoveLeft(5, 0);
                }
                On_RefreshMotorPosition();
                CheckStatus();
            }

        }
        public void IncreasePetrol()
        {
            m_motorCycle.m_petrolQuantity += 10; 
        }
        public void DecreasePetrol()
        {
            
            m_motorCycle.m_petrolQuantity -= m_motorCycle.m_consumption;
            if (m_motorCycle.m_petrolQuantity < 0)
            {
                m_motorCycle.m_petrolQuantity = 0; 
            }
        }
        public Pair getMotorCycleCoordinates()
        {
            return m_motorCycle.getCoordinatesOfMotorCycle();
        }
        public async Task SaveGameAsync(String path)
        {
            if (m_dataAccess == null)
                throw new InvalidOperationException("No data access is provided.");

            await m_dataAccess.SaveAsync(path, m_petrolsList, m_motorCycle, m_time, m_panelSize, m_speed, m_counter);
            
        }
        public async Task LoadGameAsync(String path)
        {
            Pair panelSize = new Pair(0,0);
            Pair motorDimension = new Pair(0, 0);
            Pair petrolDimension = new Pair(0, 0);
            AccelerationGameModel model; 
            if (m_dataAccess == null)
                throw new InvalidOperationException("No data access is provided.");

            model = await m_dataAccess.LoadAsync(path);
            m_panelSize = model.m_panelSize;
            m_motorCycle = model.m_motorCycle;
            m_motorCycle.m_coordinates.m_x = model.m_motorCycle.m_coordinates.m_x;
            m_motorCycle.m_coordinates.m_y = model.m_motorCycle.m_coordinates.m_y;
            m_petrolsList = model.m_petrolsList;
            m_time = model.m_time;
            m_speed = model.m_speed;
            m_counter = model.m_counter;
            On_RefreshTimeAndPetrol();
            On_RefreshMotorPosition();

        }
    }
}
