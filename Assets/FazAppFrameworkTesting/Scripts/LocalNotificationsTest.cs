using System.Collections;
using System.Collections.Generic;
using FazAppFramework;
using FazAppFramework.Managers;
using UnityEngine;
using UnityEngine.UI;

public class LocalNotificationsTest : MonoBehaviour
{
    public Image statusImage;

    public Text callbackText;

    public Image callbackButtonImage;

    public void GetNotificationCallback()
    {
        if (!FrameworkValues.UseLocalNotifications)
            return;

        string callback;
        
        if (LocalNotificationManager.GetNotificationCallback(out callback))
        {
            callbackText.text = callback;
            callbackButtonImage.color = Color.green;
        }
        else
        {
            callbackButtonImage.color = Color.red;
        }
    }

    private void Start()
    {
        statusImage.color = !FrameworkValues.UseLocalNotifications ? Color.yellow : Color.green;
    }
}
