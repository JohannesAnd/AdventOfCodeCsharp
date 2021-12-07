using NUnit.Framework;

namespace AdventOfCode2021.test;

public class Day7Tests
{
    private readonly Day7 _day = new();

    [Test]
    public void Part1()
    {
        Assert.AreEqual(328318, _day.Part1());
    }

    [Test]
    public void Part2()
    {
        Assert.AreEqual(89791146, _day.Part2());
    }
}