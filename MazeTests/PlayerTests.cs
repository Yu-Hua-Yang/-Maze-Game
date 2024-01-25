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
    public class PlayerTests
    {
        private static Block[,] CreateMapGrid()
        {
            Block[,] mapGrid = new Block[,]
            {
        { Block.Solid, Block.Solid, Block.Solid, Block.Solid, Block.Solid },
        { Block.Solid, Block.Empty, Block.Empty, Block.Empty, Block.Solid },
        { Block.Solid, Block.Empty, Block.Solid, Block.Empty, Block.Solid },
        { Block.Solid, Block.Empty, Block.Solid, Block.Empty, Block.Solid },
        { Block.Solid, Block.Solid, Block.Solid, Block.Solid, Block.Solid }
            };
            return mapGrid;
        }

        [TestMethod]
        public void GetRotationTestNorth()
        {
            MapVector playerPostion = new MapVector(1,1);
            Block[,] testGrid = CreateMapGrid();
            Player testPlayer = new Player(playerPostion, 1, 1, testGrid);
            float resultRadians = (float)((Math.PI / 180) * 0);
            Assert.AreEqual(testPlayer.GetRotation(), resultRadians);   
        }

        [TestMethod]
        public void GetRotationTestEast()
        {
            MapVector playerPostion = new MapVector(1, 1);
            Block[,] testGrid = CreateMapGrid();
            Player testPlayer = new Player(playerPostion, 1, 1, testGrid);
            testPlayer.Facing = Direction.E;
            float resultRadians = (float)((Math.PI / 180) * 90);
            Assert.AreEqual(testPlayer.GetRotation(), resultRadians);
        }

        [TestMethod]
        public void GetRotationTestSouth()
        {
            MapVector playerPostion = new MapVector(1, 1);
            Block[,] testGrid = CreateMapGrid();
            Player testPlayer = new Player(playerPostion, 1, 1, testGrid);
            testPlayer.Facing = Direction.S;
            float resultRadians = (float)((Math.PI / 180) * 180);
            Assert.AreEqual(testPlayer.GetRotation(), resultRadians);
        }

        [TestMethod]
        public void GetRotationTestWest()
        {
            MapVector playerPostion = new MapVector(1, 1);
            Block[,] testGrid = CreateMapGrid();
            Player testPlayer = new Player(playerPostion, 1, 1, testGrid);
            testPlayer.Facing = Direction.W;
            float resultRadians = (float)((Math.PI / 180) * 270);
            Assert.AreEqual(testPlayer.GetRotation(), resultRadians);
        }

        [TestMethod]
        public void MoveBackwardTestIfMoveIsValid()
        {
            MapVector playerPostion = new MapVector(2, 2);
            Block[,] testGrid = CreateMapGrid();
            Player testPlayer = new Player(playerPostion, 2, 2, testGrid);
            testPlayer.MoveBackward();
            MapVector resultVector = new MapVector(2, 3);
            Assert.AreEqual(testPlayer.Position, resultVector);
        }

        [TestMethod]
        public void MoveBackwardTestIfMoveIsInvalid()
        {
            MapVector playerPostion = new MapVector(2, 2);
            Block[,] testGrid = new Block[5,5];
            testGrid[2, 2] = Block.Empty;
            Player testPlayer = new Player(playerPostion, 2, 2, testGrid);
            testPlayer.MoveBackward();
            //Vector stays the same if there is no valid move
            MapVector resultVector = new MapVector(2, 2);
            Assert.AreEqual(testPlayer.Position, resultVector);
        }

        [TestMethod]
        public void MoveForwardTestMoveIsValid()
        {
            MapVector playerPostion = new MapVector(2, 2);
            Block[,] testGrid = CreateMapGrid();
            Player testPlayer = new Player(playerPostion, 2, 2, testGrid);
            testPlayer.MoveForward();
            MapVector resultVector = new MapVector(2, 1);
            Assert.AreEqual(testPlayer.Position, resultVector);
        }

        [TestMethod]
        public void MoveForwardTestMoveIsInvalid()
        {
            MapVector playerPostion = new MapVector(2, 2);
            Block[,] testGrid = new Block[5, 5];
            testGrid[2, 2] = Block.Empty;
            Player testPlayer = new Player(playerPostion, 2, 2, testGrid);
            testPlayer.MoveForward();
            //Vector stays the same if there is no valid move
            MapVector resultVector = new MapVector(2, 2);
            Assert.AreEqual(testPlayer.Position, resultVector);
        }

        [TestMethod]
        public void TurnLeftTestWhileNorth()
        {
            MapVector playerPostion = new MapVector(1, 1);
            Block[,] testGrid = CreateMapGrid();
            Player testPlayer = new Player(playerPostion, 1, 1, testGrid);
            testPlayer.TurnLeft();
            Assert.AreEqual(testPlayer.Facing, Direction.W);
        }

        [TestMethod]
        public void TurnLeftTestWhileEast()
        {
            MapVector playerPostion = new MapVector(1, 1);
            Block[,] testGrid = CreateMapGrid();
            Player testPlayer = new Player(playerPostion, 1, 1, testGrid);
            testPlayer.Facing = Direction.E;
            testPlayer.TurnLeft();
            Assert.AreEqual(testPlayer.Facing, Direction.N);
        }

        [TestMethod]
        public void TurnLeftTestWhileSouth()
        {
            MapVector playerPostion = new MapVector(1, 1);
            Block[,] testGrid = CreateMapGrid();
            Player testPlayer = new Player(playerPostion, 1, 1, testGrid);
            testPlayer.Facing = Direction.S;
            testPlayer.TurnLeft();
            Assert.AreEqual(testPlayer.Facing, Direction.E);
        }

        [TestMethod]
        public void TurnLeftTestWhileWest()
        {
            MapVector playerPostion = new MapVector(1, 1);
            Block[,] testGrid = CreateMapGrid();
            Player testPlayer = new Player(playerPostion, 1, 1, testGrid);
            testPlayer.Facing = Direction.W;
            testPlayer.TurnLeft();
            Assert.AreEqual(testPlayer.Facing, Direction.S);
        }

        [TestMethod]
        public void TurnRightTestWhileNorth()
        {
            MapVector playerPostion = new MapVector(1, 1);
            Block[,] testGrid = CreateMapGrid();
            Player testPlayer = new Player(playerPostion, 1, 1, testGrid);
            testPlayer.TurnRight();
            Assert.AreEqual(testPlayer.Facing, Direction.E);
        }

        [TestMethod]
        public void TurnRightTestWhileEast()
        {
            MapVector playerPostion = new MapVector(1, 1);
            Block[,] testGrid = CreateMapGrid();
            Player testPlayer = new Player(playerPostion, 1, 1, testGrid);
            testPlayer.Facing = Direction.E;
            testPlayer.TurnRight();
            Assert.AreEqual(testPlayer.Facing, Direction.S);
        }

        [TestMethod]
        public void TurnRightTestWhileSouth()
        {
            MapVector playerPostion = new MapVector(1, 1);
            Block[,] testGrid = CreateMapGrid();
            Player testPlayer = new Player(playerPostion, 1, 1, testGrid);
            testPlayer.Facing = Direction.S;
            testPlayer.TurnRight();
            Assert.AreEqual(testPlayer.Facing, Direction.W);
        }

        [TestMethod]
        public void TurnRightTestWhileWest()
        {
            MapVector playerPostion = new MapVector(1, 1);
            Block[,] testGrid = CreateMapGrid();
            Player testPlayer = new Player(playerPostion, 1, 1, testGrid);
            testPlayer.Facing = Direction.W;
            testPlayer.TurnRight();
            Assert.AreEqual(testPlayer.Facing, Direction.N);
        }

        [TestMethod]
        public void ConvertDegreesToRadiansTestReturnsCorrectValue()
        {
            float testRadians = Player.ConvertDegreesToRadians(90);
            float resultRadians = (float)((Math.PI / 180) * 90);
            Assert.AreEqual(testRadians, resultRadians);
        }

    }
}