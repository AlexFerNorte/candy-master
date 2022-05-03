using System;
using CandyMaster.Project.Scripts.Controllers.Core;
using UnityEngine;

namespace CandyMaster.Project.Scripts.Controllers.Implementations.GameController
{
    [Serializable]
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
        public void PrepareGame()
        {
            InitializeData.StageController.PrepareStage(InitializeData.StageController.NextStageIndex);
        }

        public void PerformGame()
        {
            InitializeData.StageController.PerformCurrentStage();
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
