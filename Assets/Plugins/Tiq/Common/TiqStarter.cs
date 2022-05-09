using UnityEngine;

namespace Tiq.Plugins.Tiq.Common
{
    public class TiqStarter : MonoBehaviour
    {
        private static TiqStarter _instance;
        
        public static TiqStarter Instance => GetInstance();
        public Tiq Tiq { get; private set; }
        

        private static TiqStarter GetInstance()
        {
            if (_instance == null)
            {
                var gameObject = new GameObject("TiqStarter[required]");
                _instance = gameObject.AddComponent<TiqStarter>();
                _instance.Initialize();
            }

            return _instance;
        }

        private void Initialize()
        {
            Tiq = new Tiq();
        }
    }
}