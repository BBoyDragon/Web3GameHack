using System;
using System.Collections.Generic;

public class MazeGenerator
{
    // Wilson's algorithm
    public static Maze Generate(int sizeX, int sizeY)
    {
        var maze = new Maze(sizeX, sizeY);
        maze.SetAllWalls();

        Cell[,] cells = new Cell[sizeX, sizeY];

        var rand = new Random();
        int startX = rand.Next(sizeX);
        int startY = rand.Next(sizeY);

        cells[startX, startY].isVisited = true;
        cells[startX, startY].child = Direction.Undefined;

        int cellsLeft = sizeX * sizeY - 1;
        while(cellsLeft > 0)
        {
            int walkStartCellX = rand.Next(sizeX);
            int walkStartCellY = rand.Next(sizeY);
            
            // WALK!
            int curCellX = walkStartCellX;
            int curCellY = walkStartCellY;
            while(!cells[curCellX, curCellY].isVisited)
            {
                Direction dir = (Direction) (rand.Next(4) + 1);
                (int x, int y) = DirectionMetohods.ApplyDirection(dir, curCellX, curCellY);
                if (CellExists(x, y, sizeX, sizeY))
                {
                    cells[curCellX, curCellY].child = dir;
                    curCellX = x;
                    curCellY = y;
                }
            }

            // Retrace 
            while(walkStartCellX != curCellX || walkStartCellY != curCellY)
            {
                Direction dir = cells[walkStartCellX, walkStartCellY].child;
                maze.SetWallBetween(walkStartCellX, walkStartCellY, dir, false);
                cells[walkStartCellX, walkStartCellY].isVisited = true;
                (walkStartCellX, walkStartCellY) = DirectionMetohods.ApplyDirection(dir, walkStartCellX, walkStartCellY);
                --cellsLeft;
            }
        }

        return maze;
    }
    
    private struct Cell
    {
        public bool isVisited;
        public Direction child;
    }

    private static bool CellExists(int givenX, int givenY, int sizeX, int sizeY)
    {
        return 0 <= givenX && givenX < sizeX && 0 <= givenY && givenY < sizeY;
    }
}
