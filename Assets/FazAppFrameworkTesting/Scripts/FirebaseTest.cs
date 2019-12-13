using System.Collections;
using System.Collections.Generic;
using FazAppFramework;
using FazAppFramework.Managers;
using UnityEngine;
using UnityEngine.UI;

public class FirebaseTest : MonoBehaviour
{
    public Image statusImage;

    public void SendTestEvent()
    {
        FirebaseManager.LogEvent("TEST_EVENT");
    }

    private void Start()
    {
        if (!FrameworkValues.UseFirebase)
        {
            statusImage.color = Color.yellow;
            return;
        }

        if (FirebaseManager.FirebaseIsReady)
        {
            statusImage.color = Color.green;
        }
        else
        {
            statusImage.color = Color.red;
        }
    }
}
