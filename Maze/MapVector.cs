using System.Buffers.Text;
using System.ComponentModel;

namespace Maze
{
    public class MapVector : IMapVector
    { 

        public bool IsValid => X > 0 && Y > 0;

        public int X { get; set; }

        public int Y { get; set; }

        public MapVector(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool InsideBoundary(int width, int height)
        {
            return IsValid && X < width && Y < height;
        }

        public double Magnitude()
        {
            return Math.Sqrt(X * X + Y * Y);
        }

        public double GoalToPlayerMagnitude(MapVector playerPosition)
        {
            double distanceA = Math.Abs(playerPosition.X - X);
            double distanceB = Math.Abs(playerPosition.Y - Y);
            return Math.Sqrt(Math.Pow(distanceA, 2) + Math.Pow(distanceB, 2));
        }

        public bool IsDeadEnd(Block[,] mapGrid)
        {
            int deadEndCount = 0;
            if (mapGrid[X, Y - 1] == Block.Solid) deadEndCount++;
            if (mapGrid[X, Y + 1] == Block.Solid) deadEndCount++;
            if (mapGrid[X - 1, Y] == Block.Solid) deadEndCount++;
            if (mapGrid[X + 1, Y] == Block.Solid) deadEndCount++;

            if (deadEndCount == 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static MapVector operator + (MapVector a, MapVector b)
        {
            return new MapVector(a.X + b.X, a.Y + b.Y);
        }

        public static MapVector operator -(MapVector a, MapVector b)
        {
            return new MapVector(a.X - b.X, a.Y - b.Y);
        }

        public static MapVector operator *(MapVector a, int b)
        {
            return new MapVector(a.X * b, a.Y * b);
        }

        public static implicit operator MapVector(Direction direction)
        {
            int x = 0;
            int y = 0;

            switch (direction)
            {
                case Direction.N:
                    y--;
                    break;
                case Direction.E:
                    x++;
                    break;
                case Direction.S:
                    y++;
                    break;
                case Direction.W:
                    x--;
                    break;
            }
            return new MapVector(x, y);
        }

        public override bool Equals(object? obj)
        {
            return obj is MapVector vector && X == vector.X && Y == vector.Y;
        }
    }
}