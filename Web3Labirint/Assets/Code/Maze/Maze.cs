using System;
using System.Collections.Generic;

public class Maze
{
    public Maze(int sizeX, int sizeY)
    {
        _walls = new List<List<Cell>>(sizeX + 1);
        for (int i = 0; i < sizeX + 1; i++)
        {
            _walls.Add(new List<Cell>(new Cell[sizeY + 1]));
        }
    }

    public int SizeX()
    {
        return Math.Max(_walls.Count - 1, 0);
    }
    public int SizeY()
    {
        return _walls.Count > 0 ? Math.Max(_walls[0].Count - 1, 0) : 0;
    }

    public bool IsHorizontalWall(int x, int y)
    {
        return _walls[x][y].horizontalBottom;
    }
    public bool IsVerticalWall(int x, int y)
    {
        return _walls[x][y].verticalRight;
    }

    public void SetWallBetween(int cellX, int cellY, Direction dir, bool exists)
    {
        DirectionMetohods.RequireDefined(dir, "Maze.SetWallBetween(int, int, Direction, bool)");
        if (dir == Direction.Top)
        {
            SetHorizontalWall(cellX + 1, cellY, exists);
        }
        else if (dir == Direction.Bottom)
        {
            SetHorizontalWall(cellX + 1, cellY + 1, exists);
        }
        else if (dir == Direction.Left)
        {
            SetVerticalWall(cellX, cellY + 1, exists);
        }
        else if (dir == Direction.Right)
        {
            SetVerticalWall(cellX + 1, cellY + 1, exists);
        }
    }

    public void SetHorizontalWall(int x, int y, bool exists)
    {
        Cell cell = _walls[x][y];
        cell.horizontalBottom = exists;
        _walls[x][y] = cell;
    }
    public void SetVerticalWall(int x, int y, bool exists)
    {
        Cell cell = _walls[x][y];
        cell.verticalRight = exists;
        _walls[x][y] = cell;
    }

    public void SetAllWalls()
    {
        for (int x = 0; x <= SizeX(); x++)
        {
            for (int y = 0; y <= SizeY(); y++)
            {
                SetHorizontalWall(x, y, true);
                SetVerticalWall(x, y, true);
            }
        }
    }

    public void CreateBorder()
    {
        int sizeX = _walls.Count;
        int sizeY = _walls[0].Count;
        CreateHorizontalBorders(sizeX, sizeY);
        CreateVerticalBorders(sizeX, sizeY);
    }
    private void CreateHorizontalBorders(int sizeX, int sizeY)
    {
        for (int x = 0; x < sizeX; x++)
        {
            SetHorizontalWall(x, 0, true);
            SetHorizontalWall(x, sizeY - 1, true);
        }
    }
    private void CreateVerticalBorders(int sizeX, int sizeY)
    {
        for (int y = 0; y < sizeY; y++)
        {
            SetVerticalWall(0, y, true);
            SetVerticalWall(sizeX - 1, y, true);
        }
    }

    private List<List<Cell>> _walls;

    private struct Cell
    {
        public Cell(bool isHorizontal, bool isVertical)
        {
            horizontalBottom = isHorizontal;
            verticalRight = isVertical;
        }
        public bool horizontalBottom;
        public bool verticalRight;
    }
}