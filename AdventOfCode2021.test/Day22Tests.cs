using NUnit.Framework;

namespace AdventOfCode2021.test;

public class Day22Tests
{
    private readonly Day22 _day = new();

    [Test]
    public void Part1()
    {
        Assert.AreEqual(588200, _day.Part1());
    }

    [Test]
    public void Part2()
    {
        Assert.AreEqual(1207167990362099, _day.Part2());
    }
}