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
        [SerializeField] private Button button;

        public Image Image => image;

        public Button Button => button;

        public event Action OnButtonClick;
        
        public void Init()
        {
            button.onClick.AddListener(OnBuy);
        }
        
        public void CleanUp()
        {
            button.onClick.RemoveAllListeners();
        }

        private void OnBuy()
        {
            OnButtonClick?.Invoke();
        }

        public void LoadAssetFromBundle(string bundleUrl, string assetName, PlayerController playerController)
        {
            StartCoroutine(SetNewViewFromBundle(bundleUrl, assetName, playerController));
        }

        private IEnumerator SetNewViewFromBundle(string bundleUrl, string assetName, PlayerController playerController)
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

        public void LoadImage(string imageUrl)
        {
            StartCoroutine(SetImage(imageUrl));
        }

        IEnumerator SetImage(string imageUrl)
        {
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageUrl);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success) {
                Debug.Log(www.error);
            }
            else {
                Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0));
            }
        }
    }
}
