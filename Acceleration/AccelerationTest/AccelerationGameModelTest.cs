using Microsoft.VisualStudio.TestTools.UnitTesting;
using Acceleration.Persistence;
using Acceleration.model;
using System;
using System.Threading.Tasks;
using Moq;
using System.Diagnostics;

namespace AccelerationTest
{
    [TestClass]
    public class UnitTest1
    {
        
        private AccelerationGameModel m_model;
        private Mock<IAccelerationDataAccess> m_mock;
        Pair coordinates = new Pair(749, 241);
        Pair sizes = new Pair(50, 50);
        Pair panelSize = new Pair(799, 582);
        [TestInitialize]
        public void Initialize()
        {
            m_mock = new Mock<IAccelerationDataAccess>();
            m_mock.Setup(mock => mock.LoadAsync(It.IsAny<String>()))
                .Returns(() => Task.FromResult(m_model));

            m_model = new AccelerationGameModel(coordinates, panelSize, sizes);
            m_model.RefreshMotorPosition += new EventHandler<AccelerationEventArgs>(Model_Slot_RefreshMotorPosition);
            m_model.AppearNewPetrols += new EventHandler<AppearNewPetrolsArgs>(Model_Slot_NewPetrolsAppearOnScreen);
            /*
            m_model.RefreshPetrolsMoving += Slot_RefreshMovingPetrols;
            m_model.RefreshTimeAndPetrol += Slot_RefreshTimeAndPetrol;
            m_model.RemovePetrolObject += Slot_RemovePetrolFromScreen;
            m_model.GameOver += Slot_GameOver;
            */
        }
        [TestMethod]
        public void AccelerationModelNewGame()
        {
            m_model.NewGame();
            Assert.AreEqual(m_model.m_motorCycle.m_consumption,1);
            Assert.AreEqual(m_model.m_motorCycle.m_petrolQuantity, 1000);
            Assert.AreEqual(m_model.m_petrolsList.Count, 0);
        }
        [TestMethod]
        public void AccelerationModelAppearsPetrol()
        {
            m_model.NewGame();
            Assert.AreEqual(m_model.m_motorCycle.m_consumption, 1);
            Assert.AreEqual(m_model.m_motorCycle.m_petrolQuantity, 1000);
            Assert.AreEqual(m_model.m_petrolsList.Count, 0);
            m_model.NewPetrol(sizes, panelSize.m_y);
            Assert.AreEqual(m_model.m_petrolsList.Count, 1);
            m_model.NewPetrol(sizes, panelSize.m_y);
            Assert.AreEqual(m_model.m_petrolsList.Count, 2);
            m_model.NewPetrol(sizes, panelSize.m_y);
            Assert.AreEqual(m_model.m_petrolsList.Count, 3);
        }
        [TestMethod]
        public void IsGameOver()
        {
            m_model.NewGame();
            m_model.NewPetrol(sizes, panelSize.m_y);
            m_model.m_petrolsList[0].m_coordinates.m_x = m_model.m_motorCycle.m_coordinates.m_x;
            m_model.m_petrolsList[0].m_coordinates.m_y = m_model.m_motorCycle.m_coordinates.m_y;
            Assert.AreEqual(m_model.m_motorCycle.isOverlapped(m_model.m_petrolsList[0]), true);
            m_model.m_petrolsList[0].m_coordinates.m_x = m_model.m_motorCycle.m_coordinates.m_x-30;
            m_model.m_petrolsList[0].m_coordinates.m_y = m_model.m_motorCycle.m_coordinates.m_y-30;
            Assert.AreEqual(m_model.m_motorCycle.isOverlapped(m_model.m_petrolsList[0]), true);
            m_model.m_petrolsList[0].m_coordinates.m_x = m_model.m_motorCycle.m_coordinates.m_x - (m_model.m_motorCycle.m_dimensions.m_x+1);
            m_model.m_petrolsList[0].m_coordinates.m_y = m_model.m_motorCycle.m_coordinates.m_y - (m_model.m_motorCycle.m_dimensions.m_y+1);
            Assert.AreEqual(m_model.m_motorCycle.isOverlapped(m_model.m_petrolsList[0]), false);
            m_model.m_petrolsList[0].m_coordinates.m_x = m_model.m_motorCycle.m_coordinates.m_x - 49;
            m_model.m_petrolsList[0].m_coordinates.m_y = m_model.m_motorCycle.m_coordinates.m_y - 49;
            Assert.AreEqual(m_model.m_motorCycle.isOverlapped(m_model.m_petrolsList[0]), true);
        }
        private void Model_Slot_RefreshMotorPosition(Object sender, AccelerationEventArgs e)
        {

        }
        private void Model_Slot_NewPetrolsAppearOnScreen(Object sender, AppearNewPetrolsArgs e)
        {

        }
    }
}
