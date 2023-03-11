using System;
using System.Collections.Generic;
using UnityEngine;

public enum Direction : int
{
    Top,
    Bottom,
    Left,
    Right,
}

public static class DirectionMetohods
{
    private static readonly Dictionary<Direction, Vector2Int> _applyMap = new()
    {
        { Direction.Top, new (0, -1)},
        { Direction.Bottom, new (0, 1)},
        { Direction.Left, new (-1, 0)},
        { Direction.Right, new (1, 0)},
    };
    public static Vector2Int ApplyDirection(Vector2Int vector, Direction direction)
    {
        return vector + _applyMap[direction];
    }

    private static readonly Dictionary<Direction, Direction> _opositeMap = new()
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

    public static Direction GenerateRandomDirection(System.Random rand)
    {
        Array directions = Enum.GetValues(typeof(Direction));
        return (Direction)directions.GetValue(rand.Next(directions.Length));
    }

}