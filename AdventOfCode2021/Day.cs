using System;
using System.IO;
using System.Linq;

namespace AdventOfCode2021
{
    public class Day
    {
        protected readonly string[] LinesStrings;

        protected int[] LinesInts => LinesStrings.Select(int.Parse).ToArray();


        protected Day(string filename)
        {
            var path = Path.Combine(Environment.CurrentDirectory, "..", "..", "..", "Input", filename);
            LinesStrings = File.ReadAllLines(path);
        }
    }
}