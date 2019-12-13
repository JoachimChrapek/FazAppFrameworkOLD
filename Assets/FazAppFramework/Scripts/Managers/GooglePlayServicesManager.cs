using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

namespace FazAppFramework.Managers
{
    public static class GooglePlayServicesManager
    {
        private static bool servicesReady;
        private static bool userLoggedIn;

        public static void Initialize()
        {
            if(!FrameworkValues.UseGooglePlayServices)
                return;

            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.Activate();

            servicesReady = true;

            LogUser();
        }

        public static bool IsUserLoggedIn()
        {
            return servicesReady && userLoggedIn;
        }

        private static bool CheckIfUserIsLogged()
        {
            if (userLoggedIn)
                return true;

            LogUser();
            return false;
        }

        private static void LogUser()
        {
            if(!servicesReady)
                return;

            Social.localUser.Authenticate(succes =>
            {
                userLoggedIn = succes;

                if (!succes)
                {
                    Debug.LogError("FazApp: Failed to authenticate user!");
                }
            });
        }
    }
}
