using System;
using CandyMasters.Project.Scripts.Controllers.Core;

namespace CandyMasters.Project.Scripts.Controllers.Implementations.GameController
{
    public class GameController : Controller<GameControllerInitializeData>
    {
        public override void Initialize(GameControllerInitializeData data)
        {
            base.Initialize(data);
        }

        #region Input
        public void OnFingerDown()
        {

        }
        
        public void OnFingerHold(float xDelta)
        {

            //Variables.PlayerInstance.TryLocalMoveX(xDelta, out var newX);
            //CommonObjects.ActiveCameraInstance.Move(newX);
        }

        public void OnFingerUp()
        {

        }

        public void OnSwipe()
        {
            /*var state = NpcStickPointsState.First;
            switch (Variables.PlayerInstance.Variables.NpcCrowd.Variables.StickStructure.CurrentState)
            {
                case NpcStickPointsState.First:
                    state = NpcStickPointsState.Second;
                    break;
                case NpcStickPointsState.Second:
                    state = NpcStickPointsState.First;
                    break;
            }
            Variables.PlayerInstance.Variables.NpcCrowd.TryRegroup(state);*/
        }
        #endregion


        #region Launch
        public void PrepareMenu()
        {

        }
        
        public void PerformMenu()
        {

        }
        
        public void PrepareGame()
        {
            Data.StageController.PrepareStage(Data.StageController.NextStageIndex);
            Data.StageStepController.PrepareStageStep(Data.StageStepController.NextStageStepIndex);
        }

        public void PerformGame()
        {
            Data.StageController.PerformCurrentStage();
            Data.StageStepController.PerformCurrentStageStep();
        }
        #endregion


        #region GameEvents
        private void OnMainMenuRecipeConfirmed()
        {
            
        }
        
        private void OnMainMenuRecipeDeclined()
        {
            
        }
        
        private void OnStagePrepare()
        {
            
        }
        
        private void OnStagePerform()
        {
            
        }
        
        private void OnStageComplete()
        {
            
        }
        
        private void OnStageExit()
        {
            
        }
        
        private void OnStageStepPrepare()
        {
            
        }
        
        private void OnStageStepPerform()
        {
            
        }
        
        private void OnStageStepComplete()
        {
            
        }
        #endregion


        #region Reset
        private void ResetControllers()
        {
            
        }

        private void ResetInstancesInstructions()
        {

        }

        private void ResetInstancesVariables()
        {

        }
        #endregion
    }
}
