using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class MazeTest
{
    private static string _prefix = "Assets/Code/Tests/Generated/";
    private static System.Random _random = new();
    private static int _maxSizeX = 100;
    private static int _maxSizeY = 100;

    private int _sizeX = _random.Next(_maxSizeX);
    private int _sizeY = _random.Next(_maxSizeY);

    [Test]
    public void CreateBorderTest()
    {
        var maze = new Maze(_sizeX, _sizeY);
        maze.CreateBorder();
        CheckBorders(maze);
    }

    private static void CheckBorders(Maze maze)
    {
        int sizeX = maze.SizeX();
        int sizeY = maze.SizeY();
        for (int x = 0; x <= sizeX; x++)
        {
            Assert.AreEqual(true, maze.IsHorizontalWall(x, 0));
            Assert.AreEqual(true, maze.IsHorizontalWall(x, sizeY));
        }
        for (int y = 0; y <= sizeY; y++)
        {
            Assert.AreEqual(true, maze.IsVerticalWall(0, y));
            Assert.AreEqual(true, maze.IsVerticalWall(sizeX, y));
        }
    }

    [Test]
    public void SaveToFileTest()
    {
        var maze = new Maze(_sizeX, _sizeY);
        maze.CreateBorder();
        CheckBorders(maze);
        MazeLoader.SaveToFile(_prefix + "mazeWithBorders.txt", maze);
    }

    [Test]
    public void LoadFromFileTest()
    {
        var maze = MazeLoader.LoadFromFile(_prefix + "mazeWithBorders.txt");
        CheckBorders(maze);
    }
    
    [Test]
    public void SetWallBettweenTest()
    {
        const int iterations = 10;
        var maze = new Maze(_sizeX, _sizeY);

        for (int i = 0; i < iterations; i++)
        {
            int x = _random.Next(_sizeX);
            int y = _random.Next(_sizeY);
            Direction dir = DirectionMetohods.GenerateRandomDirection(_random);

            Func<int, int, bool> func;
            
            if (dir == Direction.Top )
            {
                func = (int x, int y) => maze.IsHorizontalWall(x + 1, y);
            }
            else if (dir == Direction.Bottom)
            {
                func = (int x, int y) => maze.IsHorizontalWall(x + 1, y + 1);
            }
            else if (dir == Direction.Left)
            {
                func = (int x, int y) => maze.IsVerticalWall(x, y + 1);
            }
            else if (dir == Direction.Right)
            {
                func = (int x, int y) => maze.IsVerticalWall(x + 1, y + 1);
            } 
            else
            {
                Assert.Fail("Unknown direction found :'" + dir.ToString() + "'");
                return;
            }

            bool exists = func(x, y);
            maze.SetWallBetween(x, y, dir, !exists);
            Assert.AreNotEqual(exists, func(x, y));
        }
    }

    [Test]
    public void GenerateTest()
    {
        var maze = MazeGenerator.Generate(_sizeX, _sizeY);
        CheckBorders(maze);
    }
}
