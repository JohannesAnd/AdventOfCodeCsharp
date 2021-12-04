using NUnit.Framework;

namespace AdventOfCode2021.test;

public class Day2Tests
{
    private readonly Day2 _day = new();

    [Test]
    public void Part1()
    {
        Assert.AreEqual(1893605, _day.Part1());
    }

    [Test]
    public void Part2()
    {
        Assert.AreEqual(2120734350, _day.Part2());
    }
}