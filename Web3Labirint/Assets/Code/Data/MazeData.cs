using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Maze", menuName = "Data/MazeData")]
public class MazeData : ScriptableObject
{
    [SerializeField]
    private GameObject _mazeCellPrefab;
    public GameObject MazeCellPrefab { get => _mazeCellPrefab; }
    
    [SerializeField]
    private MazeView _view;
    public MazeView View { get => _view; }

    [SerializeField]
    private float _cellSize = 1.0f;
    public float CellSize { get => _cellSize; }

}
