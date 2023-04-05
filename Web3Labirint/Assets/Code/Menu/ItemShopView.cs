using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Code.Menu
{
    public class ItemShopView : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private Button buyButton;
        [SerializeField] private Button equipButton;

        public Image Image => image;

        public Button BuyButton => buyButton;
        public Button EquipButton => equipButton;

        public event Action OnBuyButtonClick;
        public event Action OnEquipButtonClick;
        
        public void Init()
        {
            buyButton.onClick.AddListener(OnBuy);
            equipButton.onClick.AddListener(OnEquip);
        }
        
        public void CleanUp()
        {
            buyButton.onClick.RemoveAllListeners();
        }

        private void OnBuy()
        {
            OnBuyButtonClick?.Invoke();
        }
        private void OnEquip()
        {
            OnEquipButtonClick?.Invoke();
        }

        public void LoadAssetFromBundle(string bundleUrl, string assetName, PlayerController playerController)
        {
            StartCoroutine(SetNewViewFromBundle(bundleUrl, assetName, playerController));
        }

        public IEnumerator SetNewViewFromBundle(string bundleUrl, string assetName, PlayerController playerController)
        {
            UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(bundleUrl);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                playerController.ResetView(Instantiate(
                    (DownloadHandlerAssetBundle.GetContent(www).LoadAsset(assetName) 
                    as GameObject).GetComponent<PlayerView>()));
            }
        }
    }
}
