using System;
using System.Collections;
using GoogleMobileAds.Api;
using UnityEngine;

namespace FazAppFramework.Managers
{
    public class AdmobManager : MonoBehaviour
    {
        public static Action OnRewardPlayer;

        private const string BannerIDTest = "ca-app-pub-3940256099942544/6300978111";
        private const string RewardedVideoIDTest = "ca-app-pub-3940256099942544/5224354917";
        private const string RewardedInterstitalIDTest = "ca-app-pub-3940256099942544/1033173712";
        private const string InterstitalIDTest = "ca-app-pub-3940256099942544/1033173712";

        private static bool admobInitialized;

        private static BannerView bannerView;

        private static InterstitialAd interstitial;

        private static InterstitialAd rewardedInterstitial;
        private static RewardBasedVideoAd rewardedVideo;

        private static bool interstitalAsRewardedAd;

        private bool rewardPlayer;

        public static bool IsAdmobInitialized()
        {
            return admobInitialized;
        }

        public void Initialize()
        {
            if(!FrameworkValues.UseAdmob)
                return;

            if (string.IsNullOrEmpty(FrameworkValues.AdmobAppID))
            {
                Debug.LogError("FazApp: Please add your Admob App ID to FrameworkValues");
                return;
            }

            MobileAds.Initialize(FrameworkValues.AdmobAppID);

            if(FrameworkValues.UseBanner)
                InitializeAndRequestBanner();

            InitializeStaticInterstitial();
            InitializeInterstitial();
            InitializeRewardedVideo();

            admobInitialized = true;
        }
    
        public static void HideBanner()
        {
            if(!admobInitialized || !FrameworkValues.UseBanner)
                return;

            bannerView.Hide();
        }

        public static void ShowBanner()
        {
            if (!admobInitialized || !FrameworkValues.UseBanner)
                return;

            bannerView.Show();
        }

        public static bool CanShowInterstitial()
        {
            if (!admobInitialized)
                return false;

            return interstitial.IsLoaded();
        }

        public static void ShowIntertitial()
        {
            if (!admobInitialized || !interstitial.IsLoaded())
                return;

            interstitial.Show();
        }

        public static bool CanShowRewardedVideo()
        {
            if (!admobInitialized)
                return false;

#if UNITY_EDITOR
            return true;
#endif

            return (rewardedVideo.IsLoaded() || rewardedInterstitial.IsLoaded()) && 
                   OnRewardPlayer != null;
        }

        public static void ShowRewardedVideo()
        {
            if (!admobInitialized)
                return;

#if UNITY_EDITOR
            OnRewardPlayer?.Invoke();
            return;
#endif

            if (rewardedVideo.IsLoaded())
            {
                rewardedVideo.Show();
                return;
            }

            if (rewardedInterstitial.IsLoaded())
            {
                interstitalAsRewardedAd = true;
                rewardedInterstitial.Show();
            }
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (!pauseStatus && rewardPlayer)
            {
                rewardPlayer = false;
                OnRewardPlayer?.Invoke();
            }
        }

        private static AdRequest BuildAdRequest()
        {
            var builder = new AdRequest.Builder();

            foreach (var testDevice in FrameworkValues.TestDevices)
            {
                builder.AddTestDevice(testDevice);
            }

            var request = builder.Build();

            return request;
        }

        private static void InitializeAndRequestBanner()
        {
            bannerView = new BannerView(FrameworkValues.TestAds ? BannerIDTest : FrameworkValues.BannerID, AdSize.Banner, AdPosition.Bottom);
            AdRequest request = BuildAdRequest();
            bannerView.LoadAd(request);
        }
    
        private void InitializeStaticInterstitial()
        {
            rewardedInterstitial = new InterstitialAd(FrameworkValues.TestAds ? RewardedInterstitalIDTest : FrameworkValues.RewardedInterstitalID);

            rewardedInterstitial.OnAdClosed += HandleStaticInterstitialClosed;
            rewardedInterstitial.OnAdFailedToLoad += HandleStaticInterstitalFailedToLoad;

            RequestStaticInterstitial();
        }

        private void RequestStaticInterstitial()
        {
            AdRequest request = BuildAdRequest();
            rewardedInterstitial.LoadAd(request);
        }

        private void HandleStaticInterstitialClosed(object sender, EventArgs args)
        {
            if (interstitalAsRewardedAd)
            {
                interstitalAsRewardedAd = false;

                rewardPlayer = true;
            }

            RequestStaticInterstitial();
        }

        private void HandleStaticInterstitalFailedToLoad(object sender, EventArgs args)
        {
            StartCoroutine(WaitAndTryToLoadStaticInterstital());
        }

        private IEnumerator WaitAndTryToLoadStaticInterstital()
        {
            yield return new WaitForSecondsRealtime(10f);
            RequestStaticInterstitial();
        }

        private void InitializeInterstitial()
        {
            interstitial = new InterstitialAd(FrameworkValues.TestAds ? InterstitalIDTest : FrameworkValues.InterstitialID);

            interstitial.OnAdClosed += HandleInterstitialClosed;
            interstitial.OnAdFailedToLoad += HandleInterstitalFailedToLoad;

            RequestInterstitial();
        }

        private void RequestInterstitial()
        {
            AdRequest request = BuildAdRequest();
            interstitial.LoadAd(request);
        }

        private void HandleInterstitialClosed(object sender, EventArgs args)
        {
            RequestInterstitial();
        }

        private void HandleInterstitalFailedToLoad(object sender, EventArgs args)
        {
            StartCoroutine(WaitAndTryToLoadInterstital());
        }

        private IEnumerator WaitAndTryToLoadInterstital()
        {
            yield return new WaitForSecondsRealtime(10f);
            RequestInterstitial();
        }

        private void InitializeRewardedVideo()
        {
            rewardedVideo = RewardBasedVideoAd.Instance;

            rewardedVideo.OnAdRewarded += HandleRewardedVideoFinished;
            rewardedVideo.OnAdClosed += HandleRewardedVideoClosed;
            rewardedVideo.OnAdFailedToLoad += HandleRewardedVideoFailedToLoad;

            RequestRewardedVideo();
        }

        private void RequestRewardedVideo()
        {
            AdRequest request = BuildAdRequest();
            rewardedVideo.LoadAd(request, FrameworkValues.TestAds ? RewardedVideoIDTest : FrameworkValues.RewardedVideoID);
        }

        private void HandleRewardedVideoFinished(object sender, Reward args)
        {
            rewardPlayer = true;
        }

        private void HandleRewardedVideoClosed(object sender, EventArgs args)
        {
            RequestRewardedVideo();
        }

        private void HandleRewardedVideoFailedToLoad(object sender, EventArgs args)
        {
            StartCoroutine(WaitAndTryToLoadRewardedAd());
        }

        private IEnumerator WaitAndTryToLoadRewardedAd()
        {
            yield return new WaitForSecondsRealtime(10f);
            RequestRewardedVideo();
        }
    }
}
