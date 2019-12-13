using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FazAppFramework.Managers
{
    public class MainManager : MonoBehaviour
    {
        public static MainManager Instance = null;

        [HideInInspector]public OwnInterstitial ownInterstitial;

        private float currentTime;

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);

            Application.targetFrameRate = 60;
            
            Debug.Log("FazApp: FRAMEWORK STARTING");
            
            InitializeServices();

            var sessions = PlayerPrefs.GetInt(FrameworkValues.SESSION_COUNTER_KEY, 0);
            PlayerPrefs.SetInt(FrameworkValues.SESSION_COUNTER_KEY, sessions + 1);
            
            if (sessions == 0)
            {
                LocalNotificationManager.SendNotifications();
            }

            currentTime = 0;
            StartCoroutine(WaitForServicesInitialization());
        }

        private void InitializeServices()
        {
            GetComponent<AdmobManager>().Initialize();

            GooglePlayServicesManager.Initialize();
            
            if (FrameworkValues.UseOwnIntertital)
                ownInterstitial = gameObject.AddComponent<OwnInterstitial>();
            FirebaseManager.Initialize();
        }

        private IEnumerator WaitForServicesInitialization()
        {
            while (true)
            {
                if (ServicesReady())
                {
                    SceneManager.LoadScene(1, LoadSceneMode.Single);
                    yield break;
                }

                currentTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }

        private bool ServicesReady()
        {
            var firebase = !FrameworkValues.UseFirebase || FirebaseManager.FirebaseIsReady || FirebaseManager.FirebaseInitFailed;
            var interstitial = !FrameworkValues.UseOwnIntertital || !FrameworkValues.UseFirebase ||
                               (FirebaseManager.OwnIntertitialDataIsReady && ownInterstitial.loadingFinished) ||
                                FirebaseManager.OwnInterstitalDataFetchFailed || FirebaseManager.FirebaseInitFailed;
            var time = currentTime >= FrameworkValues.SERVICE_WAITING_TIME;
            
            return (firebase && interstitial) || time;
        }
    }
}
