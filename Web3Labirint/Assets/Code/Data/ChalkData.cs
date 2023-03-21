using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ChalkData", menuName = "Data/ChalkData")]
public class ChalkData : ScriptableObject
{
    [SerializeField]
    private float _diePosition;
    public float DiePosition { get => _diePosition;}

    [SerializeField]
    private float _maxAmountOfChalk;
    public float MaxAmountOfChalk { get => _maxAmountOfChalk;}

    [SerializeField]
    private float _chalkPerSecond;
    public float ChalkPerSecond { get => _chalkPerSecond;}

    [SerializeField]
    private ChalkView View;
    public ChalkView View1 { get => View;}


    [SerializeField]
    private GameObject _brush;
    public GameObject Brush { get => _brush; }
}
