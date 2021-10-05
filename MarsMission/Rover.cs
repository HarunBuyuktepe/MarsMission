using System;

namespace MarsMission
{
    public class Rover
    {
        /// <summary>
        /// X position
        /// </summary>
        internal int X { get; set; }

        /// <summary>
        /// Y position on plateau
        /// </summary>
        internal int Y { get; set; }

        /// <summary>
        /// plateauX size
        /// </summary>
        internal int plateauX { get; set; }

        /// <summary>
        /// plateauY size
        /// </summary>
        internal int plateauY { get; set; }

        /// <summary>
        /// direction referenced of 0,0
        /// </summary>
        internal Direction Direction { get; set; }

        /// <summary>
        /// given path from txt
        /// </summary>
        internal string Path { get; set; }

        /// <summary>
        /// if is last position, true 
        /// </summary>
        internal bool Moved { get; set; }

        public Rover(int x, int y, Direction direction, string path)
        {
            X = x;
            Y = y;
            Direction = direction;
            Path = path;
            Moved = false;
        }

        public void MoveRover()
        {
            foreach (var instruction in Path)
            {
                int nextvalue;
                switch (instruction)
                {
                    //(Direction) Enum.Parse(typeof(Direction), afterPlateau[2].ToString())
                    case 'R':
                        nextvalue = ((int) Direction + 1) % 4;
                        Direction = GetNewDirection(nextvalue);
                        break;
                    case 'L':
                        nextvalue = ((int) Direction + 3) % 4;
                        Direction = GetNewDirection(nextvalue);
                        break;
                    case 'M':
                        switch (Direction)
                        {
                            case Direction.N:
                                Y += 1;
                                break;
                            case Direction.E:
                                X += 1;
                                break;
                            case Direction.S:
                                Y -= 1;
                                break;
                            case Direction.W:
                                X -= 1;
                                break;
                        }

                        if (X > plateauX)
                        {
                            X = plateauX;
                        }
                        else if (X < 0)
                        {
                            X = 0;
                        }

                        if (Y > plateauY)
                        {
                            Y = plateauY;
                        }
                        else if (Y < 0)
                        {
                            Y = 0;
                        }

                        break;
                }
            }

            Moved = true;
        }
        
        public Direction GetNewDirection(int nextvalue)
        {
            return (Direction) Enum.ToObject(typeof(Direction), nextvalue);
        }
    }

    // R represents direction + 1 ; L --> direction -1
    public enum Direction
    {
        N = 0, // y+1
        E = 1, // x+1
        S = 2, // y-1
        W = 3 // x-1
    }
}