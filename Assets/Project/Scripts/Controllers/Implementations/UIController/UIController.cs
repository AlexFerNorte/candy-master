using System;
using System.Collections.Generic;
using CandyMaster.Project.Scripts.Controllers.Core;
using UnityEngine;
using Screen = CandyMaster.Project.Scripts.UI.Core.Screen;


namespace CandyMaster.Project.Scripts.Controllers.Implementations.UIController
{
    [Serializable]
    public class UIController : Controller<UIControllerInitializeData>
    {
        #region Serialized
        [SerializeField] private List<Screen> screens;
        #endregion

        #region Current

        #endregion

        

        #region Initialization
        public override void Initialize(UIControllerInitializeData data)
        {
            base.Initialize(data);

            foreach (var screen in screens)
            {
                screen.Initialize(InitializeData.StageEvents);
            }
        }
        #endregion

        
        
        #region Common
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

        public void DeactivateScreen(Screen screen)
        {
            screen.Disappear();
        }
        #endregion
        
        
        
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