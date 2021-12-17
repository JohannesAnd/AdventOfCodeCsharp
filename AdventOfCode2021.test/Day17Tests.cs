using NUnit.Framework;

namespace AdventOfCode2021.test;

public class Day17Tests
{
    private readonly Day17 _day = new();

    [Test]
    public void Part1()
    {
        Assert.AreEqual(4186, _day.Part1());
    }

    [Test]
    public void Part2()
    {
        Assert.AreEqual(2709, _day.Part2());
    }
}