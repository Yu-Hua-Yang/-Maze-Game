using Microsoft.VisualStudio.TestTools.UnitTesting;
using Maze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace Maze.Tests
{
    [TestClass]
    public class MapTests
    {
        public IMapProvider CreateMapProvider()
        {
            Mock<IMapProvider> provider = new Mock<IMapProvider>();
            provider.Setup(m => m.CreateMap()).Returns(
                new Direction[,]
                {
                    { Direction.E | Direction.S, Direction.W | Direction.S },
                    { Direction.N, Direction.N }
                }
            );
            return provider.Object;
        }

        [TestMethod]
        public void CreateMapTestIsNotNull()
        {
            Map testMap = new Map(CreateMapProvider());
            testMap.CreateMap();
            Assert.IsNotNull(testMap);
        }

        [TestMethod]
        public void CreateMapTestYIsCorrect()
        {
            Map testMap = new Map(CreateMapProvider());
            testMap.CreateMap();
            Assert.AreEqual(testMap.Height, 5);
        }

        [TestMethod]
        public void CreateMapTestXIsCorrect()
        {
            Map testMap = new Map(CreateMapProvider());
            testMap.CreateMap();
            Assert.AreEqual(testMap.Width, 5);
        }

        [TestMethod]
        public void GeneratePlayerSpawnTestIsNotNull()
        {
            Map testMap = new Map(CreateMapProvider());
            testMap.CreateMap();
            Assert.IsNotNull(testMap.Player);
        }

        [TestMethod]
        public void GeneratePlayerSpawnTestSpawnIsEmpty()
        {
            Map testMap = new Map(CreateMapProvider());
            testMap.CreateMap();
            if (testMap.MapGrid[testMap.Player.StartY, testMap.Player.StartX] == Block.Empty)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void GenerateGoalSpawnTestIsNotNull()
        {
            Map testMap = new Map(CreateMapProvider());
            testMap.CreateMap();
            Assert.IsNotNull(testMap.Goal);
        }

        [TestMethod]
        public void GenerateGoalSpawnTestSpawnIsEmpty()
        {
            Map testMap = new Map(CreateMapProvider());
            testMap.CreateMap();
            if (testMap.MapGrid[testMap.Goal.X, testMap.Goal.Y] == Block.Empty)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void MapConstructorTestIsNotNull()
        {
            Map testMap = new Map(CreateMapProvider());
            Assert.IsNotNull(testMap.Provider);
        }

        [TestMethod]
        public void MapToStringTestIsNotNull()
        {
            Map testMap = new Map(CreateMapProvider());
            StringBuilder testStringBuilder = testMap.MapToString();
            Assert.IsNotNull(testStringBuilder);
        }
    }
}