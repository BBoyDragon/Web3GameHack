using UnityEngine;

namespace MazeUtils.Generators
{
    using WilsonsGeneratorUtils;
    public class WilsonsGenerator : IMazeGenerator
    {
        public IMaze Generate(int sizeX, int sizeY)
        {
            var maze = new Maze(sizeX, sizeY);
            maze.SetAllWalls();

            Cell[,] cells = new Cell[sizeX, sizeY];

            var rand = new System.Random();
            int startX = rand.Next(sizeX);
            int startY = rand.Next(sizeY);

            cells[startX, startY].isVisited = true;

            int cellsLeft = sizeX * sizeY - 1;
            while (cellsLeft > 0)
            {
                Vector2Int startCell = new(rand.Next(sizeX), rand.Next(sizeY));

                Vector2Int curCell = new(startCell.x, startCell.y);
                while (!cells[curCell.x, curCell.y].isVisited)
                {
                    Direction dir = DirectionMetohods.GenerateRandomDirection(rand);
                    Vector2Int nextCell = DirectionMetohods.ApplyDirection(curCell, dir);
                    if (CellExists(nextCell, sizeX, sizeY))
                    {
                        cells[curCell.x, curCell.y].child = dir;
                        curCell = nextCell;
                    }
                }

                while (startCell.x != curCell.x || startCell.y != curCell.y)
                {
                    Direction dir = cells[startCell.x, startCell.y].child;
                    DirectionMetohods.SetWallBetween(maze, startCell.x, startCell.y, dir, false);
                    cells[startCell.x, startCell.y].isVisited = true;
                    startCell = DirectionMetohods.ApplyDirection(startCell, dir);
                    --cellsLeft;
                }
            }

            return maze;
        }

        private static bool CellExists(Vector2Int cell, int sizeX, int sizeY)
        {
            return 0 <= cell.x && cell.x < sizeX && 0 <= cell.y && cell.y < sizeY;
        }

        private struct Cell
        {
            public bool isVisited;
            public Direction child;
        }
    }
}
