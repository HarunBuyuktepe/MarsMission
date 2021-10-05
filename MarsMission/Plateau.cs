using System.Collections.Generic;

namespace MarsMission
{
    public class Plateau
    {
        internal int X { get; set; }
        internal int Y { get; set; }
        internal List<Rover> Rovers { get; set; }

        public void MoveRovers()
        {
            throw new System.NotImplementedException();
        }
    }
}