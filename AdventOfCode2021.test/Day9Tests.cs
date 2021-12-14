using NUnit.Framework;

namespace AdventOfCode2021.test;

public class Day9Tests
{
    private readonly Day9 _day = new();

    [Test]
    public void Part1()
    {
        Assert.AreEqual(530, _day.Part1());
    }

    [Test]
    public void Part2()
    {
        Assert.AreEqual(1019494, _day.Part2());
    }
}