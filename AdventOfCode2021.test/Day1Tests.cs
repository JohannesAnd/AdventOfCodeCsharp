using NUnit.Framework;

namespace AdventOfCode2021.test;

public class Day1Tests
{
    private readonly Day1 _day = new();

    [Test]
    public void Part1()
    {
        Assert.AreEqual(1226, _day.Part1());
    }

    [Test]
    public void Part2()
    {
        Assert.AreEqual(1252, _day.Part2());
    }
}