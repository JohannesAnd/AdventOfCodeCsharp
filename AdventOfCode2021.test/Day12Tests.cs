using NUnit.Framework;

namespace AdventOfCode2021.test;

public class Day12Tests
{
    private readonly Day12 _day = new();

    [Test]
    public void Part1()
    {
        Assert.AreEqual(4775, _day.Part1());
    }

    [Test]
    public void Part2()
    {
        Assert.AreEqual(152480, _day.Part2());
    }
}