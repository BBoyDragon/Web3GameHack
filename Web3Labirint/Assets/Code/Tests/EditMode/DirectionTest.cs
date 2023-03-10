using NUnit.Framework;

public class DirectionTest
{
    private static System.Random _random = new();

    [Test]
    public void ApplyDirectionTest()
    {
        int x = _random.Next();
        int y = _random.Next();

        Assert.AreEqual((x, y - 1), DirectionMetohods.ApplyDirection(Direction.Top, x, y));
        Assert.AreEqual((x, y + 1), DirectionMetohods.ApplyDirection(Direction.Bottom, x, y));
        Assert.AreEqual((x - 1, y), DirectionMetohods.ApplyDirection(Direction.Left, x, y));
        Assert.AreEqual((x + 1, y), DirectionMetohods.ApplyDirection(Direction.Right, x, y));
    }
    
    [Test]
    public void GetOpositeTest()
    {
        Assert.AreEqual(Direction.Bottom, DirectionMetohods.GetOposite(Direction.Top));
        Assert.AreEqual(Direction.Top,    DirectionMetohods.GetOposite(Direction.Bottom));
        Assert.AreEqual(Direction.Right,  DirectionMetohods.GetOposite(Direction.Left));
        Assert.AreEqual(Direction.Left,   DirectionMetohods.GetOposite(Direction.Right));
    }
}
