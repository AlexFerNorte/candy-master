using CandyMasters.Project.Scripts.Common.Enums;
using CandyMasters.Project.Scripts.Objects.Core;
using CandyMasters.Project.Scripts.Objects.Core.Entities;

namespace CandyMasters.Project.Scripts.Objects.Implementations.Entities.Stage
{
    public class Stage : Entity<StageInitializeData>
    {
        public CompletionState CurrentState { get; set; }
        public StageStep CurrentStageStep { get; set; }
        
        
        protected override void InitializeVariables(StageInitializeData initializeData)
        {

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

        public void Appear() => gameObject.SetActive(true);
        
        public void Disappear() => gameObject.SetActive(false);

        public void Prepare()
        {
            Appear();
            CurrentState = CompletionState.Prepared;
        }

        public void Perform()
        {
            CurrentState = CompletionState.Performed;
        }

        public void Complete()
        {
            Disappear();
            CurrentState = CompletionState.Completed;
        }
    }
}