using NUnit.Framework;

namespace AdventOfCode2021.test;

public class Day5Tests
{
    private readonly Day5 _day = new();

    [Test]
    public void Part1()
    {
        Assert.AreEqual(4745, _day.Part1());
    }

    [Test]
    public void Part2()
    {
        Assert.AreEqual(18442, _day.Part2());
    }
}