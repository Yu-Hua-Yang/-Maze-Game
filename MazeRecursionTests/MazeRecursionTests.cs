using Microsoft.VisualStudio.TestTools.UnitTesting;
using MazeRecursion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maze;
using System.Runtime.InteropServices;

namespace MazeRecursion.Tests
{
    [TestClass]
    public class MazeRecursionTests
    {
        private static Direction[,] Generate5x5MazeWithSeed()
        {
            return new Direction[2, 2]
            {
                { Direction.E, Direction.S | Direction.W},
                { Direction.E, Direction.N | Direction.W }
            };
        }
        [TestMethod]
        public void MazeRecursionIsNotNull()
        {
            MazeRecursion mazeRecursion = new MazeRecursion();

            Assert.IsNotNull(mazeRecursion);
        }


        [TestMethod]
        public void MazeRecursionSeedIsProvidingCorrectNumber()
        {
            MazeRecursion mazeRecursion = new MazeRecursion(1);
            Assert.AreEqual(534011718, mazeRecursion.CurrentRandom.Next());
        }

        [TestMethod]
        public void MazeRecursionSeedIsNull()
        {
            MazeRecursion mazeRecursion = new MazeRecursion();
            if(534011718 == mazeRecursion.CurrentRandom.Next())
            {
                Assert.Fail();
            }
            else
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void CreateMapTestDefaultSizeReturnsValidMaze()  
        {
            MazeRecursion mazeRecursion = new MazeRecursion(1);
            
            Direction[,] result = mazeRecursion.CreateMap();

            Direction[,] expected = Generate5x5MazeWithSeed();

            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CreateMapTestCustomSizeReturnsValidMaze()
        {
            MazeRecursion mazeRecursion = new MazeRecursion(1);

            Direction[,] result = mazeRecursion.CreateMap(5, 5);

            Direction[,] expected = Generate5x5MazeWithSeed();

            CollectionAssert.AreEqual(expected, result);
        }
    }
}

