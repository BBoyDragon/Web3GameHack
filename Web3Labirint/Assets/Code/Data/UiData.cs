using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Code.Menu;


[CreateAssetMenu(fileName = "UiData", menuName = "Data/UiData")]
public class UiData : ScriptableObject
{
    [SerializeField]
    private GameObject _menuView;

    public UIBehaviour MenuView { get => _menuView.GetComponent<UIBehaviour>();}
}
