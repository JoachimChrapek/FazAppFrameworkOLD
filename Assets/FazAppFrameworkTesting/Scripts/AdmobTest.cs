using FazAppFramework;
using FazAppFramework.Managers;
using UnityEngine;
using UnityEngine.UI;

public class AdmobTest : MonoBehaviour
{
    public Image statusImage;

    public Image bannerButtonImage;
    private bool banner = false;

    public Image interstitialButtonImage;

    public Image rewardedButtonImage;

    public Image resetButtonImage;

    public void ToggleBaner()
    {
        banner = !banner;

        if (banner)
        {
            AdmobManager.ShowBanner();
            bannerButtonImage.color = Color.green;
        }
        else
        {
            AdmobManager.HideBanner();
            bannerButtonImage.color = Color.white;
        }
    }

    public void ShowInterstitial()
    {
        AdmobManager.ShowIntertitial();
        interstitialButtonImage.color = Color.white;
    }

    public void ShowRewarded()
    {
        AdmobManager.ShowRewardedVideo();
        rewardedButtonImage.color = Color.white;
    }

    public void ResetReward()
    {
        resetButtonImage.color = Color.white;
    }

    private void Start()
    {
        AdmobManager.OnRewardPlayer += delegate { resetButtonImage.color = Color.green; };

        if (!FrameworkValues.UseAdmob)
        {
            statusImage.color = Color.yellow;
            return;
        }

        if (AdmobManager.IsAdmobInitialized())
        {
            statusImage.color = Color.green;
        }
        else
        {
            statusImage.color = Color.red;
        }
    }

    private void Update()
    {
        interstitialButtonImage.color = AdmobManager.CanShowInterstitial() ? Color.green : Color.white;
        rewardedButtonImage.color = AdmobManager.CanShowRewardedVideo() ? Color.green : Color.white;
    }

    private void OnDestroy()
    {
        AdmobManager.OnRewardPlayer = null;
    }
}
