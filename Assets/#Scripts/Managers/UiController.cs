using FazAppFramework;
using FazAppFramework.UI;
using UnityEngine;

namespace Managers
{
    public class UiController : MonoBehaviour
    {
        public static UiController instance = null;

        private bool rateUsForThisSessionDone;

        public void ShowOwnInterstitial()
        {
            GetComponentInChildren<OwnInterstitialPanel>(true).Show();
        }

        private void OnGameStart()
        {

        }

        private void OnGameOver()
        {

        }

        private void OnGameReset()
        {
            if (!rateUsForThisSessionDone &&
                GameManager.instance.GamesPlayedInSession > 0 &&
                PlayerPrefs.GetInt(FrameworkValues.RATE_US_DONE_KEY, 0) == 0)
            {
                //TODO show/animate rate us
                rateUsForThisSessionDone = true;
            }

        }

        private void Awake()
        {
            instance = this;

            GameEvents.OnGameStart += OnGameStart;
            GameEvents.OnGameOver += OnGameOver;
            GameEvents.OnGameReset += OnGameReset;
        }
    }
}
