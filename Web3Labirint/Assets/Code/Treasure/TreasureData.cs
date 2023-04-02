using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TreasureData", menuName = "Data/TreasureData")]
public class TreasureData : ScriptableObject
{
    [SerializeField]
    private TreasureView _view;

    public TreasureView View { get => _view; }
}
