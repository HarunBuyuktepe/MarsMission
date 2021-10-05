using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

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
                Console.WriteLine(text);
                // First 2 char plateau others Rovers
                text = text.Replace(" ", "").Replace("\n", "")
                    .Replace("\r", "");
                //.WriteLine(text);
            }
            catch (Exception e)
            {
                Console.WriteLine("Eror while reading text");
                Console.WriteLine(e);
                throw;
            }

            ControlInputFile(text);

            Plateau plateau = PreparePlateau(text);
            plateau.MoveRovers();

        }

        private static void ControlInputFile(string text)
        {
            ExpectedCharsControl(text);
            IsPlateauExist(text);
            //IsRoverPositionValid(text);
        }

        //TODO
        private static void IsRoverPositionValid(string text)
        {
            var X = Convert.ToInt32(text[0]);
            var Y = Convert.ToInt32(text[1]);

            System.Environment.Exit(0);
        }

        private static void IsPlateauExist(string text)
        {
            if (!Char.IsDigit(text[0]) || !Char.IsDigit(text[1]))
            {
                Console.WriteLine("Plateau does not exist");
                System.Environment.Exit(0);
            }
        }

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

        private static Plateau PreparePlateau(string text)
        {
            var plateau = new Plateau();
            plateau.X = Convert.ToInt32(text[0]);
            plateau.Y = Convert.ToInt32(text[1]);
            var afterPlateau = text.Substring(2);
            plateau.Rovers = PrepareRovers(afterPlateau);
            return plateau;
        }

        private static List<Rover> PrepareRovers(string afterPlateau)
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
                    y = Convert.ToInt32(afterPlateau[i+1].ToString());
                    direction = (Direction) Enum.Parse(typeof(Direction), afterPlateau[i + 2].ToString());
                    i = i + 3;
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
                Console.WriteLine(xCords[j].ToString() + yCords[j].ToString() +directions[j].ToString() + roversPath[j]);
                rovers.Add(new Rover(xCords[j], yCords[j], directions[j], roversPath[j]));
            }
            
            return rovers;
        }

        private static string GetPath()
        {
            var path = Environment.CurrentDirectory.Replace(@"bin\Debug\net461", "")
                .Replace(@"bin\Release\net461", "");
            path = Path.Combine(path, @"MyText.txt");
            return path;
        }

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