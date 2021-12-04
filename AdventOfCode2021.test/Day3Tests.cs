using NUnit.Framework;

namespace AdventOfCode2021.test;

public class Day3Tests
{
    private readonly Day3 _day = new();

    [Test]
    public void Part1()
    {
        Assert.AreEqual(2640986, _day.Part1());
    }

    [Test]
    public void Part2()
    {
        Assert.AreEqual(6822109, _day.Part2());
    }
}