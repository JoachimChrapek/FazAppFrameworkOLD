using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase;
using Firebase.Analytics;
using Firebase.RemoteConfig;
using UnityEngine;

namespace FazAppFramework.Managers
{
    public static class FirebaseManager
    {
        public static bool FirebaseIsReady;
        public static bool OwnIntertitialDataIsReady;

        public static bool FirebaseInitFailed;
        public static bool OwnInterstitalDataFetchFailed;

        public static void Initialize()
        {
            if (!FrameworkValues.UseFirebase)
                return;

            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
            {
                var dependencyStatus = task.Result;
                if (dependencyStatus == DependencyStatus.Available)
                {
                    FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);

                    Firebase.Messaging.FirebaseMessaging.TokenRegistrationOnInitEnabled = false;
                    Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;
                    Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReceived;

                    FirebaseIsReady = true;

                    LogEvent("game_started");

                    if (FrameworkValues.UseOwnIntertital)
                    {
                        InitializeRemoteConfig();
                        FetchDataAsync();
                    }
                }
                else
                {
                    FirebaseIsReady = false;
                    FirebaseInitFailed = true;

                    Debug.LogError("FazApp: Could not resolve all Firebase dependencies: " + dependencyStatus);

                    return;
                }
            });
        }

        public static void LogEvent(string eventName)
        {
            if (!FirebaseIsReady)
                return;

            FirebaseAnalytics.LogEvent(eventName);
        }

        public static OwnInterstitialData GetOwnInterstitialData()
        {
            var fetchedData = new OwnInterstitialData
            {
                showOwnInterstitial = FirebaseRemoteConfig.GetValue(FrameworkValues.FIREBASE_REMOTE_CONFIG_SHOW_OWN_INTERSTITIAL_KEY_B).BooleanValue,
                imageURL = FirebaseRemoteConfig.GetValue(FrameworkValues.FIREBASE_REMOTE_CONFIG_OWN_INTERSTITIAL_IMAGE_URL_KEY_S).StringValue,
                storeURL = FirebaseRemoteConfig.GetValue(FrameworkValues.FIREBASE_REMOTE_CONFIG_OWN_INTERSTITIAL_STORE_URL_KEY_S).StringValue,
                title = FirebaseRemoteConfig.GetValue(FrameworkValues.FIREBASE_REMOTE_CONFIG_OWN_INTERSTITIAL_TITLE_KEY_S).StringValue,
                description = FirebaseRemoteConfig.GetValue(FrameworkValues.FIREBASE_REMOTE_CONFIG_OWN_INTERSTITIAL_DESCRIPTION_KEY_S).StringValue
            };

            return fetchedData;
        }

        private static void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token)
        {
            Debug.Log("Received Registration Token: " + token.Token);
        }

        private static void OnMessageReceived(object sender, Firebase.Messaging.MessageReceivedEventArgs e)
        {
            Debug.Log("Received a new message from: " + e.Message.From);
        }

        private static void InitializeRemoteConfig()
        {
            var defaults = new Dictionary<string, object>
            {
                {FrameworkValues.FIREBASE_REMOTE_CONFIG_SHOW_OWN_INTERSTITIAL_KEY_B, false},
                {FrameworkValues.FIREBASE_REMOTE_CONFIG_OWN_INTERSTITIAL_IMAGE_URL_KEY_S, "DEFAULT"},
                {FrameworkValues.FIREBASE_REMOTE_CONFIG_OWN_INTERSTITIAL_STORE_URL_KEY_S, "DEFAULT"},
                {FrameworkValues.FIREBASE_REMOTE_CONFIG_OWN_INTERSTITIAL_TITLE_KEY_S, "DEFAULT"},
                {FrameworkValues.FIREBASE_REMOTE_CONFIG_OWN_INTERSTITIAL_DESCRIPTION_KEY_S, "DEFAULT"}
            };


            FirebaseRemoteConfig.SetDefaults(defaults);
        }

        private static Task FetchDataAsync()
        {
            Debug.Log("FazApp: Fetching own interstitial data...");
            
            Task fetchTask = FirebaseRemoteConfig.FetchAsync(TimeSpan.Zero);
            return fetchTask.ContinueWith(FetchComplete);
        }

        private static void FetchComplete(Task fetchTask)
        {
            if (fetchTask.IsCanceled)
            {
                Debug.LogWarning("FazApp: Own interstitial data fetch canceled.");
            }
            else if (fetchTask.IsFaulted)
            {
                Debug.LogError("FazApp: Own interstitial data fetch encountered an error.");
            }
            else if (fetchTask.IsCompleted)
            {
                Debug.Log("FazApp: Own interstitial data fetch completed successfully!");
            }

            var info = FirebaseRemoteConfig.Info;
            switch (info.LastFetchStatus)
            {
                case LastFetchStatus.Success:
                    FirebaseRemoteConfig.ActivateFetched();
                    Debug.Log($"FazApp: Own interstitial data loaded and ready (last fetch time {info.FetchTime}).");
                    
                    OwnIntertitialDataIsReady = true;
                    break;
                case LastFetchStatus.Failure:
                    OwnInterstitalDataFetchFailed = true;
                    switch (info.LastFetchFailureReason)
                    {
                        case FetchFailureReason.Error:
                            Debug.LogError("FazApp: Own interstitial data fetch failed for unknown reason");
                            break;
                        case FetchFailureReason.Throttled:
                            Debug.LogWarning("FazApp: Own interstitial data fetch throttled until " +
                                             info.ThrottledEndTime);
                            break;
                    }

                    break;
                case LastFetchStatus.Pending:
                    Debug.Log("FazApp: Latest own interstitial data fetch call still pending.");
                    break;
            }
        }
    }

    public struct OwnInterstitialData
    {
        public bool showOwnInterstitial;
        public string imageURL;
        public string storeURL;
        public string title;
        public string description;
    }
}
