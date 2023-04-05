using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
 
namespace NFT 
{
    public class BundleWebLoader
    {
        private static readonly Requseter requseter = new();
        public static AssetBundle LoadBundle(string bundleUrl)
        {
            using (WWW web = new WWW(bundleUrl))
            {
                while (!web.isDone) { }
                AssetBundle remoteAssetBundle = web.assetBundle;
                if (remoteAssetBundle == null)
                {
                    Debug.LogError("Failed to download AssetBundle!");
                }
                return remoteAssetBundle;
            }
        }
        //void Start() {
        //    StartCoroutine(GetAssetBundle());
        //}

        public static IEnumerator GetAssetBundle(string bundleUrl)
        {
            UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(bundleUrl);
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
            }
        }
    }
}