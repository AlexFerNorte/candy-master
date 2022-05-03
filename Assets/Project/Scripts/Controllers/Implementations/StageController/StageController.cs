using System;
using System.Collections.Generic;
using CandyMaster.Project.Scripts.Controllers.Core;
using CandyMaster.Project.Scripts.Data.Immutable;
using CandyMaster.Project.Scripts.Objects.Implementations.Entities.Stage;
using UnityEngine;

namespace CandyMaster.Project.Scripts.Controllers.Implementations.StageController
{
    [Serializable]
    public class StageController : Controller<StageControllerInitializeData>
    {
        #region Serialized
        [field: SerializeField] public List<Stage> Stages { get; set; }
        #endregion

        #region Current
        public int CurrentStageIndex { get; private set; }
        public int NextStageIndex => CurrentStageIndex + 1 >= Stages.Count ? 0 : CurrentStageIndex + 1;
        public Stage CurrentStage => Stages[CurrentStageIndex];
        #endregion


        public override void Initialize(StageControllerInitializeData initializeData)
        {
            base.Initialize(initializeData);

            foreach (var stage in Stages)
            {
                stage.Initialize(new StageInitializeData(initializeData.StageEvents));
            }
        }


        #region Stages
        public void PrepareStage(int index)
        {
            CurrentStageIndex = index;
            
            CurrentStage.Prepare();
        }

        public void PerformCurrentStage()
        {
            CurrentStage.Perform();
        }

        public void CompleteCurrentStage()
        {
            CurrentStage.Complete();
        }
        #endregion
    }
}