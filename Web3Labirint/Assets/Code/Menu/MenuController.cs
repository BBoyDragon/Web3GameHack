using NFT;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Object = UnityEngine.Object;

namespace Code.Menu
{
    public class MenuController:ICleanup
    {
        private static readonly int Transparency = Animator.StringToHash("Transparency");
        private readonly UiData _data;
        private readonly UIBehaviour _view;
        private ItemShopController[] _shopItems;
        private PlayerController _playerController;

        public event Action OnStartGame;
        public MenuController(UiData data, PlayerController playerController)
        {
            _playerController = playerController;
            _data = data;
            _view = Object.Instantiate(_data.MenuView);
            _view.Init();
            _view.OnStartButtonClick += IncreaseSize;
            _view.OnStartButtonClick += IncreaseTransparency;
            _view.OnShopButtonClick += OpenShop;
            _view.OnExitButtonClick += OnExit;
            _view.OnGameStarted += StartGame;
        }

        public void Cleanup()
        {
            _view.CleanUp();
            _view.OnStartButtonClick -= IncreaseSize;
            _view.OnStartButtonClick -= IncreaseTransparency;
            _view.OnShopButtonClick -= OpenShop;
            _view.OnExitButtonClick -= OnExit;
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
            _view.StartCoroutine(LoadAllGameAssetsAndFill());
        }

        private static readonly string authToken = "jrN8UPnrck:0L3l7yt4odamcsX2XNvP";
        private static void SetAuthHeader(UnityWebRequest www)
        {
            www.SetRequestHeader("X-Auth-Tonplay", authToken);
        }

        private static readonly string allGameAssetsUrl = "https://external.api.tonplay.io/x/tondata/v1/assets/game";
        public IEnumerator LoadAllGameAssetsAndFill()
        {
            UnityWebRequest www = UnityWebRequest.Get(allGameAssetsUrl);
            SetAuthHeader(www);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                FillShop(JsonUtility.FromJson<Content>(www.downloadHandler.text).content);
            }
        }


        private void FillShop(Asset[] assets)
        {
            var shopItemTransform = _data.ShopItem.gameObject.GetComponent<RectTransform>();
            float shopItemHeight = shopItemTransform.rect.height;
            var itemContainerTransform = _view.ShopItemsContainer.GetComponent<RectTransform>();
            itemContainerTransform.sizeDelta = new Vector2(0, assets.Length * shopItemHeight + 100);
            _shopItems = new ItemShopController[assets.Length];
            float startCoord = -shopItemHeight * (assets.Length - 0.5f) - 50;
            _view.ShopMenu.SetActive(true);
            for (int i = 0; i < assets.Length; i++)
            {
                var view = Object.Instantiate(_data.ShopItem, _view.ShopItemsContainer.transform);
                view.GetComponent<RectTransform>().localPosition = new Vector3(itemContainerTransform.rect.width / 2, startCoord + shopItemHeight * i, 0);
                _shopItems[i] = new ItemShopController(this, view, assets[i], _data, _playerController);
            }
        }

        public void BuyAsset(string assetAddress, long amount, string buyerAddress, string sellerAddress)
        {
            _view.StartCoroutine(GetLinkAndCreatePopUp(assetAddress, amount, buyerAddress, sellerAddress));
        }

        private static readonly string assetSaleUrl = "https://external.api.tonplay.io/x/market/v1/sale";
        public IEnumerator GetLinkAndCreatePopUp (string assetAddress, long amount, string buyerAddress, string sellerAddress)
        {
            string json = JsonUtility.ToJson(new BuyRequest(assetAddress, amount, "SFT", buyerAddress, sellerAddress));
            var www = new UnityWebRequest(assetSaleUrl, "POST");
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
            www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");
            SetAuthHeader(www);
            yield return www.SendWebRequest();
            string responseStr = www.downloadHandler.text;
            var confirmation = JsonUtility.FromJson<Confirmation>(responseStr);
            string url = confirmation.url;
            var popUpView = Object.Instantiate(_data.PopUp);
            var popUpController = new PopUpController(popUpView, url);
        }

        private void OnExit()
        {
            if (_shopItems != null) 
            { 
                foreach (var shopItem in _shopItems)
                {
                    shopItem.Destroy();
                }
            }
            _view.ShopMenu.SetActive(false);
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
