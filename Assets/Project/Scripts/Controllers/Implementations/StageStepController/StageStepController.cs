using System;
using System.Collections.Generic;
using CandyMasters.Project.Scripts.Controllers.Core;
using CandyMasters.Project.Scripts.Objects.Core.Entities;
using UnityEngine;

namespace CandyMasters.Project.Scripts.Controllers.Implementations.StageStepController
{
    [Serializable]
    public class StageStepController : Controller<StageStepControllerInitializeData>
    {
        #region Serialized
        [field: SerializeField] public List<StageStep> StageSteps { get; set; }
        #endregion

        #region Current
        public int CurrentStageStepIndex { get; private set; }
        public int NextStageStepIndex => CurrentStageStepIndex + 1 >= StageSteps.Count ? 0 : CurrentStageStepIndex + 1;
        public StageStep CurrentStageStep => StageSteps[CurrentStageStepIndex];
        #endregion
        
                
        public void PrepareStageStep(int index)
        {
            CurrentStageStepIndex = index;
            
            CurrentStageStep.Prepare();
        }

        public void PerformCurrentStageStep()
        {
            CurrentStageStep.Perform();
        }

        public void CompleteCurrentStageStep()
        {
            CurrentStageStep.Complete();
        }
    }
}