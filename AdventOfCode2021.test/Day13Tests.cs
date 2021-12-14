using NUnit.Framework;

namespace AdventOfCode2021.test;

public class Day13Tests
{
    private readonly Day13 _day = new();

    [Test]
    public void Part1()
    {
        Assert.AreEqual(669, _day.Part1());
    }

    [Test]
    public void Part2()
    {
        Assert.AreEqual("X__X_XXXX_XXXX_XXXX__XX__X__X__XX____XX\nX__X_X____X_______X_X__X_X__X_X__X____X\nX__X_XXX__XXX____X__X____X__X_X_______X\nX__X_X____X_____X___X____X__X_X_______X\nX__X_X____X____X____X__X_X__X_X__X_X__X\n_XX__XXXX_X____XXXX__XX___XX___XX___XX_", _day.Part2());
    }
}