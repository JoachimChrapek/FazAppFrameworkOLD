using System.Collections;
using System.Collections.Generic;
using FazAppFramework;
using FazAppFramework.Managers;
using UnityEngine;
using UnityEngine.UI;

public class OwnInterstitialTest : MonoBehaviour
{
    public Image statusImage;

    public Text titleText;
    public Text descriptionText;
    public Text storeUrl;
    public Text imageUrl;
    public Image textureToShow;

    public void ShowOwnInterstital()
    {
        var data = MainManager.Instance.ownInterstitial.data;
        
        var tex = MainManager.Instance.ownInterstitial.interstitialTexture;
        if (tex != null)
        {
            textureToShow.sprite = tex;
        }

        titleText.text = data.title;
        descriptionText.text = data.description;
        storeUrl.text = data.storeURL;
        imageUrl.text = data.imageURL;
    }

    private void Start()
    {
        if (!FrameworkValues.UseOwnIntertital)
        {
            statusImage.color = Color.yellow;
            return;
        }

        if (MainManager.Instance.ownInterstitial.showInterstitial)
        {
            statusImage.color = Color.green;
        }
        else
        {
            statusImage.color = Color.red;
        }
    }
}
