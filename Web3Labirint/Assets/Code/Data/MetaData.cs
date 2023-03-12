using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MetaData", menuName = "Data/MetaData")]
public class MetaData : ScriptableObject
{
    [SerializeField]
    private PlayerData _playerData;

    public PlayerData PlayerData { get => _playerData; }
}
