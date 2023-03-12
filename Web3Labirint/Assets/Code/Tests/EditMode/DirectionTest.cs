using NUnit.Framework;

public class DirectionTest
{
    private static System.Random _random = new();

    [Test]
    public void ApplyDirectionTest()
    {
        int x = _random.Next();
        int y = _random.Next();

        Assert.AreEqual(new UnityEngine.Vector2Int(x, y - 1), DirectionMetohods.ApplyDirection(new(x, y), Direction.Top));
        Assert.AreEqual(new UnityEngine.Vector2Int(x, y + 1), DirectionMetohods.ApplyDirection(new(x, y), Direction.Bottom));
        Assert.AreEqual(new UnityEngine.Vector2Int(x - 1, y), DirectionMetohods.ApplyDirection(new(x, y), Direction.Left));
        Assert.AreEqual(new UnityEngine.Vector2Int(x + 1, y), DirectionMetohods.ApplyDirection(new(x, y), Direction.Right));
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
