using Microsoft.VisualStudio.TestTools.UnitTesting;
using MazeHuntKill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze;
using System.Runtime.InteropServices;

namespace MazeHuntKillTests
{
    [TestClass]
    public class MazeHuntKillTests
    {
        private static Direction[,] Generate5x5MazeWithSeed()
        {
            return new Direction[2, 2]
            {
                { Direction.E, Direction.W | Direction.S },
                { Direction.E, Direction.N | Direction.W }
            };
        }

        [TestMethod]
        public void MazeHuntKillIsNotNull()
        {
            MazeHuntKill.MazeHuntKill mazeHuntKill = new MazeHuntKill.MazeHuntKill();

            Assert.IsNotNull(mazeHuntKill);
        }


        [TestMethod]
        public void MazeHuntKillSeedIsProvidingCorrectNumber()
        {
            MazeHuntKill.MazeHuntKill mazeHuntKill = new MazeHuntKill.MazeHuntKill(1);
            Assert.AreEqual(534011718, mazeHuntKill.CurrentRandom.Next());
        }

        [TestMethod]
        public void MazeHuntKillSeedIsNull()
        {
            MazeHuntKill.MazeHuntKill mazeHuntKill = new MazeHuntKill.MazeHuntKill(); ;
            if (534011718 == mazeHuntKill.CurrentRandom.Next())
            {
                Assert.Fail();
            }
            else
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void CreateMapTestCustomSizeReturnsValidMaze()
        {
            MazeHuntKill.MazeHuntKill mazeHuntKill = new MazeHuntKill.MazeHuntKill(1);

            Direction[,] resultDirectionMap = mazeHuntKill.CreateMap(5, 5);

            Direction[,] expected = Generate5x5MazeWithSeed();

            CollectionAssert.AreEqual(expected, resultDirectionMap);
        }

        [TestMethod]
        public void CreateMapTestDefaultSizeReturnsValidMaze()
        {
            MazeHuntKill.MazeHuntKill mazeHuntKill = new MazeHuntKill.MazeHuntKill(1);

            Direction[,] resultDirectionMap = mazeHuntKill.CreateMap();

            Direction[,] expected = Generate5x5MazeWithSeed();

            CollectionAssert.AreEqual(expected, resultDirectionMap);
        }
    }
}