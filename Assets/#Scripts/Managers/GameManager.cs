using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance = null;
        
        public int GamesPlayedInSession { get; private set; }

        private void OnGameStart()
        {
            GamesPlayedInSession++;
        }

        private void OnGameOver()
        {

        }

        private void OnGameReset()
        {

        }

        private void Awake()
        {
            GameEvents.OnGameStart += OnGameStart;
            GameEvents.OnGameOver += OnGameOver;
            GameEvents.OnGameReset += OnGameReset;
        }

        private void Start()
        {


            UiController.instance.ShowOwnInterstitial();
        }

        private void OnDestroy()
        {
            GameEvents.OnGameStart -= OnGameStart;
            GameEvents.OnGameOver -= OnGameOver;
            GameEvents.OnGameReset -= OnGameReset;
        }
    }
}
