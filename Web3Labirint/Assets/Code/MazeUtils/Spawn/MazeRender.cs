using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MazeUtils.Spawn
{
    public class MazeRender : MonoBehaviour
    {
        [SerializeField] GameObject mazeCellPrefab;
        IMazeGenerator mazeGenerator = new Generators.KruskalsGenerator();

        public float CellSize = 1.0f;

        private void Start()
        {
            IMaze maze = mazeGenerator.Generate(20, 20);

            for (int x = 0; x < maze.SizeX(); x++)
            {
                for (int y = 0; y < maze.SizeY(); y++)
                {
                    GameObject newCell = Instantiate(mazeCellPrefab, new Vector3(x * CellSize, .0f, (maze.SizeY() - y) * CellSize), Quaternion.identity, transform);
                    MazeCellObject mazeCell = newCell.GetComponent<MazeCellObject>();

                    bool top = y == 0;
                    bool bottom = maze.IsHorizontalWall(x + 1, y + 1);
                    bool left = x == 0;
                    bool right = maze.IsVerticalWall(x + 1, y + 1);

                    mazeCell.Init(top, bottom, left, right);
                }
            }
        }
    }
}
