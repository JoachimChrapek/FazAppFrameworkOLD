using System.Collections;
using System.Collections.Generic;
using FazAppFramework;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestGameManager : MonoBehaviour
{
    public enum Context
    {
        Main = 0,
        Admob = 1,
        Firebase = 2,
        OwnInterstital = 3,
        LocalNotification = 4,
        PlayServices = 5
    }
    
    public GameObject mainPanel;
    public GameObject admobPanel;
    public GameObject firebasePanel;
    public GameObject ownInterstitialPanel;
    public GameObject localNotificationsPanel;
    public GameObject playServicesPanel;

    private Context currentContext;
    private Dictionary<Context, GameObject> panels = new Dictionary<Context, GameObject>(8);

    private void Start()
    {
        panels[Context.Main] = mainPanel;
        panels[Context.Admob] = admobPanel;
        panels[Context.Firebase] = firebasePanel;
        panels[Context.OwnInterstital] = ownInterstitialPanel;
        panels[Context.LocalNotification] = localNotificationsPanel;
        panels[Context.PlayServices] = playServicesPanel;

        foreach (var panel in panels)
        {
            panel.Value.SetActive(panel.Key == Context.Main);
        }

        currentContext = Context.Main;
    }

    public void SwitchContext(int newContext)
    {
        panels[currentContext].SetActive(false);
        panels[(Context)newContext].SetActive(true);
        currentContext = (Context) newContext;
    }
    
    public void OnShare()
    {
        var share = new NativeShare();
        share.SetSubject(FrameworkValues.ShareSubject);
        share.SetText(FrameworkValues.ShareText + "\n https://play.google.com/store/apps/details?id=com.FazApp.TestApp");
        share.Share();
    }

    public void OnBack()
    {
        SwitchContext(0);
    }

    public void OnLoadGameScene()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }

    public void OnExit()
    {
        Application.Quit();
    }
}
