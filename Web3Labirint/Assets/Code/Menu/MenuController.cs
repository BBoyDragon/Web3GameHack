using Code.Leaderboard;
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
        private ItemLeaderboardController[] _leaderboardItems;
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
            _view.OnOpenShopButtonClick += OpenShop;
            _view.OnOpenLeaderboardButtonClick += OpenLeaderboard;
            _view.OnExitShopButtonClick += ExitShop;
            _view.OnExitLeaderboardButtonClick += ExitLeaderboard;
            _view.OnGameStarted += StartGame;
        }

        public void Cleanup()
        {
            _view.CleanUp();
            _view.OnStartButtonClick -= IncreaseSize;
            _view.OnStartButtonClick -= IncreaseTransparency;
            _view.OnOpenShopButtonClick -= OpenShop;
            _view.OnOpenLeaderboardButtonClick -= OpenLeaderboard;
            _view.OnExitShopButtonClick -= ExitShop;
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

        private void OpenLeaderboard()
        {
            _view.StartCoroutine(LoadLeaderboard());
        }
        
        private static readonly string addWalletUrl = "https://tonapi.io/login?app=085f941afd4dedddda03cc8708b17c3db6fd9acedf2593117debbc71019a047a353632&callback_url=https://web.api.tonplay.io/auth/v1/tonkeeper?uid=";
        private void OpenShop()
        {
            if (PlayerPrefs.GetString("Wallet") == "")
            {
                var popUpView = Object.Instantiate(_data.PopUp);
                popUpView.Text = "Please link your Ton wallet";
                popUpView.ButtonText = "Link";
                var popUpController = new PopUpController(popUpView, addWalletUrl + PlayerPrefs.GetString("Sub"));
            }
            else
            {
                _view.StartCoroutine(LoadAllGameAssetsAndFill());
            }
        }

        private static readonly string authToken = "jrN8UPnrck:0L3l7yt4odamcsX2XNvP";
        private static void SetAuthHeader(UnityWebRequest www)
        {
            www.SetRequestHeader("X-Auth-Tonplay", authToken);
        }
        
        private static readonly string leaderboardUrl = "https://ismaxis.ru/api/leaderboard/scores";
        public IEnumerator LoadLeaderboard()
        {
            UnityWebRequest www = UnityWebRequest.Get(leaderboardUrl);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                var users = JsonUtility.FromJson<Users>(www.downloadHandler.text);
                var scores = users.scores;
                Array.Sort(scores, Comparer<User>.Create((u1, u2) => 
                {
                    return u1.score.CompareTo(u2.score);
                }));
                FillLeaderboard(scores);
            }
        }

        private void FillLeaderboard(User[] users)
        {
            var leaderboardItemTransform = _data.LeaderboardItem.gameObject.GetComponent<RectTransform>();
            var leaderboardItemHeight = leaderboardItemTransform.rect.height;
            var itemContainerTransform = _view.LeaderboardItemsContainer.GetComponent<RectTransform>();
            itemContainerTransform.sizeDelta = new Vector2(0, users.Length * leaderboardItemHeight + 100);
            _leaderboardItems = new ItemLeaderboardController[users.Length];
            var startCoord = -leaderboardItemHeight * (users.Length - 0.5f) - 50;
            _view.LeaderboardMenu.SetActive(true);
            for (var i = 0; i < users.Length; i++)
            {
                var view = Object.Instantiate(_data.LeaderboardItem, _view.LeaderboardItemsContainer.transform);
                view.GetComponent<RectTransform>().localPosition = new Vector3(itemContainerTransform.rect.width / 2, startCoord + leaderboardItemHeight * i, 0);
                _leaderboardItems[i] = new ItemLeaderboardController(view, (users.Length - i).ToString(), users[i].score.ToString(), users[i].username);
            }
        }

        private static readonly string allUserAssetsUrl = "https://external.api.tonplay.io/x/tondata/v2/assets/";
        public IEnumerator LoadAllUserAssetsAndFill()
        {
            string userWallet = PlayerPrefs.GetString("Wallet");
            UnityWebRequest www = UnityWebRequest.Get(allUserAssetsUrl + userWallet);
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

        private void ExitShop()
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
        private void ExitLeaderboard()
        {
            if (_leaderboardItems != null) 
            { 
                foreach (var leaderboardItem in _leaderboardItems)
                {
                    leaderboardItem.Destroy();
                }
            }
            _view.LeaderboardMenu.SetActive(false);
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
