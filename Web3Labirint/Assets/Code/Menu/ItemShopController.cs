using UnityEngine;
using UnityEngine.Networking;

namespace Code.Menu
{

    public class ItemShopController 
    {
        private readonly ItemShopView _view;
        
        public ItemShopController(ItemShopView view /* here are all params that will be needed in purchase AND IMAGE */)
        {
            _view = view;
            _view.Init();
            

            NFT.Asset[] assets = NFT.AssetsRequester.GetAllGameAssets();
            NFT.Asset asset = assets[2];

            string url = asset.image;
            using (WWW www = new WWW(url))
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
    
        public void Purchase()
        {
            Debug.Log("Purchase");
        }
    }
}
