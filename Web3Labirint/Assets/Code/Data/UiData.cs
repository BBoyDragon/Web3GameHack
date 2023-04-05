using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Code.Menu;


[CreateAssetMenu(fileName = "UiData", menuName = "Data/UiData")]
public class UiData : ScriptableObject
{
    [SerializeField]
    private GameObject _menuView;
    [SerializeField]
    private GameObject _shopItem;
    [SerializeField] 
    private GameObject _popUp;
    

    public UIBehaviour MenuView => _menuView.GetComponent<UIBehaviour>();
    public ItemShopView ShopItem => _shopItem.GetComponent<ItemShopView>();

    public PopUpView PopUp => _popUp.GetComponent<PopUpView>();
}
