using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MetaData", menuName = "Data/MetaData")]
public class MetaData : ScriptableObject
{
    [SerializeField]
    private PlayerData _playerData;
    [SerializeField]
    private UiData _uiData;

    public PlayerData PlayerData { get => _playerData; }
    public UiData UiData { get => _uiData;}
}
