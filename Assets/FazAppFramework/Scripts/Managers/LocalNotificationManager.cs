using System;
using Unity.Notifications.Android;
using UnityEngine;

namespace FazAppFramework.Managers
{
    public class LocalNotificationManager
    {
        private const string ChannelName = "main_channel";

        public static void SendNotifications()
        {
            if (!FrameworkValues.UseLocalNotifications)
                return;
            
            try
            {
                Debug.Log("FazApp: SENDING NOTIFICATION");

                var channel = new AndroidNotificationChannel()
                {
                    Id = ChannelName,
                    Name = "Default Channel",
                    Importance = Importance.High,
                    Description = "Generic notifications",
                    EnableLights = true,
                    EnableVibration = true,
                    
                };
                AndroidNotificationCenter.RegisterNotificationChannel(channel);

                var notification = new AndroidNotification()
                {
                    Title = FrameworkValues.Notification24hTitle,
                    Text = FrameworkValues.Notification24hMessage,
                    FireTime = DateTime.Now.AddHours(24),
                    SmallIcon = "small_icon",
                    LargeIcon = "large_icon",
                    IntentData = FrameworkValues.Notification24hCallback,

                };
                AndroidNotificationCenter.SendNotification(notification, ChannelName);

                notification = new AndroidNotification()
                {
                    Title = FrameworkValues.Notification48hTitle,
                    Text = FrameworkValues.Notification48hMessage,
                    FireTime = DateTime.Now.AddHours(48),
                    SmallIcon = "small_icon",
                    LargeIcon = "large_icon",
                    IntentData = FrameworkValues.Notification48hCallback
                };
                AndroidNotificationCenter.SendNotification(notification, ChannelName);

                notification = new AndroidNotification()
                {
                    Title = FrameworkValues.Notification72hTitle,
                    Text = FrameworkValues.Notification72hMessage,
                    FireTime = DateTime.Now.AddHours(72),
                    SmallIcon = "small_icon",
                    LargeIcon = "large_icon",
                    IntentData = FrameworkValues.Notification72hCallback
                };
                AndroidNotificationCenter.SendNotification(notification, ChannelName);

                notification = new AndroidNotification()
                {
                    Title = FrameworkValues.NotificationRepeatableTitle,
                    Text = FrameworkValues.NotificationRepeatableMessage,
                    FireTime = DateTime.Now.AddHours(96),
                    SmallIcon = "small_icon",
                    LargeIcon = "large_icon",
                    IntentData = FrameworkValues.NotificationRepeatableCallback,
                    RepeatInterval = TimeSpan.FromHours(24)
                };
                AndroidNotificationCenter.SendNotification(notification, ChannelName);
            }
            catch (Exception e)
            {
                Debug.LogError("FazApp: NOTIFICATION ERROR: " + e);
            }
        }

        public static bool GetNotificationCallback(out string callback)
        {
            var notificationIntentData = AndroidNotificationCenter.GetLastNotificationIntent();

            if (notificationIntentData != null)
            {
                callback = notificationIntentData.Notification.IntentData;
                return true;
            }

            callback = String.Empty;
            return false;
        }
    }
}
