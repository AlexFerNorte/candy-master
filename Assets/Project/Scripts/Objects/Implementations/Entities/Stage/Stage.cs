using System.Collections.Generic;
using CandyMaster.Project.Scripts.Common.Enums;
using CandyMaster.Project.Scripts.Objects.Core;
using CandyMaster.Project.Scripts.Objects.Core.Entities;
using CandyMaster.Project.Scripts.Objects.Implementations.Entities.Camera;
using UnityEngine;

namespace CandyMaster.Project.Scripts.Objects.Implementations.Entities.Stage
{
    public class Stage : Entity<StageInitializeData>
    {
        [field: SerializeField] private List<StageStep> StageSteps { get; set; }
        [field: SerializeField] private ActiveCamera ActiveCamera { get; set; }

        private CompletionState CurrentState { get; set; }
        private int CurrentStageStepIndex { get; set; }
        private int NextStageStepIndex => CurrentStageStepIndex + 1 >= StageSteps.Count ? 0 : CurrentStageStepIndex + 1;
        private StageStep CurrentStageStep => StageSteps[CurrentStageStepIndex];
        


        #region Initialization
        protected override void InitializeVariables(StageInitializeData initializeData)
        {
            InitializeData.StageEvents.OnStageStepPrepared.AddListener((screen) => PerformCurrentStageStep());
            InitializeData.StageEvents.OnStageStepCompleted.AddListener(OnStageStepCompleted);
            
            foreach (var step in StageSteps)
            {
                step.Initialize(new StageStepInitializeData
                (
                    ActiveCamera,
                    InitializeData.StageEvents
                ));
            }
        }

        public override void ResetVariables()
        {

        }

        protected override void InitializeInstructions(StageInitializeData initializeData)
        {

        }

        public override void ResetInstructions()
        {

        }
        #endregion


        
        #region Inherent
        public void Prepare()
        {
            CurrentState = CompletionState.Preparing;
            PrepareStageStep(0);
            InitializeData.StageEvents.OnStagePrepared.Invoke();
        }

        public void Perform()
        {
            PerformCurrentStageStep();
            CurrentState = CompletionState.Performing;
            InitializeData.StageEvents.OnStagePerforming.Invoke();
        }

        public void Complete()
        {
            CurrentState = CompletionState.Completed;
            InitializeData.StageEvents.OnStageCompleted.Invoke();
        }
        #endregion

        
        
        #region StageSteps
        private void PrepareStageStep(int index)
        {
            CurrentStageStepIndex = index;

            CurrentStageStep.Prepare();
        }

        private void PerformCurrentStageStep()
        {
            CurrentStageStep.Perform();
        }

        private void OnStageStepCompleted()
        {
            if (CurrentStageStepIndex >= StageSteps.Count)
            {
                Complete();
                return;
            }
            
            PrepareStageStep(NextStageStepIndex);
        }
        #endregion
    }
}