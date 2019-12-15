using System.Collections.Generic;

namespace FazAppFramework
{
    public class FrameworkValues
    {
        public const bool UseAdmob = true;
        public const bool TestAds = true;
        public const string AdmobAppID = "";
        public const bool UseBanner = false;
        public const string BannerID = "";
        public const string InterstitialID = "";
        public const string RewardedInterstitalID = "";
        public const string RewardedVideoID = "";

        public static readonly List<string> TestDevices = new List<string>
        {
            "",
        };

        public const bool UseFirebase = true;
        public const bool UseOwnIntertital = true;

        public const bool UseGooglePlayServices = false;

        public const bool UseLocalNotifications = true;
        public const string Notification24hTitle = "24h Title";
        public const string Notification24hMessage = "24h Message";
        public const string Notification24hCallback = "Notification24hCallback";
        public const string Notification48hTitle = "48h Title";
        public const string Notification48hMessage = "48h Message";
        public const string Notification48hCallback = "Notification48hCallback";
        public const string Notification72hTitle = "72h Title";
        public const string Notification72hMessage = "72h Message";
        public const string Notification72hCallback = "Notification72hCallback";
        public const string NotificationRepeatableTitle = "Repeat";
        public const string NotificationRepeatableMessage = "Repeat Message";
        public const string NotificationRepeatableCallback = "NotificationRepeatableCallback";

        public const string ShareSubject = "Test share subject";
        public const string ShareText = "Test share text";

        public const bool UseFacebook = true;
        public const string FacebookShareTitle = "Test FB Title";
        public const string FacebookShareDescription = "Test FB Description";

        #region CONSTANT_VALUES

        public const string MORE_GAMES_LINK = "https://play.google.com/store/apps/developer?id=FazApp";

        public const float SERVICE_WAITING_TIME = 5f;

        public const string SESSION_COUNTER_KEY = "SessionCounter";

        public const string RATE_US_DONE_KEY = "RateUsDone";

        public const string FIREBASE_REMOTE_CONFIG_SHOW_OWN_INTERSTITIAL_KEY_B = "show_own_interstitial";
        public const string FIREBASE_REMOTE_CONFIG_OWN_INTERSTITIAL_IMAGE_URL_KEY_S = "own_interstitial_image_url";
        public const string FIREBASE_REMOTE_CONFIG_OWN_INTERSTITIAL_STORE_URL_KEY_S = "own_interstitial_store_url";
        public const string FIREBASE_REMOTE_CONFIG_OWN_INTERSTITIAL_TITLE_KEY_S = "own_interstitial_title";
        public const string FIREBASE_REMOTE_CONFIG_OWN_INTERSTITIAL_DESCRIPTION_KEY_S = "own_interstitial_description";

        #endregion
    }
}
