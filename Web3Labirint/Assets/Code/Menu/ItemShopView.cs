using System;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Menu
{
    public class ItemShopView : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private Button buyButton;
        [SerializeField] private Button equipButton;

        public Image Image => image;

        public Button BuyButton => buyButton;
        public Button EquipButton => equipButton;

        public event Action OnBuyButtonClick;
        public event Action OnEquipButtonClick;
        
        public void Init()
        {
            buyButton.onClick.AddListener(OnBuy);
            equipButton.onClick.AddListener(OnEquip);
        }
        
        public void CleanUp()
        {
            buyButton.onClick.RemoveAllListeners();
        }

        private void OnBuy()
        {
            OnBuyButtonClick?.Invoke();
        }
        private void OnEquip()
        {
            OnEquipButtonClick?.Invoke();
        }
    }
}
