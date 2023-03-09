using System;

public enum Direction : int
{
    Undefined = 0,
    Top = 1,
    Bottom = 2, 
    Left = 3,
    Right = 4,
}

static class DirectionMetohods
{
    public static (int x, int y) ApplyDirection(Direction dir, int x, int y)
    {
        RequireDefined(dir, "DirectionMetohods.ApplyDirection(Direction, int, int)");
        if (dir == Direction.Top)
        {
            --y;
        } 
        else if (dir == Direction.Bottom)
        {
            ++y;
        }
        else if (dir == Direction.Left)
        {
            --x;
        }
        else if (dir == Direction.Right)
        {
            ++x;
        }
        return (x ,y);
    } 

    public static Direction GetOposite (Direction dir)
    {
        RequireDefined(dir, "DirectionMetohods.GetOposite(Direction)");
        if (dir == Direction.Top)
        {
            return Direction.Bottom;
        }
        else if (dir == Direction.Bottom)
        {
            return Direction.Top;
        }
        else if (dir == Direction.Left)
        {
            return Direction.Right;
        }
        else if (dir == Direction.Right)
        {
            return Direction.Left;
        }
        return Direction.Undefined;
    }

    public static void RequireDefined(Direction direction, string methodName)
    {
        if (direction == Direction.Undefined)
        {
            throw new ArgumentException("Can't perform '" + methodName + "' on 'Direction.Undefined'.");
        }   
    }
}