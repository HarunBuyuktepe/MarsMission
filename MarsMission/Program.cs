using System;
using System.Collections.Generic;
using System.IO;

namespace MarsMission
{
    class Program
    {
        static void Main(string[] args)
        {
            string text;
            try
            {
                var path = GetPath();
                text = File.ReadAllText(path);
                text = text.Replace(" ", "").Replace("\n", "")
                    .Replace("\r", "");
            }
            catch (Exception e)
            {
                Console.WriteLine("Eror while reading text");
                Console.WriteLine(e);
                throw;
            }
            ControlInputFile(text);

            Plateau plateau = PreparePlateau(text);
            IsRoverPositionValid(plateau);
            foreach (var rover in plateau.Rovers)
            {
                rover.MoveRover();
                Console.WriteLine(rover.X + " " + rover.Y + " " + rover.Direction);
            }
        }

        /// <summary>
        /// genaral validations  
        /// </summary>
        /// <param name="text"></param>
        private static void ControlInputFile(string text)
        {
            ExpectedCharsControl(text);
            IsPlateauExist(text);
        }

        /// <summary>
        /// Check rovers' positions
        /// </summary>
        /// <param name="plateau"></param>
        private static void IsRoverPositionValid(Plateau plateau)
        {
            foreach (var rover in plateau.Rovers)
            {
                if (rover.Y > plateau.Y || rover.Y > plateau.Y)
                {
                    Console.WriteLine("Rover can not be outside of the plateau");
                    System.Environment.Exit(0);
                }
            }
        }

        /// <summary>
        /// validate plateau size
        /// </summary>
        /// <param name="text"></param>
        private static void IsPlateauExist(string text)
        {
            if (!Char.IsDigit(text[0]) || !Char.IsDigit(text[1]))
            {
                Console.WriteLine("Plateau does not exist");
                System.Environment.Exit(0);
            }
        }

        /// <summary>
        /// validate the input
        /// </summary>
        /// <param name="text"></param>
        private static void ExpectedCharsControl(string text)
        {
            var ExpectedChars = GetExpectedChars();
            foreach (var t in text)
            {
                if (!ExpectedChars.Contains(t))
                {
                    Console.WriteLine("Txt file must have proper input");
                    System.Environment.Exit(0);
                }
            }
        }

        /// <summary>
        /// prepare plateau to move
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static Plateau PreparePlateau(string text)
        {
            var plateau = new Plateau();
            plateau.X = Convert.ToInt32(text[0]);
            plateau.Y = Convert.ToInt32(text[1]);
            var afterPlateau = text.Substring(2);
            plateau.Rovers = PrepareRovers(afterPlateau, plateau);
            return plateau;
        }

        /// <summary>
        /// create rovers and put on the plateau, ready to move 
        /// </summary>
        /// <param name="afterPlateau"></param>
        /// <param name="plateau"></param>
        /// <returns></returns>
        private static List<Rover> PrepareRovers(string afterPlateau, Plateau plateau)
        {
            int i = 3;
            List<string> roversPath = new List<string>();
            List<Direction> directions = new List<Direction>();
            List<int> xCords = new List<int>();
            List<int> yCords = new List<int>();

            var tempRoute = "";
            var direction = (Direction) Enum.Parse(typeof(Direction), afterPlateau[2].ToString());
            var x = Convert.ToInt32(afterPlateau[0].ToString());
            var y = Convert.ToInt32(afterPlateau[1].ToString());
            while (i < afterPlateau.Length)
            {
                if (Char.IsDigit(afterPlateau[i]) && Char.IsDigit(afterPlateau[i + 1]))
                {
                    roversPath.Add(tempRoute);
                    directions.Add((Direction) Enum.Parse(typeof(Direction), direction.ToString()));
                    xCords.Add(x);
                    yCords.Add(y);
                    x = Convert.ToInt32(afterPlateau[i].ToString());
                    y = Convert.ToInt32(afterPlateau[i + 1].ToString());
                    direction = (Direction) Enum.Parse(typeof(Direction), afterPlateau[i + 2].ToString());
                    i = i + 2;
                    tempRoute = "";
                }
                else
                {
                    tempRoute += afterPlateau[i];
                }

                i++;
            }

            roversPath.Add(tempRoute);
            directions.Add(direction);
            xCords.Add(x);
            yCords.Add(y);
            List<Rover> rovers = new List<Rover>();
            for (int j = 0; j < roversPath.Count; j++)
            {
                // Console.WriteLine(
                //     xCords[j].ToString() + yCords[j].ToString() + directions[j].ToString() + roversPath[j]);
                rovers.Add(new Rover(xCords[j], yCords[j], directions[j], roversPath[j]));
                rovers[j].plateauX = plateau.X;
                rovers[j].plateauY = plateau.Y;
            }

            return rovers;
        }

        //path of the input file
        private static string GetPath()
        {
            var path = Environment.CurrentDirectory.Replace(@"bin\Debug\net461", "")
                .Replace(@"bin\Release\net461", "");
            path = Path.Combine(path, @"MyText.txt");
            return path;
        }

        /// <summary>
        /// expected characters
        /// </summary>
        /// <returns></returns>
        private static List<char> GetExpectedChars()
        {
            List<char> ExpectedChars = new List<char>();
            ExpectedChars.AddRange(new List<char>
                {
                    'N', 'S', 'W', 'E', 'M', 'R', 'L', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0'
                }
            );
            return ExpectedChars;
        }
    }
}