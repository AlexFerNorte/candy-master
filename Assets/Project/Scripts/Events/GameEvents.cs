using System;

namespace CandyMasters.Project.Scripts.Events
{
    public class GameEvents
    {
        #region Gameplay
        public Action GPonMainMenuRecipeConfirmed { get; set; }
        public Action GPonMainMenuRecipeDeclined { get; set; }

        public Action GPonStagePrepare { get; set; }
        public Action GPonStagePerform { get; set; }
        public Action GPonStageComplete { get; set; }
        public Action GPonStageExit { get; set; }

        public Action GPonStageStepPrepare { get; set; }
        public Action GPonStageStepPerform { get; set; }
        public Action GPonStageStepComplete { get; set; }
        #endregion
    }
}