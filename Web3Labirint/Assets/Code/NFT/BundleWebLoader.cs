using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
 
namespace NFT 
{
    public class BundleWebLoader
    {   
        AssetBundle LoadBundle(string bundleUrl)
        {
            using (WWW web = new WWW(bundleUrl))
            {
                while(!web.isDone) { }
                AssetBundle remoteAssetBundle = web.assetBundle;
                if (remoteAssetBundle == null) {
                    Debug.LogError("Failed to download AssetBundle!");
                }
                return remoteAssetBundle;
                //Instantiate(remoteAssetBundle.LoadAsset(assetName));
                //remoteAssetBundle.Unload(false);
            }
        }
    }
}