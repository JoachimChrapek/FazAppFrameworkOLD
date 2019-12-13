using System.Collections;
using System.Collections.Generic;
using FazAppFramework;
using FazAppFramework.Managers;
using UnityEngine;
using UnityEngine.UI;

public class PlayServicesTest : MonoBehaviour
{
    public Image statusImage;

    private void Start()
    {
        if (!FrameworkValues.UseGooglePlayServices)
        {
            statusImage.color = Color.yellow;
            return;
        }

        if (GooglePlayServicesManager.IsUserLoggedIn())
        {
            statusImage.color = Color.green;
        }
        else
        {
            statusImage.color = Color.red;
        }
    }
}
