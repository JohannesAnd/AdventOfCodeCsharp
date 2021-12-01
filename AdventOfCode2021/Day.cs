using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2021
{
    public class Day
    {
        protected readonly string[] linesStrings;
        protected readonly int[] linesInts;

        protected Day()
        {
            var path = Path.Combine(Environment.CurrentDirectory, "..", "..", "..", "Input", "day1.txt");
            

            linesStrings = File.ReadAllLines(path);
            linesInts = File.ReadAllLines(path).Select(int.Parse).ToArray();

        }
    }
}