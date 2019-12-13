using FazAppFramework;
using FazAppFramework.Managers;
using UnityEngine;

namespace UI
{
    public class ButtonEvents : MonoBehaviour
    {
        public void OnMoreGames()
        {
            FirebaseManager.LogEvent("more_games_button_click");
            Application.OpenURL(FrameworkValues.MORE_GAMES_LINK);
        }

        public void OnShare()
        {
            FirebaseManager.LogEvent("share_button_click");
            var share = new NativeShare();
            share.SetSubject(FrameworkValues.ShareSubject);
            share.SetText(FrameworkValues.ShareText + "\n" + "https://play.google.com/store/apps/details?id=" + Application.identifier);
            share.Share();
        }

        public void OnExitApp()
        {
            Application.Quit();
        }
    }
}
