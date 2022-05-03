using UnityEngine.Events;

namespace CandyMaster.Project.Scripts.UI.Common
{
    public class UIEvents
    {
        public readonly UnityEvent OnRecipeAccepted;
        public readonly UnityEvent OnRecipeDeclined;
        public readonly UnityEvent OnStageContinueClicked;


        public UIEvents()
        {
            OnRecipeAccepted = new UnityEvent();
            OnRecipeDeclined = new UnityEvent();
            OnStageContinueClicked = new UnityEvent();
        }
    }
}