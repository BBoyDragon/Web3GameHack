using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

namespace Code.Menu
{
    public class ItemShopController 
    {
        private const string ON_SALE = "ON_SALE";
        private readonly ItemShopView _view;
        private NFT.Asset _asset;

        private PlayerController _playerController;
        private MenuController _menuController;

        public string ButtonText { get => _view.Button.GetComponentInChildren<TMP_Text>().text; set => _view.Button.GetComponentInChildren<TMP_Text>().text = value; }

        public ItemShopController(MenuController menuController, ItemShopView view, NFT.Asset asset, bool canBeDisabled, PlayerController playerController)
        {
            _view = view;
            _asset = asset;
            _playerController = playerController;
            _menuController = menuController;
            _view.Init();
            
            _view.LoadImage(asset.image);

            if (canBeDisabled && asset.market.status != ON_SALE)
            {
                _view.Button.interactable = false;
            }
            else
            {
                _view.OnButtonClick += Clicked;
            }
        }
    
        public void CleanUp()
        {
            _view.CleanUp();
            _view.OnButtonClick -= Clicked;
        }
        
        public void Destroy()
        {
            CleanUp();
            if (_view != null)
            {
                GameObject.Destroy(_view.gameObject);
            }
        }

        public void Clicked()
        {
            if (ButtonText == "Buy")
            {
                Debug.Log("Purchase");           
                string userWallet =PlayerPrefs.GetString("Wallet");  
                Debug.Log("userWallet: " + userWallet); 
                _menuController.BuyAsset(_asset.address, 1, userWallet, _asset.market.seller.address);
            }
            else 
            {
                Debug.Log("Equip");
                var attributes = _asset.properties.GetAttributes();
                var bundleUrl = attributes[0].value;
                var assetName = attributes[1].value;
                _view.LoadAssetFromBundle(bundleUrl, assetName, _playerController);
            }
        }
    }
}
