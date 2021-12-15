using NUnit.Framework;

namespace AdventOfCode2021.test;

public class Day15Tests
{
    private readonly Day15 _day = new();

    [Test]
    public void Part1()
    {
        Assert.AreEqual(595, _day.Part1());
    }

    [Test]
    public void Part2()
    {
        Assert.AreEqual(2914, _day.Part2());
    }
}