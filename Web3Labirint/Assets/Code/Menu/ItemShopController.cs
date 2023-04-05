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
        private UiData _data;
        private PlayerController _playerController;

        public ItemShopController(ItemShopView view, NFT.Asset asset, UiData data, PlayerController playerController)
        {
            _playerController = playerController;
            _data = data;
            _asset = asset;
            _view = view;
            _view.Init();
            
            _view.LoadImage(asset.image);

            if (asset.market.status == ON_SALE)
            {
                _view.OnBuyButtonClick += Purchase;
            }
            else
            {
                _view.BuyButton.interactable = false;
            }
            
            //if (asset.market.isOwner)
            //{
                _view.OnEquipButtonClick += Equip;
            //}
            //else
            //{
            //    _view.EquipButton.interactable = false;
            //}
        }
    
        public void CleanUp()
        {
            _view.CleanUp();
            _view.OnBuyButtonClick -= Purchase;
            _view.OnEquipButtonClick -= Equip;
        }
        
        public void Destroy()
        {
            CleanUp();
            if (_view != null)
            {
                GameObject.Destroy(_view.gameObject);
            }
        }
    
        private string ourWallet = "EQBwJbd6smxdoSeGQPqCyVbnqglAaHqgK3xST1HpVzfBYfgS";
        public void Purchase()
        {
            Debug.Log("Purchase");
            string userWallet = ourWallet;
            var confirmation = NFT.AssetsRequester.BuyAsset(_asset.address, 1, userWallet, _asset.market.seller.address);
            string url = confirmation.url;
            var popUpView = Object.Instantiate(_data.PopUp);
            var popUpController = new PopUpController(popUpView, url);
        }
        
        public void Equip()
        {
            Debug.Log("Equip");
            var attributes = _asset.properties.GetAttributes();
            var bundleUrl = attributes[0].value;
            var assetName = attributes[1].value;
            _view.LoadAssetFromBundle(bundleUrl, assetName, _playerController);
        }
    }
}
