using System;
using System.Collections.Generic;
using UnionFind;
using UnityEngine;

public class MazeGenerator
{
    public static Maze KruskalGenerate(int sizeX, int sizeY)
    {

        UnionFind.DisjointSet<Cell6> set = new();
        List<Cell6> unfilled = new();

        for (int x = 0; x <= sizeX; x++)
        {
            for (int y = 0; y <= sizeY; y++)
            {
                Cell6 hor = new(true, new(x, y));
                Cell6 ver = new(false, new(x, y));
                set.MakeSet(hor);
                set.MakeSet(ver);
                unfilled.Add(hor);
                unfilled.Add(ver);
            }
        }

        var maze = new Maze(sizeX, sizeY);
        maze.CreateBorder();

        var topLeftHor = set.GetData(new(true, new(0, 0)));
        topLeftHor.isFilled = true;
        {
            var bottomLeftHor = set.GetData(new(true, new(0, sizeY)));
            bottomLeftHor.isFilled = true;
            for (int x = 1; x <= sizeX; x++)
            {
                Cell6 topBorderCell = set.GetData(new(true, new(x, 0)));
                topBorderCell.isFilled = true;
                Cell6 bottomBorderCell = set.GetData(new(true, new(x, sizeY)));
                bottomBorderCell.isFilled = true;
                set.Union(topLeftHor, topBorderCell);
                set.Union(bottomLeftHor, bottomBorderCell);
            }
            set.Union(topLeftHor, bottomLeftHor);
        }
        var topLeftVert= set.GetData(new(false, new(0, 0)));
        topLeftVert.isFilled = true;
        {
            var topRightVert = set.GetData(new(false, new(sizeX, 0)));
            topRightVert.isFilled = true;
            for (int y = 1; y <= sizeY; y++)
            {
                Cell6 leftBorderCell = set.GetData(new(false, new(0, y)));
                leftBorderCell.isFilled = true;
                Cell6 rightBorderCell = set.GetData(new(false, new(sizeX, y)));
                rightBorderCell.isFilled = true;

                set.Union(topLeftVert, leftBorderCell);
                set.Union(topRightVert, rightBorderCell);
            }
            set.Union(topLeftVert, topRightVert);
        }

        set.Union(topLeftHor, topLeftVert);

        var rand = new System.Random(228);
        var targetCount = unfilled.Count / 2; 
        while(unfilled.Count != targetCount)
        {
            Cell6 cell = unfilled[rand.Next(unfilled.Count)];
            if(!HasNeighboursFromSameSets(set, cell))
            {
                List<Cell6> neighboursSets = GetBeighboursSet(set, cell);
                foreach (var neighbourSet in neighboursSets)
                {
                    set.Union(cell, neighbourSet);
                }
                cell.isFilled = true;
                if (cell.isHor)
                {
                    maze.SetHorizontalWall(cell.coords.x, cell.coords.y, true);
                }
                else
                {
                    maze.SetVerticalWall(cell.coords.x, cell.coords.y, true);
                }
            } 
            unfilled.Remove(cell);
            // MazeLoader.SaveToFile("Assets/Code/Tests/KMaze/" + "KruskalGeneratedMaze_" + unfilled.Count + ".txt", maze);
        }

        return maze;
    }

    private static bool HasNeighboursFromSameSets(DisjointSet<Cell6> set, Cell6 cell)
    {
        Cell6[] topLeftNeighbours = cell.GetTopLeftFilledNeighbours();
        Cell6[] bottomRightNeighbours = cell.GetBottomRightNeighbours();
        foreach (var oneSide in topLeftNeighbours)
        {
            if (!set.ContainsData(oneSide))
            {
                continue;
            }

            Cell6 oneSideSet = set.FindSet(oneSide);
            if (!oneSideSet.isFilled)
            {
                continue;
            }

            foreach (var otherSide in bottomRightNeighbours)
            {
                if (!set.ContainsData(otherSide))
                {
                    continue;
                }

                Cell6 otherSideSet = set.FindSet(otherSide);
                if (!otherSideSet.isFilled)
                {
                    continue;
                }
                
                if (oneSideSet.Equals(otherSideSet))
                {
                    return true;
                }
            }
        }
        
        return false;
}
    
    private static List<Cell6> GetBeighboursSet(DisjointSet<Cell6> set, Cell6 cell)
    {
        Cell6[] topLeftNeighbours = cell.GetTopLeftFilledNeighbours();
        Cell6[] bottomRightNeighbours = cell.GetBottomRightNeighbours();
        Cell6[] neighbours = new Cell6[topLeftNeighbours.Length + bottomRightNeighbours.Length];
        Array.Copy(topLeftNeighbours, 0, neighbours, 0, topLeftNeighbours.Length);
        Array.Copy(bottomRightNeighbours, 0, neighbours, topLeftNeighbours.Length, bottomRightNeighbours.Length);
        List<Cell6> neighborSets = new(2);
        foreach (var neighbour in neighbours)
        {
            if (!set.ContainsData(neighbour))
            {
                continue;
            }

            Cell6 neighbourSet = set.FindSet(neighbour);
            if (neighbourSet.isFilled)
            {
                neighborSets.Add(neighbourSet);
            }
        }

        return neighborSets;
    }

