using Maze;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MazeHuntKillTests")]
namespace MazeHuntKill;
public class MazeCreationHuntKill
{
    public static IMapProvider CreateMazeHuntKill(int? seed = null)
    { return new MazeHuntKill(seed); }
}

internal class MazeHuntKill : IMapProvider
{
    private int? _seed;
    public int? Seed
    {
        get { return _seed; }
        private set { _seed = value; }
    }
    private Random _random;
    public Random CurrentRandom
    {
        get { return _random; }
        private set { _random = value; }
    }
    public MazeHuntKill(int? seed = null)
    {
        _seed = seed;

        if (seed != null)
        {_random = new Random((int)seed);}
        else
        {_random = new Random();}
    }

    public Direction[,] CreateMap(int width, int height)
    {
        if (width <= 0 || height <= 0)
        {
            throw new ArgumentException("Width and height must be greater than zero.");
        }

        Direction[,] direction = new Direction[((height - 1) / 2), ((width - 1) / 2)];
        MapVector startingPosition = new MapVector(_random.Next(((width - 1) / 2)), _random.Next(((height - 1) / 2)));

        do
        {
            do
            {
                startingPosition = Walking(direction, startingPosition);
            }
            while (startingPosition.X != -1 && startingPosition.Y != -1);

            startingPosition = Hunt(direction);
        }
        while (startingPosition.X != -1 && startingPosition.Y != -1);

        return direction;
    }

    public Direction[,] CreateMap()
    {
        return CreateMap(5,5);
    }

    private MapVector Walking(Direction[,] directionMatrix, MapVector startingPosition)
    {
        Direction[] shuffledDirections = ShufflingDirections();

        for (int i = 0; i < shuffledDirections.Length; i++)
        {
            MapVector wantedVector = startingPosition + shuffledDirections[i];

            if (IsMoveValid(directionMatrix, wantedVector))
            {
                if (directionMatrix[wantedVector.Y, wantedVector.X] == Direction.None)
                {
                    directionMatrix[startingPosition.Y, startingPosition.X] |= shuffledDirections[i];
                    directionMatrix[wantedVector.Y, wantedVector.X] |= GetOpposingDirection(shuffledDirections[i]);
                    return wantedVector;
                }
            }
        }

        return new MapVector(-1, -1);
    }

    private MapVector Hunt(Direction[,] directionMatrix)
    {
        for (int y = 0; y < directionMatrix.GetLength(0); y++)
        {
            for (int x = 0; x < directionMatrix.GetLength(1); x++)
            { 
                if (directionMatrix[y, x] == Direction.None)
                {
                    Direction[] shuffledDirections = ShufflingDirections();
                    for (int i = 0; i < shuffledDirections.Length; i++)
                    {
                        MapVector testVector = new MapVector(x, y) + ((MapVector)shuffledDirections[i]);

                        if (IsMoveValid(directionMatrix, testVector))
                        {
                            if (directionMatrix[testVector.Y, testVector.X] != Direction.None)
                            {
                                MapVector newStarting = new MapVector(x, y);
                                directionMatrix[newStarting.Y, newStarting.X] |= shuffledDirections[i];
                                directionMatrix[testVector.Y, testVector.X] |= GetOpposingDirection(shuffledDirections[i]);
                                return newStarting;
                            }
                        }
                    }
                }
            }
        }

        return new MapVector(-1, -1);
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
