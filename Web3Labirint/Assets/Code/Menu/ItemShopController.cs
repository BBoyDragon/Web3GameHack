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
            
            string url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQLFgrx_H5K9hkWU-sZLKFosqfKwLVKbBwbnnTZmbA9Lnau5XuUdutBrDcq4UxJVwGTcF0&usqp=CAU";
            Texture2D texture = _view.Image.canvasRenderer.GetMaterial().mainTexture as Texture2D;

            using (WWW www = new WWW(url))
            {
                while (!www.isDone) { }
                Debug.Log("Done!");
                www.LoadImageIntoTexture(texture);
                www.Dispose();
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
