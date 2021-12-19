using NUnit.Framework;

namespace AdventOfCode2021.test;

public class Day18Tests
{
    private readonly Day18 _day = new();

    [Test]
    public void Part1()
    {
        Assert.AreEqual(3756, _day.Part1());
    }

    [Test]
    public void Part2()
    {
        Assert.AreEqual(4585, _day.Part2());
    }

    [Test]
    public void ExplodeNoLeft()
    {
        var n = new SnailFishMath();

        n.Add("[[[[[9,8],1],2],3],4]");

        Assert.AreEqual("[[[[0,9],2],3],4]", n.GetExpression());
    }

    [Test]
    public void ExplodeNoRight()
    {
        var n = new SnailFishMath();

        n.Add("[7,[6,[5,[4,[3,2]]]]]");

        Assert.AreEqual("[7,[6,[5,[7,0]]]]", n.GetExpression());
    }

    [Test]
    public void ExplodeMiddle()
    {
        var n = new SnailFishMath();

        n.Add("[[6,[5,[4,[3,2]]]],1]");

        Assert.AreEqual("[[6,[5,[7,0]]],3]", n.GetExpression());
    }

    [Test]
    public void ExplodeMultiple()
    {
        var n = new SnailFishMath();

        n.Add("[[3,[2,[1,[7,3]]]],[6,[5,[4,[3,2]]]]]");

        Assert.AreEqual("[[3,[2,[8,0]]],[9,[5,[7,0]]]]", n.GetExpression());
    }

    [Test]
    public void Split()
    {
        var n = new SnailFishMath();

        n.Add("[10,0]");

        Assert.AreEqual("[[5,5],0]", n.GetExpression());
    }

    [Test]
    public void AddAndReduce()
    {
        var n = new SnailFishMath();

        n.Add("[[[[4,3],4],4],[7,[[8,4],9]]]");
        n.Add("[1,1]");

        Assert.AreEqual("[[[[0,7],4],[[7,8],[6,0]]],[8,1]]", n.GetExpression());
    }

    [Test]
    public void Add()
    {
        var n = new SnailFishMath();

        n.Add("[1,1]");
        n.Add("[2,2]");
        n.Add("[3,3]");
        n.Add("[4,4]");
        n.Add("[5,5]");
        n.Add("[6,6]");

        Assert.AreEqual("[[[[5,0],[7,4]],[5,5]],[6,6]]", n.GetExpression());
    }
    
    [Test]
    public void Add2()
    {
        var n = new SnailFishMath();

        n.Add("[[2,[[7,7],7]],[[5,8],[[9,3],[0,2]]]]");
        n.Add("[[[0,[5,8]],[[1,7],[9,6]]],[[4,[1,2]],[[1,4],2]]]");

        Assert.AreEqual("[[[[7,8],[6,6]],[[6,0],[7,7]]],[[[7,8],[8,8]],[[7,9],[0,6]]]]", n.GetExpression());
    }

    
    [Test]
    public void AddLarge()
    {
        var n = new SnailFishMath();
        
        n.Add("[[[0,[4,5]],[0,0]],[[[4,5],[2,6]],[9,5]]]");
        n.Add("[7,[[[3,7],[4,3]],[[6,3],[8,8]]]]");
        n.Add("[[2,[[0,8],[3,4]]],[[[6,7],1],[7,[1,6]]]]");
        n.Add("[[[[2,4],7],[6,[0,5]]],[[[6,8],[2,8]],[[2,1],[4,5]]]]");
        n.Add("[7,[5,[[3,8],[1,4]]]]");
        n.Add("[[2,[2,2]],[8,[8,1]]]");
        n.Add("[2,9]");
        n.Add("[1,[[[9,3],9],[[9,0],[0,7]]]]");
        n.Add("[[[5,[7,4]],7],1]");
        n.Add("[[[[4,2],2],6],[8,7]]");
        
        Assert.AreEqual("[[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]", n.GetExpression());
    }
    
    [Test]
    public void ComputeMagnitude()
    {
        var n = new SnailFishMath();
        
        n.Add("[[1,2],[[3,4],5]]");

        Assert.AreEqual(143, n.GetMagnitude());
    }
    
    [Test]
    public void ComputeMagnitude2()
    {
        var n = new SnailFishMath();
        
        n.Add("[[[[7,8],[6,6]],[[6,0],[7,7]]],[[[7,8],[8,8]],[[7,9],[0,6]]]]");

        Assert.AreEqual(3993, n.GetMagnitude());
    }

    [Test]
    public void BigExample()
    {
        var n = new SnailFishMath();

        n.Add("[[[0,[5,8]],[[1,7],[9,6]]],[[4,[1,2]],[[1,4],2]]]");
        n.Add("[[[5,[2,8]],4],[5,[[9,9],0]]]");
        n.Add("[6,[[[6,2],[5,6]],[[7,6],[4,7]]]]");
        n.Add("[[[6,[0,7]],[0,9]],[4,[9,[9,0]]]]");
        n.Add("[[[7,[6,4]],[3,[1,3]]],[[[5,5],1],9]]");
        n.Add("[[6,[[7,3],[3,2]]],[[[3,8],[5,7]],4]]");
        n.Add("[[[[5,4],[7,7]],8],[[8,3],8]]");
        n.Add("[[9,3],[[9,9],[6,[4,9]]]]");
        n.Add("[[2,[[7,7],7]],[[5,8],[[9,3],[0,2]]]]");
        n.Add("[[[[5,2],5],[8,[3,7]]],[[5,[7,5]],[4,4]]]");
        
        Assert.AreEqual(4140, n.GetMagnitude());
    }
}