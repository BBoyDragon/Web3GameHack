using UnityEngine;
using UnityEngine.Networking;

namespace Code.Menu
{
    public class ItemShopController 
    {
        private readonly ItemShopView _view;
        private NFT.Asset asset;
        public ItemShopController(ItemShopView view, NFT.Asset asset)
        {
            this.asset = asset;
            _view = view;
            _view.Init();
            
            using (WWW www = new WWW(asset.image))
            {
                while (!www.isDone) { }
                Debug.Log("Done!");
                _view.Image.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
            }

            _view.OnBuyButtonClick += Purchase;
        }
    
        public void CleanUp()
        {
            _view.CleanUp();
            _view.OnBuyButtonClick -= Purchase;
        }
    
        private string ourWallet = "EQBwJbd6smxdoSeGQPqCyVbnqglAaHqgK3xST1HpVzfBYfgS";
        public void Purchase()
        {
            string userWallet = ourWallet;
            Debug.Log("Purchase");
            var confirmation = NFT.AssetsRequester.BuyAsset(asset.address, 1, userWallet, asset.market.seller.address);
            string url = confirmation.url;
            Debug.Log(url); // This is confurmation url to get to user
        }
    }
}
