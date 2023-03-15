using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MetaData", menuName = "Data/MetaData")]
public class MetaData : ScriptableObject
{
    [SerializeField]
    private PlayerData _playerData;
    public PlayerData PlayerData { get => _playerData; }
    
    [SerializeField]
    private UiData _uiData;
    public UiData UiData { get => _uiData;}
    
    [SerializeField]
    private MazeData _mazeData;
    public MazeData MazeData { get => _mazeData; }
}
