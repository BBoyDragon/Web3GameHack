using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "FinishData", menuName = "Data/FinishData")]
public class FinishData : ScriptableObject
{
    [SerializeField]
    private FinishView _winView;
    public FinishView WinView { get => _winView;}

    [SerializeField]
    private FinishView _loseView;
    public FinishView LoseView { get => _loseView;}

}
