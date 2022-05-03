using CandyMasters.Project.Scripts.Common.Enums;

namespace CandyMasters.Project.Scripts.Objects.Core.Entities
{
    public class StageStep : Entity<StageStepInitializeData>
    {
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
            CurrentState = CompletionState.Prepared;
        }

        public void Perform()
        {
            CurrentState = CompletionState.Performed;
        }

        public void Complete()
        {
            CurrentState = CompletionState.Completed;
        }
        #endregion
    }
}