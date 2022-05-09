using System;
using System.Collections.Generic;
using UnityEngine;
using Screen = CandyMaster.Project.Scripts.UI.Core.Screen;

namespace CandyMaster.Project.Scripts.UI.Common
{
    public class UI : MonoBehaviour
    {
        [SerializeField] private List<Screen> screens;


        public void Initialize()
        {
            
        }

        public void ActivateScreen(Screen screen)
        {
            IterateScreens(s =>
            {
                if (s == screen)
                {
                    s.Appear();
                }
                else
                {
                    s.Disappear();
                }
            });
        }

        
        
        #region Utilities
        private void IterateScreens(Action<Screen> onIterate)
        {
            foreach (var screen in screens)
            {
                onIterate?.Invoke(screen);
            }
        }
        #endregion
    }
}