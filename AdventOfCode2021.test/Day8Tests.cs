using NUnit.Framework;

namespace AdventOfCode2021.test;

public class Day8Tests
{
    private readonly Day8 _day = new();

    [Test]
    public void Part1()
    {
        Assert.AreEqual(392, _day.Part1());
    }

    [Test]
    public void Part2()
    {
        Assert.AreEqual(1004688, _day.Part2());
    }
}