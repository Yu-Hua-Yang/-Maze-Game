using System;
using System.Text;

namespace Maze
{
    public class Map : IMap
    {   
        public IMapProvider Provider { get; set; }

        public MapVector Goal { get; set; }

        public int Height { get; set; }

        public bool IsGameFinished { get; set; }

        public Block[,] MapGrid { get; set; }

        public IPlayer Player { get; set; }

        public int Width { get; set; }

        public Map(IMapProvider provider)
        {
            Provider = provider;   
        }

        public void CreateMap()
        {
            Direction[,] directionMatrix = Provider.CreateMap();
            Height = (directionMatrix.GetLength(1) * 2) + 1;
            Width = (directionMatrix.GetLength(0) * 2) + 1;
            MapGrid = new Block[Height, Width];

            InitializeMap(directionMatrix);

            GeneratePlayerSpawn();
            GenerateGoalSpawn();    
        }

        public StringBuilder MapToString()
        {
            StringBuilder mapString = new StringBuilder();
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    if (MapGrid[j, i] == Block.Empty)
                    {
                        if (i == Player.StartX && j == Player.StartY)
                        {
                            mapString.Append("P ");
                        }
                        else if (j == Goal.X && i == Goal.Y)
                        {
                            mapString.Append("G ");
                        }
                        
                        else
                        {
                            mapString.Append("  ");
                        }
                    }
                    else
                    {
                        mapString.Append("■ ");
                    }
                }
                mapString.AppendLine();
            }
            return mapString;
        }

        private void GeneratePlayerSpawn()
        {
            Random random = new();
            int xCoordinate;
            int yCoordinate;
            MapVector mapVector;

            do
            {
                xCoordinate = random.Next(1, Width - 1);
                yCoordinate = random.Next(1, Height - 1);
                mapVector = new MapVector(yCoordinate, xCoordinate);
            }
            while (!(mapVector.InsideBoundary(Width, Height) && MapGrid[yCoordinate, xCoordinate] == Block.Empty));
            Player = new Player(mapVector, xCoordinate, yCoordinate, MapGrid);
        }

        private void GenerateGoalSpawn()
        {
            MapVector goalPosition;
            double distanceMagnitude = 0;
            int goalXCoordinate = 0;
            int goalYCoordinate = 0;
            for (int i = 1; i < Width - 1; i++)
            {
                for (int j = 1; j < Height - 1; j++)
                {
                    goalPosition = new MapVector(j, i);
                    if (goalPosition.IsDeadEnd(MapGrid) && MapGrid[j, i] == Block.Empty)
                    {
                        double tempMagnitude = goalPosition.GoalToPlayerMagnitude(Player.Position);
                        if (tempMagnitude > distanceMagnitude)
                        {
                            distanceMagnitude = tempMagnitude;
                            goalXCoordinate = j;
                            goalYCoordinate = i;
                        }
                    }
                }
            }
            Goal = new MapVector(goalXCoordinate, goalYCoordinate);
        }

        public void CreateMap(int width, int height)
        {
            Direction[,] direction = Provider.CreateMap(width, height);

            Height = direction.GetLength(1) * 2 + 1;
            Width = direction.GetLength(0) * 2 + 1;

            MapGrid = new Block[Height, Width];

            InitializeMap(direction);
            GeneratePlayerSpawn();
            GenerateGoalSpawn();
        }

        public void SaveDirectionMap(string path)
        {
            throw new NotImplementedException();
        }

        public void InitializeMap( Direction[,] directionMatrix)
        {
            int xCoordinate;
            int yCoordinate;
            for (int i = 0; i < directionMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < directionMatrix.GetLength(1); j++)
                {
                    xCoordinate = j * 2 + 1;
                    yCoordinate = i * 2 + 1;

                    MapGrid[xCoordinate, yCoordinate] = Block.Empty;

                    if ((directionMatrix[i, j] & Direction.N) > 0)
                    {
                        MapGrid[xCoordinate, yCoordinate - 1] = Block.Empty;
                    }
                    if ((directionMatrix[i, j] & Direction.E) > 0)
                    {
                        MapGrid[xCoordinate + 1, yCoordinate] = Block.Empty;
                    }
                    if ((directionMatrix[i, j] & Direction.S) > 0)
                    {
                        MapGrid[xCoordinate, yCoordinate + 1] = Block.Empty;
                    }
                    if ((directionMatrix[i, j] & Direction.W) > 0)
                    {
                        MapGrid[xCoordinate - 1, yCoordinate] = Block.Empty;
                    }
                }
            }
        }
    }
}