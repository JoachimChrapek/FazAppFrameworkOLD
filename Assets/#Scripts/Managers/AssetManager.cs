using UnityEngine;

namespace Managers
{
    public class AssetManager : MonoBehaviour
    {
        public static AssetManager instance = null;

        private void Awake()
        {
            instance = this;
        }
    }
}
