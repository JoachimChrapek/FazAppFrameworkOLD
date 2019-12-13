using UnityEditor;
using UnityEngine;

namespace FazAppFramework.Development
{
    public class PlayerPrefsClear : EditorWindow
    {
        [MenuItem("FazApp/Clear PlayerPrefs")]
        public static void ClearPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
        }

    }
}
