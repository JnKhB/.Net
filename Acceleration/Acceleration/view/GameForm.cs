using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Acceleration.model;
using System.Diagnostics;
using Acceleration.Persistence; 

namespace Acceleration
{

    public partial class GameForm : Form
    {
        private AccelerationGameModel m_model;
        private IAccelerationDataAccess m_dataAccess; 
        private List<MyPictureBox> m_petrolsPicture;
        private Pair m_panelSize = new Pair(0, 0);
        private Pair m_motorDimension = new Pair(50, 50);
        private Pair m_petrolDimension = new Pair(50, 50);
        private PictureBox picOfMotor;
        private Pair m_motorCoordinates = new Pair(0, 0);

        public GameForm()
        {
            InitializeComponent();
            m_panelSize.m_x = panel1.Height-statusStrip2.Height;
            m_panelSize.m_y = panel1.Width;
            m_model = new AccelerationGameModel(m_panelSize,  m_motorDimension, m_petrolDimension);
            _newGameStrip.Click += new System.EventHandler(NewGameStrip_Click);
            _pauseOrResumeStrip.Click += new System.EventHandler(PauseOrResumeStrip_Click);
            _saveStrip.Click += new System.EventHandler(SaveStrip_Click);
            _loadStrip.Click += new System.EventHandler(LoadStrip_Click);
            _exit.Click += new System.EventHandler(MenuFileExit_Click);
            m_model.RefreshMotorPosition += Slot_RefreshMotorPosition;
            m_model.AppearNewPetrols += Slot_NewPetrolsAppearOnScreen;
            m_model.RefreshPetrolsMoving += Slot_RefreshMovingPetrols;
            m_model.RefreshTimeAndPetrol += Slot_RefreshTimeAndPetrol;
            m_model.RemovePetrolObject += Slot_RemovePetrolFromScreen;
            m_model.GameOver += Slot_GameOver;
            picOfMotor = new PictureBox();
            m_petrolsPicture = new List<MyPictureBox>();
            _pauseOrResumeStrip.Enabled = false;
            _saveStrip.Enabled = false;
            _loadStrip.Enabled = false;
        }
        int count = 0;
        private void Slot_RefreshTimeAndPetrol(object sender, RefreshTimeOnScreen e)
        {
            toolStripStatusLabel1.Text = TimeSpan.FromSeconds(e.m_time).ToString("g");
            toolStripStatusLabel6.Text = e.m_petrol.ToString(); 
        }
        private void Slot_RefreshMotorPosition(object sender, AccelerationEventArgs e)
        {
            m_motorDimension = e.m_dimensions;
            m_motorCoordinates = e.m_coordinates;
            picOfMotor.Location = new Point(m_motorCoordinates.m_y, m_motorCoordinates.m_x);
            Debug.WriteLine(picOfMotor.Location);
            panel1.Controls.Add(picOfMotor);
        }
        private void Slot_RemovePetrolFromScreen(object sender, RemovePetrolObject e)
        {
            for(int i = 0; i < m_petrolsPicture.Count; i++)
            {
                if(m_petrolsPicture[i].m_id == e.m_petrolObject.m_id)
                {
                    panel1.Controls.Remove(m_petrolsPicture[i]);
                    m_petrolsPicture.RemoveAt(i);
                    return; 
                }
            }
        }
        private void Slot_GameOver(object sender, IsGameOver e)
        {
            m_model.m_timerOfMoving.Stop();
            m_model.m_ticker.Stop();
            m_model.m_timer.Stop();
            m_model.m_accelerationTime.Stop();

            _pauseOrResumeStrip.Enabled = false;
            _saveStrip.Enabled = false;
            MessageBox.Show("Sajnálom, vesztettél, Elfogyott a benzined!",
                                "Motors játék",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Asterisk);
        }
        private void Slot_NewPetrolsAppearOnScreen(object sender, AppearNewPetrolsArgs e)
        {
            MyPictureBox picOfPetrol = new MyPictureBox();
            picOfPetrol.m_id = e.m_id; 
            Size sizeOfPetrol = new Size(e.m_dimensions.m_x, e.m_dimensions.m_y);
            picOfPetrol.Image = Image.FromFile(@"..\..\..\sources\petrol.jpg");
            picOfPetrol.Size = sizeOfPetrol;
            picOfPetrol.SizeMode = PictureBoxSizeMode.StretchImage;
            picOfPetrol.Location = new Point(e.m_coordinates.m_y, e.m_coordinates.m_x);
            panel1.Controls.Add(picOfPetrol);
            m_petrolsPicture.Add(picOfPetrol);
        }
        private void Slot_RefreshMovingPetrols(object sender, MovingPetrolsArgs e)
        {
            if (e.m_petrolObjects.Count > 0)
            {
                for (int i = 0; i < m_petrolsPicture.Count; i++)
                {
                    m_petrolsPicture[i].Location = new Point(e.m_petrolObjects[i].m_coordinates.m_y, e.m_petrolObjects[i].m_coordinates.m_x);
                    panel1.Controls.Add(m_petrolsPicture[i]);
                }
            }
        }
        private void NewGameStrip_Click(Object sender, EventArgs e)
        {
            m_model.NewGame();
            _saveStrip.Enabled = false;
            _loadStrip.Enabled = false;
            _pauseOrResumeStrip.Enabled = true;
            _pauseOrResumeStrip.Text = "Szüneteltetés";
            m_model.m_isPaused = false; 
            NewGame();
        }
        private void LoadGame()
        {
            RemoveScreenObjects();
            Size size = new Size(m_motorDimension.m_x, m_motorDimension.m_y);
            picOfMotor.Image = Image.FromFile(@"..\..\..\sources\motor.jpg");
            picOfMotor.Size = size;
            picOfMotor.SizeMode = PictureBoxSizeMode.StretchImage;
            picOfMotor.Location = new Point(m_motorCoordinates.m_y, m_motorCoordinates.m_x);
            panel1.Controls.Add(picOfMotor);

            for (int i = 0; i < m_model.m_petrolsList.Count; i++)
            {
                MyPictureBox picOfPetrol = new MyPictureBox();
                picOfPetrol.m_id = m_model.m_petrolsList[i].m_id;
                Size sizeOfPetrol = new Size(m_model.m_petrolsList[i].m_dimensions.m_x, m_model.m_petrolsList[i].m_dimensions.m_y);
                picOfPetrol.Image = Image.FromFile(@"..\..\..\sources\petrol.jpg");
                picOfPetrol.Size = sizeOfPetrol;
                picOfPetrol.SizeMode = PictureBoxSizeMode.StretchImage;
                picOfPetrol.Location = new Point(m_model.m_petrolsList[i].m_coordinates.m_y, m_model.m_petrolsList[i].m_coordinates.m_x);
                panel1.Controls.Add(picOfPetrol);
                //m_model.m_petrolsList.Add((PetrolObject)m_model.m_petrolsList[i]);
                m_petrolsPicture.Add(picOfPetrol);
            }
            _saveStrip.Enabled = false;
            _loadStrip.Enabled = false;
        }
        private void RemoveScreenObjects()
        {
            panel1.Controls.Remove(picOfMotor);
            for (int i = 0; i < m_petrolsPicture.Count; i++)
            {
                panel1.Controls.Remove(m_petrolsPicture[i]);
            }
            m_petrolsPicture.Clear();

        }
        private void NewGame()
        {
            RemoveScreenObjects();
                Size size = new Size(m_motorDimension.m_x, m_motorDimension.m_y);
                picOfMotor.Image = Image.FromFile(@"..\..\..\sources\motor.jpg");
                picOfMotor.Size = size;
                picOfMotor.SizeMode = PictureBoxSizeMode.StretchImage;
                picOfMotor.Location = new Point(m_panelSize.m_y / 2 - m_motorDimension.m_y / 2, m_panelSize.m_x - m_motorDimension.m_x);
                panel1.Controls.Add(picOfMotor);
        }

