using System;
using System.Collections.Generic;
using Plugins.TiqCore.Core;
using UnityEngine;

namespace Plugins.TiqCore.Common
{
    public class TiqStarter : MonoBehaviour
    {
        private static TiqStarter _instance;
        public static TiqStarter Instance => GetInstance();
        

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
            
        }
    }
}