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

    [SerializeField]
    private CameraData _cameraData;
    public CameraData CameraData { get => _cameraData; }
    
    [SerializeField]
    private UserNameData _userNameData;
    public UserNameData UserNameData { get => _userNameData; }

    [SerializeField]
    private TreasureData _treasureData;
    public TreasureData TreasureData { get => _treasureData; }

    [SerializeField]
    private BonusData _bonusData;
    public BonusData BonusData { get => _bonusData;}
}
