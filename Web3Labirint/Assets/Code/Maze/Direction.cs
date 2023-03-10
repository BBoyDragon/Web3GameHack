using System;

public enum Direction : int
{
    Top = -2,
    Bottom = 2, 
    Left = -1,
    Right = 1,
}

public static class DirectionMetohods
{
    public static (int x, int y) ApplyDirection(Direction dir, int x, int y)
    {
        return (x + (int)dir % 2, y + (int)dir / 2);
    } 

    public static Direction GetOposite (Direction dir)
    {
        return (Direction)(-(int)dir);
    }

    public static Direction GenerateRandomDirection(Random rand)
    {
        Array directions = Enum.GetValues(typeof(Direction));
        return (Direction)directions.GetValue(rand.Next(directions.Length));
    }

}