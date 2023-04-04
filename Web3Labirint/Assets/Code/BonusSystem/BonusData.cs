using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BonusData", menuName = "Data/BonusData")]
public class BonusData : ScriptableObject
{
    [SerializeField]
    private BonusView _view;
    public BonusView View { get => _view;}

    [SerializeField]
    private int _amount;
    public int Amount { get => _amount;}
}
