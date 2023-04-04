using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Threading.Tasks;

namespace NFT
{
    public class AssetsRequester
    {
        private static readonly string authToken = "jrN8UPnrck:0L3l7yt4odamcsX2XNvP";

        private static readonly Requseter requseter;
        static AssetsRequester()
        {
            requseter = new();
            requseter.SetDefaultHeaders(new Header[] { new("X-Auth-Tonplay", authToken) });
        }
        
        // Get all assets related to the game 
        // curl -X GET https://external.api.tonplay.io/x/tondata/v1/assets/game -H 'X-Auth-Tonplay: jrN8UPnrck:0L3l7yt4odamcsX2XNvP'

        private static readonly string allGameAssetsUrl = "https://external.api.tonplay.io/x/tondata/v1/assets/game";
        public static Asset[] GetAllGameAssets()
        {
            string responseStr = requseter.Get(allGameAssetsUrl);
            return JsonUtility.FromJson<Content>(responseStr).content;
        }

        // Get assets belonging to users' account
        // curl -X GET https://external.api.tonplay.io/x/tondata/v2/assets/EQBwJbd6smxdoSeGQPqCyVbnqglAaHqgK3xST1HpVzfBYfgS -H 'X-Auth-Tonplay: jrN8UPnrck:0L3l7yt4odamcsX2XNvP'
        
        private static readonly string allUsetAssetsUrl = "https://external.api.tonplay.io/x/tondata/v2/assets/";
        public static Asset[] GetAllUserAssets(string usersWallet)
        {
            string responseStr = requseter.Get(allUsetAssetsUrl + usersWallet);
            return JsonUtility.FromJson<Content>(responseStr).content;
        }

        // Get all items on sale
        // curl -X GET https://external.api.tonplay.io/x/market/v1/game/jrN8UPnrck -H 'X-Auth-Tonplay: jrN8UPnrck:0L3l7yt4odamcsX2XNvP'
        
        private static readonly string allAssetsOnSaleUrl = "https://external.api.tonplay.io/x/market/v1/game/jrN8UPnrck";
        public static Asset[] GetAllAssetsOnSale()
        {
            string responseStr = requseter.Get(allAssetsOnSaleUrl);
            try
            {
                return JsonUtility.FromJson<Content>(responseStr).content;
            } 
            catch (Exception)
            {
                return new Asset[0];
            }
        }

        // Buy asset
        // curl -X POST https://external.api.tonplay.io/x/market/v1/sale -H 'X-Auth-Tonplay: jrN8UPnrck:0L3l7yt4odamcsX2XNvP' -H 'Content-Type: application/json' --data '{ "address": "EQDQ0P9W58y8qRVGxMUsi1mN8aL85G6jTIB5aAaTXhCgZice", "amount": 1, "type": "SFT", "buyerAddress": "EQBwJbd6smxdoSeGQPqCyVbnqglAaHqgK3xST1HpVzfBYfgS", "sellerAddress": "EQBwJbd6smxdoSeGQPqCyVbnqglAaHqgK3xST1HpVzfBYfgS" }'

        private static readonly string assetSaleUrl = "https://external.api.tonplay.io/x/market/v1/sale";
        public static string BuyAsset(string assetAddress, long amount, string buyerAddress, string sellerAddress)
        {
            string responseStr = requseter.Post(assetSaleUrl, JsonUtility.ToJson(new BuyRequest(assetAddress, amount, "SFT", buyerAddress, sellerAddress)));
            return JsonUtility.FromJson<Confirmation>(responseStr).url;
        }
    }

    [Serializable]
    class Confirmation
    {
        public string address;
        public string url;
    }

    [Serializable]
    class BuyRequest
    {
        public string address;
        public long amount;
        public string type;
        public string buyerAddress;
        public string sellerAddress;

        public BuyRequest(string address, long amount, string type, string buyerAddress, string sellerAddress)
        {
            this.address = address;
            this.amount = amount;
            this.type = type;
            this.buyerAddress = buyerAddress;
            this.sellerAddress = sellerAddress;
        }
    }

    [Serializable]
    class Content
    {
        public Asset[] content;
    }
    
    [Serializable]
    public class Asset
    {
        public string address;
        public string name;
        public string description;
        public string image;
        public long quantity;
        public string rarity;
        public string category;
        public Market market;
        public Properties properties;

        [Serializable]
        public class Market
        {
            public bool isOwner;
            public long price;
            public override string ToString()
            {
                return JsonUtility.ToJson(this);
            }
        }

        [Serializable]
        public class Properties
        {
            public string owner;
            public string attributes;

            // NFT.Asset[] assets = NFT.AssetsRequester.GetAllGameAssets();
            // NFT.Asset.Attribute[] attributes = assets[0].properties.GetAttributes();

            public Attribute[] GetAttributes()
            {
                return JsonUtility.FromJson<Attributes>("{ \"attributes\": " + attributes + "}").attributes;
            }

            public override string ToString()
            {
                return JsonUtility.ToJson(this);
            }
        }
        
        [Serializable]
        public class Attributes
        {
            public Attribute[] attributes;
        }
        
        [Serializable]
        public class Attribute
        {
            public string trait_type;
            public string value;
            public override string ToString()
            {
                return JsonUtility.ToJson(this);
            }
        }

        public override string ToString()
        {
            return JsonUtility.ToJson(this);
        }
    }
}