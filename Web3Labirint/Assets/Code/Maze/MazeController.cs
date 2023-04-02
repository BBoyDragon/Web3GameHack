using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MazeUtils.Spawn
{
    public class MazeController
    {
        private Generators.IMazeGenerator _mazeGenerator;
        private MazeView _mazeView;
        private MazeData _mazeData;

        public MazeController(MazeData data)
        {
            _mazeGenerator = new Generators.KruskalsGenerator();
            _mazeData = data;
            _mazeView = GameObject.Instantiate<MazeView>(data.View);

            Generate();
        }

        private void Generate()
        {
            IMaze maze = _mazeGenerator.Generate(_mazeData.MazeSize,_mazeData.MazeSize);

            for (int x = 0; x < maze.SizeX(); x++)
            {
                for (int y = 0; y < maze.SizeY(); y++)
                {
                    GameObject newCell = GameObject.Instantiate(_mazeData.MazeCellPrefab, new Vector3(x * _mazeData.CellSize, .0f, (maze.SizeY() - y) * _mazeData.CellSize), Quaternion.identity, _mazeView.transform);
                    MazeCellObject mazeCell = newCell.GetComponent<MazeCellObject>();

                    bool top = y == 0;
                    bool bottom = maze.IsHorizontalWall(x + 1, y + 1);
                    bool left = x == 0;
                    bool right = maze.IsVerticalWall(x + 1, y + 1);

                    mazeCell.Init(top, bottom, left, right, _mazeData.CellSize);
                }
            }
        }
    }
}
