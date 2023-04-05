using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Menu
{
    public class MenuController:ICleanup
    {
        private static readonly int Transparency = Animator.StringToHash("Transparency");
        private readonly UiData _data;
        private readonly UIBehaviour _view;
        private ItemShopController[] _shopItems;

        public event Action OnStartGame;
        public MenuController(UiData data)
        {
            _data = data;
            _view = Object.Instantiate(_data.MenuView);
            _view.Init();
            _view.OnStartButtonClick += IncreaseSize;
            _view.OnStartButtonClick += IncreaseTransparency;
            _view.OnShopButtonClick += OpenShop;
            _view.OnGameStarted += StartGame;
        }

        public void Cleanup()
        {
            _view.CleanUp();
            _view.OnStartButtonClick -= IncreaseSize;
            _view.OnStartButtonClick -= IncreaseTransparency;
            _view.OnShopButtonClick -= OpenShop;
            _view.OnGameStarted -= StartGame;

            if (_shopItems != null) 
            { 
                foreach (var shopItem in _shopItems)
                {
                    shopItem.CleanUp();
                }
            }
        }

        private void IncreaseSize()
        {
            _view.transform.LeanScale(new Vector3(1.5f, 1.5f, 1.5f), 0.1f).setEaseInOutSine();
        }

        private void IncreaseTransparency()
        {
            _view.Animator.SetTrigger(Transparency);
        }
        
        private void OpenShop()
        {
            NFT.Asset[] assets = NFT.AssetsRequester.GetAllGameAssets();
            var shopItemTransform = _data.ShopItem.gameObject.GetComponent<RectTransform>();
            float shopItemHeight = shopItemTransform.rect.height;
            var itemContainerTransform = _view.ShopItemsContainer.GetComponent<RectTransform>();
            itemContainerTransform.sizeDelta = new Vector2(0, assets.Length * shopItemHeight + 100);
            _shopItems = new ItemShopController[assets.Length];
            float startCoord = - shopItemHeight * (assets.Length - 0.5f) - 50;
            for (int i = 0; i < assets.Length; i++)
            {
                var view = Object.Instantiate(_data.ShopItem, _view.ShopItemsContainer.transform);
                view.GetComponent<RectTransform>().localPosition = new Vector3(itemContainerTransform.rect.width / 2, startCoord + shopItemHeight * i, 0);
                _shopItems[i] = new ItemShopController(view, assets[i], _data);
            }
            
            _view.ShopMenu.SetActive(true);
        }

        private void StartGame()
        {
            OnStartGame?.Invoke();
            ToggleSetActive();
        }
        private void ToggleSetActive()
        {
            _view.gameObject.SetActive(!_view.enabled);
        }

    }

}
