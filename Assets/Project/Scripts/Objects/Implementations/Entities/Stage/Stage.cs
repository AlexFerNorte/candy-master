using CandyMaster.Project.Scripts.Common.Enums;
using CandyMaster.Project.Scripts.Objects.Core;

namespace CandyMaster.Project.Scripts.Objects.Implementations.Entities.Stage
{
    public class Stage : Entity<StageInitializeData>
    {
        private CompletionState CurrentState { get; set; }



        #region Initialization
        protected override void InitializeVariables(StageInitializeData initializeData) { }

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
            InitializeData.StageEvents.OnStagePrepared.Invoke();
        }

        public void Promote()
        {
            CurrentState = CompletionState.Performing;
            InitializeData.StageEvents.OnStagePromoted.Invoke();
        }

        public void Complete()
        {
            CurrentState = CompletionState.Completed;
            InitializeData.StageEvents.OnStageCompleted.Invoke();
        }
        #endregion
    }
}