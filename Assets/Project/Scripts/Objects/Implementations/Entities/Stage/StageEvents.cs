using System;
using UnityEngine.Events;

namespace CandyMaster.Project.Scripts.Objects.Implementations.Entities.Stage
{
    public class StageEvents
    {
        public readonly UnityEvent OnStagePrepared;
        public readonly UnityEvent OnStagePerforming;
        public readonly UnityEvent OnStageCompleted;
        public readonly UnityEvent OnStageStepPrepared;
        public readonly UnityEvent OnStageStepPerforming;
        public readonly UnityEvent OnStageStepCompleted;


        public StageEvents()
        {
            OnStagePrepared = new UnityEvent();
            OnStagePerforming = new UnityEvent();
            OnStageCompleted = new UnityEvent();
            OnStageStepPrepared = new UnityEvent();
            OnStageStepPerforming = new UnityEvent();
            OnStageStepCompleted = new UnityEvent();
        }
    }
}