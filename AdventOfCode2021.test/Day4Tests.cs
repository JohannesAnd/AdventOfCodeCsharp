using NUnit.Framework;

namespace AdventOfCode2021.test;

public class Day4Tests
{
    private readonly Day4 _day = new();

    [Test]
    public void Part1()
    {
        Assert.AreEqual(2496, _day.Part1());
    }

    [Test]
    public void Part2()
    {
        Assert.AreEqual(25925, _day.Part2());
    }
}