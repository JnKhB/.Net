using Microsoft.VisualStudio.TestTools.UnitTesting;
using Acceleration_WPF_MVVM.Model;
using Acceleration_WPF_MVVM.Persistance;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Moq;
using System;

namespace AccelerationTest
{
    [TestClass]
    public class AccelerationTest
    {
        private AccelerationGameModel m_model;
        private Mock<IAccelerationDataAccess> m_mock;
        private AccelerationGameModel m_mockedModel;
        Pair coordinates = new Pair(749, 241);
        Pair sizes = new Pair(50, 50);
        Pair panelSize = new Pair(799, 582);
        [TestInitialize]
        public void Initialize()
        {

            m_mockedModel = new AccelerationGameModel(new AccelerationFileDataAccess(), panelSize, sizes, sizes);
            m_mockedModel.NewPetrol(sizes, 532);
            m_mockedModel.m_petrolsList[0].m_coordinates.m_x = 50;
            m_mockedModel.m_petrolsList[0].m_coordinates.m_y = 50;
            m_mockedModel.NewPetrol(sizes, 532);
            m_mockedModel.m_petrolsList[0].m_coordinates.m_x = 150;
            m_mockedModel.m_petrolsList[0].m_coordinates.m_y = 150;
            m_mockedModel.NewPetrol(sizes, 532);
            m_mockedModel.m_petrolsList[0].m_coordinates.m_x = 400;
            m_mockedModel.m_petrolsList[0].m_coordinates.m_y = 350;
            m_mock = new Mock<IAccelerationDataAccess>();
            m_mock.Setup(mock => mock.LoadAsync(It.IsAny<String>()))
                .Returns(() => Task.FromResult(m_mockedModel));


            m_model = new AccelerationGameModel(m_mock.Object, panelSize, sizes, sizes);
        }
        [TestMethod]
        public void AccelerationModelNewGame()
        {
            m_model.NewGame();
            Assert.AreEqual(m_model.m_motorCycle.m_consumption, 1);
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
        public void CheckBorders()
        {
            m_model.NewGame();
            Assert.AreEqual(m_model.m_motorCycle.m_consumption, 1);
            Assert.AreEqual(m_model.m_motorCycle.m_petrolQuantity, 1000);
            Assert.AreEqual(m_model.m_petrolsList.Count, 0);
            m_model.m_motorCycle.MoveLeft(100, 0);
            Assert.AreEqual(m_model.m_motorCycle.m_coordinates.m_y, 166);
            m_model.m_motorCycle.MoveLeft(200, 0);
            Assert.AreEqual(m_model.m_motorCycle.m_coordinates.m_y, 0);
            m_model.m_motorCycle.MoveRight(200, 582);
            Assert.AreEqual(m_model.m_motorCycle.m_coordinates.m_y, 200);
            m_model.m_motorCycle.MoveRight(300, 582);
            Assert.AreEqual(m_model.m_motorCycle.m_coordinates.m_y, 500);
            m_model.m_motorCycle.MoveRight(200, 582);
            Assert.AreEqual(m_model.m_motorCycle.m_coordinates.m_y, 532);
        }
        [TestMethod]
        public void CheckPetrolsMove()
        {
            m_model.NewGame();
            Assert.AreEqual(m_model.m_motorCycle.m_consumption, 1);
            Assert.AreEqual(m_model.m_motorCycle.m_petrolQuantity, 1000);
            Assert.AreEqual(m_model.m_petrolsList.Count, 0);
            m_model.NewPetrol(sizes, panelSize.m_y);
            Assert.AreEqual(m_model.m_petrolsList.Count, 1);
            m_model.m_petrolsList[0].MoveDown(100, 799);
            Assert.AreEqual(m_model.m_petrolsList[0].m_coordinates.m_x, 100);
        }
        [TestMethod]
        public void IsGameOver()
        {
            m_model.NewGame();
            m_model.NewPetrol(sizes, panelSize.m_y);
            m_model.m_petrolsList[0].m_coordinates.m_x = m_model.m_motorCycle.m_coordinates.m_x;
            m_model.m_petrolsList[0].m_coordinates.m_y = m_model.m_motorCycle.m_coordinates.m_y;
            Assert.AreEqual(m_model.m_motorCycle.isOverlapped(m_model.m_petrolsList[0]), true);
            m_model.m_petrolsList[0].m_coordinates.m_x = m_model.m_motorCycle.m_coordinates.m_x - 30;
            m_model.m_petrolsList[0].m_coordinates.m_y = m_model.m_motorCycle.m_coordinates.m_y - 30;
            Assert.AreEqual(m_model.m_motorCycle.isOverlapped(m_model.m_petrolsList[0]), true);
            m_model.m_petrolsList[0].m_coordinates.m_x = m_model.m_motorCycle.m_coordinates.m_x - (m_model.m_motorCycle.m_dimensions.m_x + 1);
            m_model.m_petrolsList[0].m_coordinates.m_y = m_model.m_motorCycle.m_coordinates.m_y - (m_model.m_motorCycle.m_dimensions.m_y + 1);
            Assert.AreEqual(m_model.m_motorCycle.isOverlapped(m_model.m_petrolsList[0]), false);
            m_model.m_petrolsList[0].m_coordinates.m_x = m_model.m_motorCycle.m_coordinates.m_x - 49;
            m_model.m_petrolsList[0].m_coordinates.m_y = m_model.m_motorCycle.m_coordinates.m_y - 49;
            Assert.AreEqual(m_model.m_motorCycle.isOverlapped(m_model.m_petrolsList[0]), true);
        }
        [TestMethod]
        public async Task LoadTest()
        {
            m_model.NewGame();
            await m_model.LoadGameAsync(String.Empty);
            Assert.AreEqual(m_mockedModel.m_petrolsList.Count, m_model.m_petrolsList.Count);
            for(int i = 0; i < m_mockedModel.m_petrolsList.Count; i++)
            {
                Assert.AreEqual(m_mockedModel.m_petrolsList[i].m_coordinates.m_x, m_model.m_petrolsList[i].m_coordinates.m_x);
                Assert.AreEqual(m_mockedModel.m_petrolsList[i].m_coordinates.m_y, m_model.m_petrolsList[i].m_coordinates.m_y);
            }
            m_mock.Verify(dataAccess => dataAccess.LoadAsync(String.Empty), Times.Once());
        }
    }
}


