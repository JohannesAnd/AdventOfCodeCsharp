using NUnit.Framework;

namespace AdventOfCode2021.test;

public class Day14Tests
{
    private readonly Day14 _day = new();

    [Test]
    public void Part1()
    {
        Assert.AreEqual(2170, _day.Part1());
    }

    [Test]
    public void Part2()
    {
        Assert.AreEqual(2422444761283, _day.Part2());
    }
}