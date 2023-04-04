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
            // here will be all requests to web
            
            _shopItems = new ItemShopController[1];
            var view = Object.Instantiate(_data.ShopItem, _view.ShopItemsContainer.transform);
            _shopItems[0] = new ItemShopController(view /* here are all params that will be needed in purchase */ );

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