        private void MenuFileExit_Click(Object sender, EventArgs e)
        {
            m_model.m_timerOfMoving.Stop();
            m_model.m_ticker.Stop();
            m_model.m_timer.Stop();
            m_model.m_accelerationTime.Stop();
            Close();
        }


        private void PauseOrResumeStrip_Click(Object sender, EventArgs e)
        {
            if(!m_model.m_isPaused)
            {
                _pauseOrResumeStrip.Text = "Folytatás";
                _saveStrip.Enabled = true;
                _loadStrip.Enabled = true;
            }
            else
            {
                _pauseOrResumeStrip.Text = "Szüneteltetés";
                _saveStrip.Enabled = false;
                _loadStrip.Enabled = false;
            }
                
            m_model.PauseOrResume();
        }
        private async void SaveStrip_Click(Object sender, EventArgs e)
        {
            m_model.m_accelerationTime.Stop();
            m_model.m_ticker.Stop();
            m_model.m_timer.Stop();
            m_model.m_timerOfMoving.Stop();
            _pauseOrResumeStrip.Text = "Folytatás";
            m_model.m_isPaused = true; 
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    await m_model.SaveGameAsync(saveFileDialog1.FileName);
                }
                catch(AccelerationException)
                {
                    MessageBox.Show("Játék mentése sikertelen!" + Environment.NewLine + "Hibás az elérési út, vagy a könyvtár nem írható.", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
        private async void LoadStrip_Click(Object sender, EventArgs e)
        {
            m_model.m_accelerationTime.Stop();
            m_model.m_ticker.Stop();
            m_model.m_timer.Stop();
            m_model.m_timerOfMoving.Stop();
            m_model.m_loading = true; 
            
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    await m_model.LoadGameAsync(openFileDialog1.FileName);
                    _loadStrip.Enabled = true;
                }
                catch (AccelerationException)
                {
                    MessageBox.Show("Játék betöltése sikertelen!" + Environment.NewLine + "Hibás az elérési út, vagy a fájlformátum.", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    m_model.NewGame();
                    _loadStrip.Enabled = true;
                }
                m_model.StartLoadGame();
                LoadGame();
            }
            _pauseOrResumeStrip.Text = "Szüneteltetés";
            m_model.m_isPaused = false;
            _pauseOrResumeStrip.Enabled = true;
            _saveStrip.Enabled = false;
            _loadStrip.Enabled = false;
        }

        private void GameForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                Direction direction = Direction.Right;
                m_model.Move(direction);
            }
            else if (e.KeyCode == Keys.Left)
            {
                Direction direction = Direction.Left;
                m_model.Move(direction);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void statusStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
                
        }

    }
    public class MyPictureBox : PictureBox
    {
        public int m_id;

    }
}
