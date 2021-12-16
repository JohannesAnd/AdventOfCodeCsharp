using NUnit.Framework;

namespace AdventOfCode2021.test;

public class Day16Tests
{
    private readonly Day16 _day = new();

    [Test]
    public void Part1()
    {
        Assert.AreEqual(879, _day.Part1());
    }

    [Test]
    public void Part2()
    {
        Assert.AreEqual(-1, _day.Part2());
    }
}