using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace FazAppFramework.Managers
{
    public class OwnInterstitial : MonoBehaviour
    {
        public bool loadingFinished;

        public OwnInterstitialData data;
        public Sprite interstitialTexture;

        public bool showInterstitial;
        
        public void PrepareInterstitialData()
        {
            data = FirebaseManager.GetOwnInterstitialData();

            if (!data.showOwnInterstitial)
            {
                showInterstitial = false;
                loadingFinished = true;
                return;
            }

            StartCoroutine(LoadImageFromUrl());
        }

        private IEnumerator LoadImageFromUrl()
        {
            Debug.Log("FazApp: Loading image...");

            var www = UnityWebRequestTexture.GetTexture(data.imageURL);
            yield return www.SendWebRequest();

            if (www.isHttpError || www.isNetworkError)
            {
                showInterstitial = false;
                Debug.LogError("FazApp: Image not loaded. \n" + www.error);
            }
            else
            {
                showInterstitial = true;
                var tex = ((DownloadHandlerTexture)www.downloadHandler).texture;
                interstitialTexture = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100f);
                Debug.Log("FazApp: Image loaded");
            }
            
            loadingFinished = true;
        }

        private void Update()
        {
            if (FirebaseManager.OwnIntertitialDataIsReady)
            {
                PrepareInterstitialData();
                enabled = false;
            }

            if (FirebaseManager.OwnInterstitalDataFetchFailed)
            {
                showInterstitial = false;
                enabled = false;
            }
        }
    }
}
