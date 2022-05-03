using System;
using System.Collections.Generic;
using CandyMasters.Project.Scripts.Controllers.Core;
using CandyMasters.Project.Scripts.Objects.Core.Entities;
using CandyMasters.Project.Scripts.Objects.Implementations.Entities.Stage;
using UnityEngine;

namespace CandyMasters.Project.Scripts.Controllers.Implementations.StageController
{
    [Serializable]
    public class StageController : Controller<StageControllerInitializeData>
    {
        #region Serialized
        [field: SerializeField] public List<Stage> Stages { get; set; }
        [field: SerializeField] public List<StageStep> StageSteps { get; set; }
        #endregion

        #region Current
        public int CurrentStageIndex { get; private set; }
        public int NextStageIndex => CurrentStageIndex + 1 >= Stages.Count ? 0 : CurrentStageIndex + 1;
        public Stage CurrentStage => Stages[CurrentStageIndex];
        
        public int CurrentStageStepIndex { get; private set; }
        public int NextStageStepIndex => CurrentStageStepIndex + 1 >= StageSteps.Count ? 0 : CurrentStageStepIndex + 1;
        public StageStep CurrentStageStep => StageSteps[CurrentStageStepIndex];
        #endregion


        #region Stages
        public void PrepareStage(int index)
        {
            CurrentStage.Disappear();

            CurrentStageIndex = index;
            
            CurrentStage.Prepare();
            Data.GameEvents.GPonStagePrepare?.Invoke();
        }

        public void PerformCurrentStage()
        {
            CurrentStage.Perform();
            Data.GameEvents.GPonStagePerform?.Invoke();
        }

        public void OnCurrentStageCompleted()
        {
            CurrentStage.Complete();
            Data.GameEvents.GPonStageComplete?.Invoke();
        }
        #endregion

        
        #region StageSteps
        public void PrepareStageStep(int index)
        {
            CurrentStageStepIndex = index;
            
            CurrentStageStep.Prepare();
            Data.GameEvents.GPonStageStepPrepare?.Invoke();
        }

        public void PerformCurrentStageStep()
        {
            CurrentStageStep.Perform();
            Data.GameEvents.GPonStageStepPerform?.Invoke();
        }

        private void OnStageStepCompleted()
        {
            CurrentStageStep.Complete();
            Data.GameEvents.GPonStageStepComplete?.Invoke();
        }
        #endregion
    }
}