using CandyMaster.Project.Scripts.Common.Enums;
using UnityEngine;
using Screen = CandyMaster.Project.Scripts.UI.Core.Screen;

namespace CandyMaster.Project.Scripts.Objects.Core.Entities
{
    public abstract class StageStep : Entity<StageStepInitializeData>
    {
        [field: SerializeField] private Transform CameraViewPoint { get; set; }
        [field: SerializeField] private float CameraSmoothTransitionTime { get; set; }
        [field: SerializeField] private Screen Screen { get; set; }

        public CompletionState CurrentState { get; set; }


        #region Initialization
        protected override void InitializeVariables(StageStepInitializeData initializeData)
        {

        }

        public override void ResetVariables()
        {

        }

        protected override void InitializeInstructions(StageStepInitializeData initializeData)
        {

        }

        public override void ResetInstructions()
        {

        }
        #endregion


        #region Common
        public void Prepare()
        {
            CurrentState = CompletionState.Preparing;
            OnPrepare();
        }

        public void Perform()
        {
            OnPerform();
            CurrentState = CompletionState.Performing;
        }

        public virtual void Complete()
        {
            OnComplete();
        }
        #endregion


        #region Extends
        protected virtual void OnPrepare()
        {
            InitializeData.ActiveCamera.SmoothLocate
            (
                CameraViewPoint.position, 
                CameraSmoothTransitionTime, 
                () => InitializeData.StageEvents.OnStageStepPrepared.Invoke(Screen)
            );
            
            InitializeData.ActiveCamera.SmoothRotate
            (
                CameraViewPoint.rotation.eulerAngles, 
                CameraSmoothTransitionTime
            );
        }
        
        protected virtual void OnPerform()
        {
            InitializeData.StageEvents.OnStageStepPerforming.Invoke();
        }
        
        private void OnComplete()
        {
            CurrentState = CompletionState.Completed;
            InitializeData.StageEvents.OnStageStepCompleted.Invoke();
        }
        #endregion
    }
}