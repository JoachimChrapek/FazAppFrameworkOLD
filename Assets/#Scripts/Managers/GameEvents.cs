using System;

namespace Managers
{
    public static class GameEvents
    {
        public static event Action OnGameStart;
        public static event Action OnGameOver;
        public static event Action OnGameReset;

        public static void PublishOnGameStart()
        {
            OnGameStart?.Invoke();
        }

        public static void PublishOnGameOver()
        {
            OnGameOver?.Invoke();
        }

        public static void PublishOnGameReset()
        {
            OnGameReset?.Invoke();
        }
    }
}