    public class Cell6 : IComparable<Cell6>
    {
        public Cell6(bool isHor, Vector2Int coords)
        {
            this.isHor = isHor;
            this.coords = coords;
            this.isFilled = false;
        }

        public Cell6[] GetTopLeftFilledNeighbours()
        {
            Cell6[] topLeftNeighbours = new Cell6[3];

            if (isHor)
            {
                topLeftNeighbours[0] = new(false, coords + new Vector2Int(-1, 1));
                topLeftNeighbours[1] = new(true, coords + new Vector2Int(-1, 0));
                topLeftNeighbours[2] = new(false, coords + new Vector2Int(1, 1));
            } 
            else
            {
                topLeftNeighbours[0] = new(true, coords + new Vector2Int(0, -1));
                topLeftNeighbours[1] = new(false, coords + new Vector2Int(0, -1));
                topLeftNeighbours[2] = new(true, coords + new Vector2Int(1, -1));
            }

            return topLeftNeighbours;
}

        public Cell6[] GetBottomRightNeighbours()
        {
            Cell6[] bottomRightNeighbours = new Cell6[3];

            if (isHor)
            {
                bottomRightNeighbours[0] = new(false, coords + new Vector2Int(0, 0));
                bottomRightNeighbours[1] = new(true, coords + new Vector2Int(1, 0));
                bottomRightNeighbours[2] = new(false, coords + new Vector2Int(0, 1));
            }
            else
            {
                bottomRightNeighbours[0] = new(true, coords + new Vector2Int(1, 0));
                bottomRightNeighbours[1] = new(false, coords + new Vector2Int(0, 1));
                bottomRightNeighbours[2] = new(true, coords + new Vector2Int(0, 0));
            }

            return bottomRightNeighbours;
        }

        public int CompareTo(Cell6 that)
        {
            if (this.coords.x == that.coords.x)
            {
                if (this.coords.y == that.coords.y)
                {
                    if (this.isHor == that.isHor)
                    {
                        return 0;
                    }
                    else
                    {
                        return isHor ? 1 : -1;
                    }
                }
                else
                {
                    return this.coords.y > that.coords.y ? 1 : -1;
                }
            } 
            else
            {
                return this.coords.x > that.coords.x ? 1 : -1;
            }   
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(Cell6))
            {
                return false;
            }

            Cell6 that = (Cell6) obj;

            return this.coords.Equals(that.coords) && this.isHor == that.isHor;
        }

        public override int GetHashCode()
        {
            return (coords.GetHashCode() * 31 + isHor.GetHashCode()) * 31;
        }

        public bool isFilled;
        public bool isHor;
        public Vector2Int coords;
    }


    // Wilson's algorithm
    public static Maze Generate(int sizeX, int sizeY)
    {
        var maze = new Maze(sizeX, sizeY);
        maze.SetAllWalls();

        Cell[,] cells = new Cell[sizeX, sizeY];

        var rand = new System.Random();
        int startX = rand.Next(sizeX);
        int startY = rand.Next(sizeY);

        cells[startX, startY].isVisited = true;

        int cellsLeft = sizeX * sizeY - 1;
        while(cellsLeft > 0)
        {
            Vector2Int startCell = new (rand.Next(sizeX), rand.Next(sizeY));

            // Walk
            Vector2Int curCell = new (startCell.x, startCell.y);
            while(!cells[curCell.x, curCell.y].isVisited)
            {
                Direction dir = DirectionMetohods.GenerateRandomDirection(rand);
                Vector2Int nextCell = DirectionMetohods.ApplyDirection(curCell, dir);
                if (CellExists(nextCell, sizeX, sizeY))
                {
                    cells[curCell.x, curCell.y].child = dir;
                    curCell = nextCell;
                }
            }

            // Cut walk
            while (startCell.x != curCell.x || startCell.y != curCell.y)
            {
                Direction dir = cells[startCell.x, startCell.y].child;
                maze.SetWallBetween(startCell.x, startCell.y, dir, false);
                cells[startCell.x, startCell.y].isVisited = true;
                startCell = DirectionMetohods.ApplyDirection(startCell, dir);
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

    private static bool CellExists(Vector2Int cell, int sizeX, int sizeY)
    {
        return 0 <= cell.x && cell.x < sizeX && 0 <= cell.y && cell.y < sizeY;
    }
}
