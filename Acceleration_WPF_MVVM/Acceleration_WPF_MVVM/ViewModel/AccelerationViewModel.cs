using System;
using System.Collections.Generic;
using System.Text;
using Acceleration_WPF_MVVM.Model;
using Acceleration_WPF_MVVM.Persistance;
using System.Collections.ObjectModel;


namespace Acceleration_WPF_MVVM.ViewModel
{
    public class AccelerationViewModel : ViewModelBase
    {
        AccelerationGameModel m_model;
        MotorCycleObject m_motor;
        private String m_isLoadEnable;
        private String m_isSaveEnable;
        private String m_textOfPauseOrRestartButton;
        private String m_IsPauseOrRestartButtonEnable;
        private bool m_isPaused;
        public String SaveIsEnable
        {
            get { return m_isSaveEnable; }
            set
            {
                if (m_isSaveEnable != value)
                {
                    m_isSaveEnable = value;
                    OnPropertyChanged();
                }
            }
        }
        public String LoadIsEnable
        {
            get { return m_isLoadEnable; }
            set
            {
                if (m_isLoadEnable != value)
                {
                    m_isLoadEnable = value;
                    OnPropertyChanged();
                }
            }
        }
        public String PauseOrResumeButtonText
        {
            get { return m_textOfPauseOrRestartButton; }
            set
            {
                if (m_textOfPauseOrRestartButton != value)
                {
                    m_textOfPauseOrRestartButton = value;
                    OnPropertyChanged();
                }
            }
        }
        public String PauseOrResumeEnable
        {
            get { return m_IsPauseOrRestartButtonEnable; }
            set
            {
                if (m_IsPauseOrRestartButtonEnable != value)
                {
                    m_IsPauseOrRestartButtonEnable = value;
                    OnPropertyChanged();
                }
            }
        }
        public DelegateCommand NewGameCommand { get; private set; }
        public DelegateCommand PauseOrResumeCommand { get; private set; }
        public DelegateCommand SaveGameCommand { get; private set; }
        public DelegateCommand LoadGameCommand { get; private set; }
        public DelegateCommand ExitCommand { get; private set; }
        public DelegateCommand MoveLeftCommand { get; private set; }
        public DelegateCommand MoveRightCommand { get; private set; }
        public String GameTime { get; private set; }
        public ObservableCollection<ViewScreenObject> ViewScreenObjects { get; set; }
        public Int32 PetrolQuantity { get { return m_model.GetMotorCycleObject().m_petrolQuantity; } }
        public String ElapsedTime { get; private set; }

        public event EventHandler NewGame;

        public event EventHandler LoadGame;

        public event EventHandler SaveGame;

        public event EventHandler ExitGame;

        public event EventHandler MoveRight;

        public event EventHandler MoveLeft;


        public AccelerationViewModel(AccelerationGameModel model)
        {
            m_model = model;

            m_model.RefreshMotorPosition += On_RefreshMotorPosition;
            m_model.AppearNewPetrols += On_AppearNewPetrols;
            m_model.RefreshTimeAndPetrol += On_RefreshTimeAndPetrol;
            m_model.RefreshPetrolsMoving += On_RefreshPetrolsMoving;
            m_model.RemovePetrolObject += On_RemovePetrolObject;

            NewGameCommand = new DelegateCommand(param => OnNewGame());
            LoadGameCommand = new DelegateCommand(param => OnLoadGame());
            SaveGameCommand = new DelegateCommand(param => OnSaveGame());
            PauseOrResumeCommand = new DelegateCommand(param => OnPauseOrResume());
            ExitCommand = new DelegateCommand(param => OnExitGame());
            MoveLeftCommand = new DelegateCommand(param => OnMoveLeft());
            MoveRightCommand = new DelegateCommand(param => OnMoveRight());

            SaveIsEnable = "False";
            LoadIsEnable = "False";
            PauseOrResumeButtonText = "Szüneteltetés";
            PauseOrResumeEnable = "False";
            ViewScreenObjects = new ObservableCollection<ViewScreenObject>();

        }

