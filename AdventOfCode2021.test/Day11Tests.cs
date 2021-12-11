using NUnit.Framework;

namespace AdventOfCode2021.test;

public class Day11Tests
{
    private readonly Day11 _day = new();

    [Test]
    public void Part1()
    {
        Assert.AreEqual(1601, _day.Part1());
    }

    [Test]
    public void Part2()
    {
        Assert.AreEqual(368, _day.Part2());
    }
}