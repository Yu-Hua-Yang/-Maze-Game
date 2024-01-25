using Microsoft.VisualStudio.TestTools.UnitTesting;
using Maze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Tests
{
    [TestClass]
    public class MapVectorTests
    {
        [TestMethod]
        public void InsideBoundaryTestReturnsTrue()
        {
            MapVector testMapVector = new MapVector(3, 3);
            bool isInside = testMapVector.InsideBoundary(5, 5);
            Assert.IsTrue(isInside);
        }

        [TestMethod]
        public void InsideBoundaryTestReturnsFalse()
        {
            MapVector testMapVector = new MapVector(3, 3);
            bool isInside = testMapVector.InsideBoundary(2, 2);
            Assert.IsFalse(isInside);
        }

        [TestMethod]
        public void MagnitudeTestReturnsCorrectValue()
        {
            MapVector testMapVector = new MapVector(3, 3);
            Assert.AreEqual(testMapVector.Magnitude(), Math.Sqrt(3 * 3 + 3 * 3));
        }

        [TestMethod]
        public void GoalToPlayerMagnitudeTestReturnsCorrectValue()
        {
            MapVector testMapVectorOne = new MapVector(3, 3);
            MapVector testMapVectorTwo = new MapVector(6, 6);
            Assert.AreEqual(testMapVectorOne.GoalToPlayerMagnitude(testMapVectorTwo), Math.Sqrt(3 * 3 + 3 * 3));
        }

        [TestMethod]
        public void IsDeadEndTestReturnsTrue()
        {
            MapVector testMapVector = new MapVector(3, 3);
            Block[,] testGrid = new Block[6, 6];
            testGrid[3, 3] = Block.Empty;
            testGrid[3, 4] = Block.Empty;
            Assert.IsTrue(testMapVector.IsDeadEnd(testGrid));
        }

        [TestMethod]
        public void IsDeadEndTestReturnsFalse()
        {
            MapVector testMapVector = new MapVector(3, 3);
            Block[,] testGrid = new Block[6, 6];
            testGrid[3, 3] = Block.Empty;
            testGrid[3, 4] = Block.Empty;
            testGrid[4, 3] = Block.Empty;
            Assert.IsFalse(testMapVector.IsDeadEnd(testGrid));
        }

        [TestMethod]
        public void EqualsTestReturnTrue()
        {
            MapVector testMapVectorOne = new MapVector(3, 3);
            MapVector testMapVectorTwo = new MapVector(3, 3);
            Assert.IsTrue(testMapVectorOne.Equals(testMapVectorTwo));
        }

        [TestMethod]
        public void EqualsTestReturnFalse()
        {
            MapVector testMapVectorOne = new MapVector(3, 3);
            MapVector testMapVectorTwo = new MapVector(4, 3);
            Assert.IsFalse(testMapVectorOne.Equals(testMapVectorTwo));
        }

        [TestMethod]
        public void PlusOperatorTestReturnsCorrectValue()
        {
            MapVector testMapVectorOne = new MapVector(3, 3);
            MapVector testMapVectorTwo = new MapVector(1, 1);
            MapVector resultMapVector = new MapVector(4, 4);
            Assert.AreEqual(testMapVectorOne + testMapVectorTwo, resultMapVector);
        }

        [TestMethod]
        public void MinusOperatorTestReturnsCorrectValue()
        {
            MapVector testMapVectorOne = new MapVector(3, 3);
            MapVector testMapVectorTwo = new MapVector(1, 1);
            MapVector resultMapVector = new MapVector(2, 2);
            Assert.AreEqual(testMapVectorOne - testMapVectorTwo, resultMapVector);
        }

        [TestMethod]
        public void MultiplyOperatorTestReturnsCorrectValue()
        {
            MapVector testMapVectorOne = new MapVector(3, 3);
            MapVector resultMapVector = new MapVector(6, 6);
            Assert.AreEqual(testMapVectorOne * 2, resultMapVector);
        }

        [TestMethod]
        public void ImplicitCastingOperatorTestDirectionNorth()
        {
            MapVector resultMapVector = new MapVector(0, -1);
            MapVector testNorthMapVector = Direction.N;
            Assert.AreEqual(testNorthMapVector, resultMapVector);
        }

        [TestMethod]
        public void ImplicitCastingOperatorTestDirectionEast()
        {
            MapVector resultMapVector = new MapVector(1, 0);
            MapVector testEastMapVector = Direction.E;
            Assert.AreEqual(testEastMapVector, resultMapVector);
        }

        [TestMethod]
        public void ImplicitCastingOperatorTestDirectionSouth()
        {
            MapVector resultMapVector = new MapVector(0, 1);
            MapVector testSouthMapVector = Direction.S;
            Assert.AreEqual(testSouthMapVector, resultMapVector);
        }

        [TestMethod]
        public void ImplicitCastingOperatorTestDirectionWest()
        {
            MapVector resultMapVector = new MapVector(-1, 0);
            MapVector testWestMapVector = Direction.W;
            Assert.AreEqual(testWestMapVector, resultMapVector);
        }
    }
}