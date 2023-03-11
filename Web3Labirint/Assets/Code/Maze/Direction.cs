using System;
using System.Collections.Generic;
public enum Direction : int
{
    Top,
    Bottom, 
    Left,
    Right,
}

public static class DirectionMetohods
{
    private static Dictionary<Direction, (int, int)> _applyMap = new()
    {
        { Direction.Top, (0, -1)},    
        { Direction.Bottom, (0, 1)},    
        { Direction.Left, (-1, 0)},    
        { Direction.Right, (1, 0)},    
    };

    public static (int x, int y) ApplyDirection(Direction direction, int x, int y)
    {
        (int dx, int dy) = _applyMap[direction];
        return (x + dx, y + dy);
    } 
    
    private static Dictionary<Direction, Direction> _opositeMap = new() 
    {
        { Direction.Top, Direction.Bottom},    
        { Direction.Bottom, Direction.Top},    
        { Direction.Left, Direction.Right},    
        { Direction.Right, Direction.Left},    
    };

    public static Direction GetOposite (Direction direction)
    {
        return _opositeMap[direction];
    }

    public static Direction GenerateRandomDirection(Random rand)
    {
        Array directions = Enum.GetValues(typeof(Direction));
        return (Direction)directions.GetValue(rand.Next(directions.Length));
    }

}