using NUnit.Framework;

namespace AdventOfCode2021.test;

public class Day10Tests
{
    private readonly Day10 _day = new();

    [Test]
    public void Part1()
    {
        Assert.AreEqual(315693, _day.Part1());
    }

    [Test]
    public void Part2()
    {
        Assert.AreEqual(288957, _day.Part2());
    }
}