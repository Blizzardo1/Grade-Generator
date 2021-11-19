using System;
using System.Collections.Generic;

namespace Grade_Generator
{
    internal static class Program
    {
        record Grade()
        {
            public decimal Min { get; set; }
            public decimal Max { get; set; }

            public override string ToString()
            {
                return $"{Max:#.00} - {Min:#.00}";
            }
        }
        private static void GenerateGrades(decimal points, bool granularGrading)
        {
            var grades = new Dictionary<string, Grade>();
            const decimal granularFactor = 0.01m;
            for (var x = points + granularFactor; x >= 0; x -= granularFactor)
            {
                var grade = x / points * 100;
                string letter;

                if (granularGrading)
                {
                    letter = grade switch
                    {
                        >= 97.5m => "A+",
                        >= 93.5m => "A",
                        >= 90 => "A-",
                        >= 87.5m => "B+",
                        >= 83.5m => "B",
                        >= 80 => "B-",
                        >= 77.5m => "C+",
                        >= 73.5m => "C",
                        >= 70 => "C-",
                        >= 67.5m => "D+",
                        >= 63.5m => "D",
                        >= 60 => "D-",
                        < 60 => "F"
                    };
                }
                else
                {
                    letter = grade switch
                    {
                        >= 90 => "A",
                        >= 80 => "B",
                        >= 70 => "C",
                        >= 60 => "D",
                        < 60 => "F"
                    };
                }

                if (!grades.ContainsKey(letter))
                    grades.Add(letter, new Grade());
                else
                {
                    var r = Math.Round(x, 2);
                    if (grades[letter].Max == 0)
                        grades[letter].Max = r;
                    grades[letter].Min = r;
                }
            }
            
            foreach (var (letter, grade) in grades)
            {
                Console.WriteLine($"{(granularGrading ? $"{letter,-2}" : letter)}: {grade}");
            }
        }

        private static bool GranularGrading()
        {
            while (true)
            {
                Console.Write("Would you like granular grading? [Y/n] ");
                var cr = Console.ReadKey(true).Key;
                Console.WriteLine();
                switch (cr)
                {
                    case ConsoleKey.Enter:
                        return true;
                    case ConsoleKey.Y or ConsoleKey.N:
                        return cr == ConsoleKey.Y;
                    default:
                        Console.WriteLine("Invalid answer");
                        break;
                }
            }
        }

        private static void Main(string[] args)
        {
            Console.WriteLine("Grade Table Generator\n");
            Console.WriteLine("Please enter the maximum allotted points.");
            Console.Write("Points: ");
            var response = Console.ReadLine();
            _ = decimal.TryParse(response, out var points);
            
            GenerateGrades(points, GranularGrading());
        }
    }
}