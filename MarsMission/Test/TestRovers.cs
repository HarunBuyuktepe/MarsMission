using System;
using NUnit.Framework;

namespace MarsMission.Test
{
    [TestFixture]
    public class TestRovers
    {
        [TestCase(2, 3, 'S', 2, 2)]
        [TestCase(5, 3, 'S', 5, 2)]
        [TestCase(5, 3, 'E', 5, 3)]
        [TestCase(0, 5, 'N', 0, 5)]
        public void MoveRoverWithGivenRange(int x, int y, char direction, int expectedX, int expectedY)
        {
            var rover = new Rover(x, y, (Direction) Enum.Parse(typeof(Direction), direction.ToString()), "M");
            rover.plateauX = 5;
            rover.plateauY = 5;
            rover.MoveRover();
            Assert.That(rover.X == expectedX);
            Assert.That(rover.Y == expectedY);
        }

        [TestCase(0, 5, 'N', "R", "E")]
        [TestCase(0, 5, 'N', "L", "W")]
        public void ChangeRoverDirection(int x, int y, char direction, string command, string newDirection)
        {
            var rover = new Rover(x, y, (Direction) Enum.Parse(typeof(Direction), direction.ToString()), command);
            rover.plateauX = 5;
            rover.plateauY = 5;
            rover.MoveRover();
            Assert.That(rover.Direction.ToString() == newDirection);
        }
    }
}