using System;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Menu
{
    public class ItemShopView : MonoBehaviour
    {
        [SerializeField] private Button buyButton;
        [SerializeField] private RawImage image;

        public RawImage Image => image;
        
        public event Action OnBuyButtonClick;
        
        public void Init()
        {
            buyButton.onClick.AddListener(OnBuy);
        }
        
        public void CleanUp()
        {
            buyButton.onClick.RemoveAllListeners();
        }

        private void OnBuy()
        {
            OnBuyButtonClick?.Invoke();
        }
    }
}
