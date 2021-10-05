using System;
using System.Collections.Generic;

namespace MarsMission
{
    public class Plateau
    {
        /// <summary>
        /// size of the plateau 
        /// </summary>
        internal int X { get; set; }

        /// <summary>
        /// size of the plateau 
        /// </summary>
        internal int Y { get; set; }

        /// <summary>
        /// rovers on the plateau 
        /// </summary>
        internal List<Rover> Rovers { get; set; }
    }
}