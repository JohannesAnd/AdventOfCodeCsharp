using NUnit.Framework;

namespace AdventOfCode2021.test;

public class Day6Tests
{
    private readonly Day6 _day = new();

    [Test]
    public void Part1()
    {
        Assert.AreEqual(360761, _day.Part1());
    }

    [Test]
    public void Part2()
    {
        Assert.AreEqual(1632779838045, _day.Part2());
    }
}