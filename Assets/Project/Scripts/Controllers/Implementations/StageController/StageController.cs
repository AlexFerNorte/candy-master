using System;
using System.Collections.Generic;
using CandyMaster.Project.Scripts.Controllers.Core;
using CandyMaster.Project.Scripts.Objects.Core.Entities;
using CandyMaster.Project.Scripts.Objects.Implementations.Entities.Camera;
using CandyMaster.Project.Scripts.Objects.Implementations.Entities.Stage;
using UnityEngine;
using Screen = CandyMaster.Project.Scripts.UI.Core.Screen;

namespace CandyMaster.Project.Scripts.Controllers.Implementations.StageController
{
    [Serializable]
    public class StageController : Controller<StageControllerInitializeData>
    {
        #region Serialized
        [field: SerializeField] public List<Stage> Stages { get; set; }
        [field: SerializeField] public List<StageStep> StageSteps { get; set; }
        [field: SerializeField] private ActiveCamera ActiveCamera { get; set; }
        #endregion

        #region Current
        public int CurrentStageIndex { get; private set; }
        public int NextStageIndex => CurrentStageIndex + 1 >= Stages.Count ? 0 : CurrentStageIndex + 1;
        public Stage CurrentStage => Stages[CurrentStageIndex];
        
        
        private int CurrentStageStepIndex { get; set; }
        private int NextStageStepIndex => CurrentStageStepIndex + 1 >= StageSteps.Count ? 0 : CurrentStageStepIndex + 1;
        private StageStep CurrentStageStep => StageSteps[CurrentStageStepIndex];
        #endregion


        public override void Initialize(StageControllerInitializeData initializeData)
        {
            base.Initialize(initializeData);
            
            InitializeData.StageEvents.OnStageStepCompleted.AddListener(OnStageStepCompleted);

            foreach (var stage in Stages)
            {
                stage.Initialize(new StageInitializeData
                (
                    InitializeData.StageEvents
                ));
            }
            
            foreach (var step in StageSteps)
            {
                step.Initialize(new StageStepInitializeData
                (
                    ActiveCamera,
                    InitializeData.StageEvents
                ));
            }
        }


        #region Stages
        public void PrepareStage(int index)
        {
            CurrentStageIndex = index;

            PrepareStageStep(0);
            CurrentStage.Prepare();
        }

        public void StartCurrentStage()
        {
            CurrentStage.Promote();
        }

        public void CompleteCurrentStage()
        {
            CurrentStage.Complete();
        }
        #endregion
        
        
        
        #region StageSteps
        private void PrepareStageStep(int index)
        {
            CurrentStageStepIndex = index;

            CurrentStageStep.Prepare();
        }

        private void StartCurrentStageStep(Screen screen)
        {

        }

        private void OnStageStepCompleted(Screen screen)
        {
            if (CurrentStageStepIndex >= StageSteps.Count)
            {
                CompleteCurrentStage();
                return;
            }
            
            PrepareStageStep(NextStageStepIndex);
        }
        #endregion
    }
}