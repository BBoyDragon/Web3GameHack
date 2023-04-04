using UnityEngine;
using UnityEngine.Networking;

namespace Code.Menu
{

    public class ItemShopController 
    {
        private readonly ItemShopView _view;
        
        public ItemShopController(ItemShopView view, string url)
        {
            _view = view;
            _view.Init();
            
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