        private void OnNewGame()
        {
            ViewScreenObjects.Clear(); 

            if (NewGame != null)
                NewGame(this, EventArgs.Empty);

            OnPropertyChanged("petrolQuantity");
            PauseOrResumeEnable = "True";
            PauseOrResumeButtonText = "Szüneteltetés";
            m_model.m_isPaused = false;
            m_isPaused = false; 
            SaveIsEnable = "False";
            LoadIsEnable = "False";
        }
        public void OnPauseOrResume()
        {
            
            if (PauseOrResumeButtonText == "Szüneteltetés")
            {
                m_model.PauseOrResume(); 
                m_model.m_isPaused = true;
                PauseOrResumeButtonText = "Folytatás";
                OnPropertyChanged("PauseOrResumeButtonText");
                SaveIsEnable = "True";
                LoadIsEnable = "True";
                OnPropertyChanged("SaveEnable");
                OnPropertyChanged("LoadEnable");
                m_isPaused = true;
            }
            else
            {
                PauseOrResumeButtonText = "Szüneteltetés";
                SaveIsEnable = "False";
                LoadIsEnable = "False";
                OnPropertyChanged("SaveEnable");
                OnPropertyChanged("LoadEnable");
                m_model.PauseOrResume();
                m_isPaused = false;
            }   
        }
        private void OnMoveLeft()
        {
            if (MoveLeft != null)
                MoveLeft(this, EventArgs.Empty);
        }
        private void OnMoveRight()
        {
            if (MoveRight != null)
                MoveRight(this, EventArgs.Empty);
        }
        private void OnLoadGame()
        {
            if (LoadGame != null)
                LoadGame(this, EventArgs.Empty);
        }
        private void OnSaveGame()
        {
            if (SaveGame != null)
                SaveGame(this, EventArgs.Empty);
        }
        private void OnExitGame()
        {
            if (ExitGame != null)
                ExitGame(this, EventArgs.Empty);
        }
        int idNumber = 1;
        private void On_RefreshMotorPosition(object sender, AccelerationEventArgs e)
        {
            if (ViewScreenObjects.Count > 0)
            {
                ViewScreenObjects[0] = new ViewScreenObject(e.m_coordinates.m_y, e.m_coordinates.m_x, 50, false, idNumber);
            }
            else
            {
                ViewScreenObjects.Add(new ViewScreenObject(e.m_coordinates.m_y, e.m_coordinates.m_x, 50, false, idNumber)); 
            }
            OnPropertyChanged(nameof(ViewScreenObject));
        }

        private void On_AppearNewPetrols(object sender, AppearNewPetrolsArgs e)
        {
            
            ViewScreenObjects.Add(new ViewScreenObject(e.m_coordinates.m_y, e.m_coordinates.m_x, e.m_dimensions.m_x, true, idNumber));
            idNumber++;
            OnPropertyChanged(nameof(ViewScreenObject));
        }
        private void On_RefreshPetrolsMoving(object sender, MovingPetrolsArgs e)
        {
            ViewScreenObject motor = ViewScreenObjects[0];
            ViewScreenObjects.Clear();
            ViewScreenObjects.Add(motor);
            for (int i = 0; i < m_model.m_petrolsList.Count ;i++)
            {
                ViewScreenObjects.Add(new ViewScreenObject(e.m_petrolObjects[i].m_coordinates.m_y, e.m_petrolObjects[i].m_coordinates.m_x, e.m_petrolObjects[i].m_dimensions.m_x, true, i+1));
            }
            OnPropertyChanged(nameof(ViewScreenObject));
            
        }
        private void On_RemovePetrolObject(object sender, RemovePetrolObject e)
        {
            /*
            for (int i = 0; i < ViewScreenObjects.Count; i++)
            {
                if(ViewScreenObjects[i].ID == e.m_petrolObject.m_id && ViewScreenObjects[i].IsPetrol)
                {
                    ViewScreenObjects.RemoveAt(i); 
                }
            }*/

            OnPropertyChanged(nameof(ViewScreenObject));
        }
        private void On_RefreshTimeAndPetrol(object sender, RefreshTimeOnScreen e)
        {
            GameTime = TimeSpan.FromSeconds(e.m_time).ToString("mm':'ss");
            //PetrolQuantity = e.m_petrol; 
            OnPropertyChanged("GameTime");
            OnPropertyChanged("PetrolQuantity");
        }
    }
}
