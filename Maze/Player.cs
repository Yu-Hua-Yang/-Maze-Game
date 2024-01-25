namespace Maze
{
    public class Player : IPlayer
    {
        public Direction Facing { get; set; }

        public MapVector Position { get; set; }

        public Block[,] MapGrid { get; set; }

        public int StartX { get; set; }

        public int StartY { get; set; }

        public Player(MapVector position, int xCoordinate, int yCoodinate, Block[,] mapGrid)
        {
            Position = position;
            StartX = xCoordinate;
            StartY = yCoodinate;
            Facing = Direction.N;
            MapGrid = mapGrid;
        }

        public float GetRotation()
        {
            float radians = 0;
            switch (Facing)
            {
                case Direction.N:
                    radians = ConvertDegreesToRadians(0);
                    break;
                case Direction.E:
                    radians = ConvertDegreesToRadians(90);
                    break;
                case Direction.S:
                    radians = ConvertDegreesToRadians(180);
                    break;
                case Direction.W:
                    radians = ConvertDegreesToRadians(270);
                    break;
            }
            return radians;
        }

        //Add error validation if move is valid
        public void MoveBackward()
        {
            MapVector castedVector = Facing;
            int negativeScalar = -1;
            MapVector multipliedVector = castedVector * negativeScalar;
            MapVector checkValidVector = Position + multipliedVector;
            if (MapGrid[checkValidVector.X, checkValidVector.Y] == Block.Empty)
            {
                Position += multipliedVector;
            }
        }

        //Add error validation if move is valid
        public void MoveForward()
        {
            MapVector castedVector = Facing;
            MapVector checkValidVector = Position + castedVector;
            if (MapGrid[checkValidVector.X, checkValidVector.Y] == Block.Empty)
            {
                Position += castedVector;
            }
        }

        public void TurnLeft()
        {
            switch (Facing)
            {
                case Direction.N:
                    Facing = Direction.W;
                    break;
                case Direction.E:
                    Facing = Direction.N;
                    break;
                case Direction.S:
                    Facing = Direction.E;
                    break;
                case Direction.W:
                    Facing = Direction.S;
                    break;
            }
        }

        public void TurnRight()
        {
            switch (Facing)
            {
                case Direction.N:
                    Facing = Direction.E;
                    break;
                case Direction.E:
                    Facing = Direction.S;
                    break;
                case Direction.S:
                    Facing = Direction.W;
                    break;
                case Direction.W:
                    Facing = Direction.N;
                    break;
            }
        }

        public static float ConvertDegreesToRadians(double degrees)
        {
            float radians = (float)((Math.PI / 180) * degrees);
            return (radians);
        }
    }
}