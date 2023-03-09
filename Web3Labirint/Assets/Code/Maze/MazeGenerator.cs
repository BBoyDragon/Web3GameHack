using System;
using System.Collections.Generic;

public class MazeGenerator
{
    public static Maze Generate(int sizeX, int sizeY)
    {
        var maze = new Maze(sizeX, sizeY);
        var rand = new Random();
        for (int x = 0; x <= sizeX; x++)
        {
            for (int y = 0; y <= sizeY; y++)
            {
                maze.SetHorizontalWall(x, y, rand.Next(0, 2) == 0);
                maze.SetVerticalWall(x, y, rand.Next(0, 2) == 0);
            }
        }

        return maze;
    }
}
