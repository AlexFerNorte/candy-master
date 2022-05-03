using UnityEngine.Events;

namespace CandyMaster.Project.Scripts.Events
{
    public class UIEvents
    {
        public readonly UnityEvent OnRecipeAccepted;
        public readonly UnityEvent OnRecipeDeclined;
        public readonly UnityEvent OnStageContinueClicked;
        public readonly UnityEvent OnStageStepPerformed;


        public UIEvents()
        {
            OnRecipeAccepted = new UnityEvent();
            OnRecipeDeclined = new UnityEvent();
            OnStageContinueClicked = new UnityEvent();
        }
    }
}