using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Acceleration_WPF_MVVM.Model;
using Acceleration_WPF_MVVM.Persistance;
using System.Collections.ObjectModel;
using Acceleration_WPF_MVVM.ViewModel;
using Acceleration_WPF_MVVM.View;
using System.ComponentModel;
using System.Diagnostics;
using Microsoft.Win32;

namespace Acceleration_WPF_MVVM
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private AccelerationGameModel m_model;
        private AccelerationViewModel m_viewModel;
        private MainWindow m_view;
        //private Image picOfMotor; 
        //private DispatcherTimer m_timer;
        public App()
        {
            Startup += new StartupEventHandler(App_Startup);
        }
        private void App_Startup(object sender, StartupEventArgs e)
        {
            // modell létrehozása

            Pair panelSize = new Pair(650, 500);
            Pair motorDimension = new Pair(50, 50);
            Pair petrolDimension = new Pair(50, 50); 
            m_model = new AccelerationGameModel(new AccelerationFileDataAccess(), panelSize, motorDimension, petrolDimension);

            m_model.GameOver += On_GameOver;

            // nézemodell létrehozása
            m_viewModel = new AccelerationViewModel(m_model);
            m_viewModel.NewGame += ViewModel_NewGame;
            m_viewModel.ExitGame += ViewModel_ExitGame;
            m_viewModel.LoadGame += ViewModel_LoadGame;
            m_viewModel.SaveGame += new EventHandler(ViewModel_SaveGame);
            m_viewModel.MoveLeft += ViewModel_MoveLeft;
            m_viewModel.MoveRight += ViewModel_MoveRight;
           
            // nézet létrehozása
            m_view = new MainWindow();
            m_view.DataContext = m_viewModel;
            m_view.Closing += new System.ComponentModel.CancelEventHandler(View_Closing);
            m_view.Show();
        }
        private void On_GameOver(object sender, IsGameOver e)
        {
            m_model.PauseOrResume();
            MessageBox.Show("Elfogyott a benzined! :(" + Environment.NewLine +
                "Az időd: " + m_model.m_time + "másodperc");
        }
        private void ViewModel_NewGame(object sender, EventArgs e)
        {
            m_model.NewGame();
        }
        private void ViewModel_MoveLeft(object sender, EventArgs e)
        {
            m_model.Move(Direction.Left); 
        }
        private void ViewModel_MoveRight(object sender, EventArgs e)
        {
            m_model.Move(Direction.Right);
        }
        private async void ViewModel_LoadGame(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Title = "Motors játék betöltése";
                openFileDialog.Filter = "Motors játék|*.stl";
                if (openFileDialog.ShowDialog() == true)
                {
                    // játék betöltése
                    await m_model.LoadGameAsync(openFileDialog.FileName);
                    m_model.m_accelerationTime.Stop();
                    m_model.m_ticker.Stop();
                    m_model.m_timer.Stop();
                    m_model.m_timerOfMoving.Stop();
                    m_model.m_loading = true;

                    MessageBox.Show("A betöltés sikerült!", "SUCCESS", MessageBoxButton.OK, MessageBoxImage.Information);
                    m_viewModel.OnPauseOrResume();
                }
            }
            catch
            {
                MessageBox.Show("A fájl betöltése sikertelen!", "Motor játék", MessageBoxButton.OK, MessageBoxImage.Error);
            }   
        }
        private async void ViewModel_SaveGame(object sender, EventArgs e)
        {
            if (m_model.m_isPaused)
            {
                try
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog(); // dialógablak
                    saveFileDialog.Title = "Motors játék betöltése";
                    saveFileDialog.Filter = "Motor játék|*.stl";
                    if (saveFileDialog.ShowDialog() == true)
                    {
                        try
                        {
                            // játéktábla mentése
                            await m_model.SaveGameAsync(saveFileDialog.FileName);
                        }
                        catch (AccelerationException)
                        {
                            MessageBox.Show("Játék mentése sikertelen!" + Environment.NewLine + "Hibás az elérési út, vagy a könyvtár nem írható.", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("A fájl mentése sikertelen!", "Motoros játék", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void View_Closing(object sender, CancelEventArgs e)
        {
            Boolean restartTimer = m_model.m_ticker.IsEnabled;

            m_model.m_ticker.Stop();

            if (MessageBox.Show("Biztos, hogy ki akar lépni?", "Motoros játék", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
            {
                e.Cancel = true;

                if (restartTimer)
                    m_model.m_ticker.Start();
            }
        }
        private void ViewModel_ExitGame(object sender, System.EventArgs e)
        {
            m_view.Close();
        }
    }
}
