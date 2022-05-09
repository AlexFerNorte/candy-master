using CandyMaster.Project.Scripts.Data.Mutable;
using UnityEngine.Events;
using Screen = CandyMaster.Project.Scripts.UI.Core.Screen;

namespace CandyMaster.Project.Scripts.Events
{
    public class StageEvents
    {
        public readonly UnityEvent OnStagePrepared;
        public readonly UnityEvent OnStagePromoted;
        public readonly UnityEvent OnStageCompleted;
        public readonly UnityEvent<Screen> OnStageStepPrepared;
        public readonly UnityEvent OnStageStepPromoted;
        public readonly UnityEvent<float> OnStageStepPerforming;
        public readonly UnityEvent<Screen> OnStageStepCompleted;


        public StageEvents()
        {
            OnStagePrepared = new UnityEvent();
            OnStagePromoted = new UnityEvent();
            OnStageCompleted = new UnityEvent();
            OnStageStepPrepared = new UnityEvent<Screen>();
            OnStageStepPromoted = new UnityEvent();
            OnStageStepPerforming = new UnityEvent<float>();
            OnStageStepCompleted = new UnityEvent<Screen>();
        }
    }
}