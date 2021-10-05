namespace MarsMission
{
    public class Rover
    {
        private int X { get; set; }
        private int Y { get; set; }
        private Direction Direction { get; set; }
        private string Path { get; set; }

        public Rover(int x, int y, Direction direction)
        {
            X = x;
            Y = y;
            Direction = direction;
        }
        public Rover(int x, int y, Direction direction, string path)
        {
            X = x;
            Y = y;
            Direction = direction;
            Path = path;
        }
    }

    public enum Direction
    {
        N = 0, // y+1
        E = 1, // x+1
        S = 2, // y-1
        W = 3  // x-1
    }
}