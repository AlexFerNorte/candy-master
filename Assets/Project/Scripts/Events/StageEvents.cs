using CandyMaster.Project.Scripts.UI.Core;
using UnityEngine;
using UnityEngine.Events;
using Screen = CandyMaster.Project.Scripts.UI.Core.Screen;

namespace CandyMaster.Project.Scripts.Events
{
    public class StageEvents
    {
        public readonly UnityEvent OnStagePrepared;
        public readonly UnityEvent OnStagePerforming;
        public readonly UnityEvent OnStageCompleted;
        public readonly UnityEvent<Screen> OnStageStepPrepared;
        public readonly UnityEvent OnStageStepPerforming;
        public readonly UnityEvent OnStageStepCompleted;


        public StageEvents()
        {
            OnStagePrepared = new UnityEvent();
            OnStagePerforming = new UnityEvent();
            OnStageCompleted = new UnityEvent();
            OnStageStepPrepared = new UnityEvent<Screen>();
            OnStageStepPerforming = new UnityEvent();
            OnStageStepCompleted = new UnityEvent();
        }
    }
}