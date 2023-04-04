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
            
            
            // UnityWebRequest request = UnityWebRequestTexture.GetTexture("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQLFgrx_H5K9hkWU-sZLKFosqfKwLVKbBwbnnTZmbA9Lnau5XuUdutBrDcq4UxJVwGTcF0&usqp=CAU");
            // request.SendWebRequest();
            // while (!request.isDone) { }
            //
            // if(request.isNetworkError || request.isHttpError) 
            //     Debug.Log(request.error);
            // else
            //     _view.Image.texture = ((DownloadHandlerTexture) request.downloadHandler).texture;
            
            
            // _view.Image = ???
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
