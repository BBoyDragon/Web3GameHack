using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class MazeTest
{
    [Test]
    public void CreateBorderTest()
    {
        int sizeX = 10;
        int sizeY = 20;
        var maze = new Maze(sizeX, sizeY);
        maze.CreateBorder();
        CheckBorders(maze);
    }

    private static void CheckBorders(Maze maze)
    {
        int sizeX = maze.SizeX();
        int sizeY = maze.SizeY();

        for (int x = 0; x < sizeX + 1; x++)
        {
            for (int y = 0; y < sizeY + 1; y++)
            {
                Assert.AreEqual(y == 0 || y == sizeY, maze.IsHorizontalWall(x, y));
                Assert.AreEqual(x == 0 || x == sizeX, maze.IsVerticalWall(x, y));
            }
        }
    }

    private static string _prefix = "Assets/Code/Tests/";

    [Test]
    public void SaveToFileTest()
    {
        int sizeX = 20;
        int sizeY = 10;
        var maze = new Maze(sizeX, sizeY);
        maze.CreateBorder();
        MazeLoader.SaveToFile(_prefix + "mazeWithBorders.txt", maze);
    }

    [Test]
    public void LoadFromFileTest()
    {
        var maze = MazeLoader.LoadFromFile(_prefix + "mazeWithBorders.txt");

        CheckBorders(maze);
    }
    
    [Test]
    public void GenerateTest()
    {
        int sizeX = 10;
        int sizeY = 20;
        var maze = MazeGenerator.Generate(sizeX, sizeY);
        MazeLoader.SaveToFile(_prefix + "generatedMaze.txt", maze);
    }
}
