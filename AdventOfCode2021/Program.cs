global using System.Collections.Generic;
global using System.Linq;
global using System;
using System.Diagnostics;

namespace AdventOfCode2021;

class Program
{
    static void Main()
    {
        var totalWatch = Stopwatch.StartNew();

        var day1 = new Day1();
        var day2 = new Day2();
        var day3 = new Day3();
        var day4 = new Day4();
        var day5 = new Day5();
        var day6 = new Day6();
        var day7 = new Day7();
        var day8 = new Day8();
        var day9 = new Day9();
        var day10 = new Day10();
        var day11 = new Day11();
        var day12 = new Day12();
        var day13 = new Day13();
        var day14 = new Day14();
        var day15 = new Day15();

        var days = new Parts[]
            { day1, day2, day3, day4, day5, day6, day7, day8, day9, day10, day11, day12, day13, day14, day15 };

        var task = "Task".PadRight(13);
        var result = "Result".PadLeft(20);
        var time = "Time in ms".PadLeft(10);
        
        Console.WriteLine($"{task} | {result} | {time}");
        Console.WriteLine("---------------------------------------------------");
        foreach (var day in days)
        {
            var dayString = day.GetType().ToString().Split('.')[1].PadLeft(5);

            var part1Watch = Stopwatch.StartNew();
            var part1Result = day.Part1();
            part1Watch.Stop();
            var part1Time = part1Watch.ElapsedMilliseconds;

            var part2Watch = Stopwatch.StartNew();
            var part2Result = day.Part2();


            part2Watch.Stop();
            var part2Time = part2Watch.ElapsedMilliseconds;

            if (part2Result is string)
            {
                part2Result = "[string]";
            }

            part1Result = part1Result.ToString().PadLeft(20);
            part2Result = part2Result.ToString().PadLeft(20);

            var part1String = $"{dayString}: Part 1".PadRight(10);
            var part2String = $"{dayString}: Part 2".PadRight(10);
            
            var part1TimeString = part1Time.ToString().PadLeft(10);
            var part2TimeString = part2Time.ToString().PadLeft(10);

            Console.WriteLine($"{part1String} | {part1Result} | {part1TimeString} |");
            Console.WriteLine($"{part2String} | {part2Result} | {part2TimeString} |");
        }

        totalWatch.Stop();

        Console.WriteLine($"Total time: {totalWatch.ElapsedMilliseconds}ms");
    }
}