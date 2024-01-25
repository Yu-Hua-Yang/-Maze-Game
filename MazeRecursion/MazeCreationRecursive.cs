using Maze;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MazeRecursionTests")]
namespace MazeRecursion
{
    public class MazeCreationRecursive
    {
        public static IMapProvider CreateMazeRecursion(int version, int? seed = null)
        {
            if (version == 1)
            {
                return new MazeRecursion(seed);
            }
            else if (version == 2)
            {
                return new MazeRecursionV2(seed);
            }
            throw new ArgumentException("Invalid version number");
        }
    }

    internal class MazeRecursion : IMapProvider
    {
        private int? _seed;
        private Random _random;

        public int? Seed
        {
            get { return _seed; }
            private set { _seed = value; }
        }

        public Random CurrentRandom
        {
            get { return _random; }
            private set { _random = value; }
        }

        public MazeRecursion(int? seed = null)
        {
            _seed = seed;
            _random = (seed != null) ? new Random((int)seed) : new Random();
        }

        public Direction[,] CreateMap(int width, int height)
        {
            Direction[,] direction = new Direction[((height - 1) / 2), ((width - 1) / 2)];
            MapVector startingPosition = new MapVector(_random.Next(((width - 1) / 2)), _random.Next(((height - 1) / 2)));
            List<MapVector> seenMapVectors = new List<MapVector>
            {
                startingPosition
            };

            Walking(direction, startingPosition, seenMapVectors);

            return direction;
        }

        public Direction[,] CreateMap()
        {
            return CreateMap(5, 5);
        }

        private Direction[,] Walking(Direction[,] directionMatrix, MapVector startingPosition, List<MapVector> seenMapVectors)
        {
            Direction[] shuffledDirections = ShufflingDirections();

            for (int i = 0; i < shuffledDirections.Length; i++)
            {
                MapVector wantedVector = startingPosition + ((MapVector)shuffledDirections[i]);

                if (IsMoveValid(directionMatrix, wantedVector))
                {
                    if (directionMatrix[wantedVector.Y, wantedVector.X] == Direction.None && !seenMapVectors.Contains(wantedVector))
                    {
                        directionMatrix[startingPosition.Y, startingPosition.X] |= shuffledDirections[i];
                        directionMatrix[wantedVector.Y, wantedVector.X] |= GetOpposingDirection(shuffledDirections[i]);
                        seenMapVectors.Add(wantedVector);
                        Walking(directionMatrix, seenMapVectors.Last(), seenMapVectors);
                    }
                }
            }

            return directionMatrix;
        }

        private bool IsMoveValid(Direction[,] directionMatrix, MapVector wantedVector)
        {
            if (wantedVector.X < 0 || wantedVector.Y < 0)
            {
                return false;
            }
            else if (wantedVector.X > directionMatrix.GetLength(1) - 1 || wantedVector.Y > directionMatrix.GetLength(0) - 1)
            {
                return false;
            }
            return true;
        }

        private Direction[] ShufflingDirections()
        {
            Direction[] directions = new Direction[] { Direction.N, Direction.E, Direction.S, Direction.W };

            for (int i = 0; i < directions.Length; i++)
            {
                int j = _random.Next(i, directions.Length);
                Direction tempDirection = directions[i];
                directions[i] = directions[j];
                directions[j] = tempDirection;
            }

            return directions;
        }

        private static Direction GetOpposingDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.N:
                    return Direction.S;
                case Direction.E:
                    return Direction.W;
                case Direction.S:
                    return Direction.N;
                case Direction.W:
                    return Direction.E;
                case Direction.None:
                default:
                    return Direction.None;

            }
        }

    }


    internal class MazeRecursionV2 : IMapProvider
    {
        private Random _random;
        private Direction[] _directions = new Direction[] { Direction.N, Direction.E, Direction.S, Direction.W };

        public MazeRecursionV2(int? seed = null)
        {
            if (seed != null)
            {
                _random = new Random((int)seed);
            }
            else
            {
                _random = new Random();
            }
        }

        public Direction[,] CreateMap(int height, int width)
        {
            Direction[,] directionMatrix = new Direction[(height - 1) / 2, (width - 1) / 2];
            MapVector startingMapVector = new MapVector(0, 0);
            List<MapVector> seenMapVectors = new List<MapVector>
            {
                startingMapVector
            };

            Walk(ref directionMatrix, startingMapVector, ref seenMapVectors);

            return directionMatrix;
        }

        public Direction[,] CreateMap()
        {
            return CreateMap(5, 5);
        }

        private Direction[,] Walk(ref Direction[,] directionMatrix, MapVector currentPosition, ref List<MapVector> seenMapVectors)
        {
            Direction[] shuffledDirections = ShufflingDirections();

            foreach (Direction direction in shuffledDirections)
            {
                MapVector wantedVector = currentPosition + direction;

                if (IsMoveValid(ref directionMatrix, wantedVector))
                {
                    if (directionMatrix[wantedVector.Y, wantedVector.X] == Direction.None && !seenMapVectors.Contains(wantedVector))
                    {
                        directionMatrix[currentPosition.Y, currentPosition.X] |= direction;
                        directionMatrix[wantedVector.Y, wantedVector.X] |= GetOpposingDirection(direction);
                        seenMapVectors.Add(wantedVector);
                        Walk(ref directionMatrix, seenMapVectors.Last(), ref seenMapVectors);
                    }
                }
            }
            return directionMatrix;
        }

        private bool IsMoveValid(ref Direction[,] directionMatrix, MapVector wantedVector)
        {
            if (wantedVector.X < 0 || wantedVector.Y < 0)
            {
                return false;
            }
            else if (wantedVector.X > directionMatrix.GetLength(1) - 1 || wantedVector.Y > directionMatrix.GetLength(0) - 1)
            {
                return false;
            }
            return true;
        }

        private Direction[] ShufflingDirections()
        {
            for (int i = 0; i < _directions.Length; i++)
            {
                int j = _random.Next(i, _directions.Length);
                Direction tempDirection = _directions[i];
                _directions[i] = _directions[j];
                _directions[j] = tempDirection;
            }

            return _directions;
        }

        private static Direction GetOpposingDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.N:
                    return Direction.S;
                case Direction.E:
                    return Direction.W;
                case Direction.S:
                    return Direction.N;
                case Direction.W:
                    return Direction.E;
                case Direction.None:
                default:
                    return Direction.None;
            }
        }
    }
}
