using System;
using CandyMaster.Project.Scripts.Controllers.Core;
using Screen = CandyMaster.Project.Scripts.UI.Core.Screen;

namespace CandyMaster.Project.Scripts.Controllers.Implementations.GameController
{
    /// <summary>
    /// Main game controller. Manages other controllers and signals from one controller to another.
    /// </summary>
    [Serializable]
    public class GameController : Controller<GameControllerInitializeData>
    {
        #region Initialization
        public override void Initialize(GameControllerInitializeData data)
        {
            base.Initialize(data);
            InitializeData.StageEvents.OnStagePrepared.AddListener(OnStagePrepared);
            InitializeData.StageEvents.OnStagePromoted.AddListener(OnStageStarted);
            InitializeData.StageEvents.OnStageCompleted.AddListener(OnStageCompleted);
            InitializeData.StageEvents.OnStageStepPrepared.AddListener(OnStageStepPrepared);
            InitializeData.StageEvents.OnStageStepPromoted.AddListener(OnStageStepStarted);
            InitializeData.StageEvents.OnStageStepPerforming.AddListener(OnStageStepPerforming);
            InitializeData.StageEvents.OnStageStepCompleted.AddListener(OnStageStepCompleted);
            
            InitializeData.UIEvents.OnRecipeAccepted.AddListener(OnRecipeAccepted);
            InitializeData.UIEvents.OnRecipeDeclined.AddListener(OnRecipeDeclined);
            InitializeData.UIEvents.OnStageContinueClicked.AddListener(OnStageContinueClicked);
        }
        #endregion


        
        #region Launch
        public void PrepareGame()
        {
            InitializeData.StageController.PrepareStage(InitializeData.StageController.NextStageIndex);
        }

        public void PerformGame()
        {
            InitializeData.StageController.StartCurrentStage();
        }
        #endregion

        
        
        #region InputEvents
        public void OnFingerDown() { }
        
        public void OnFingerHold(float xDelta) { }

        public void OnFingerUp() { }

        public void OnSwipe() { }
        #endregion
        
        

        #region StageControllerEvents
        private void OnStagePrepared()
        {
            
        }

        private void OnStageStarted()
        {
            
        }

        private void OnStageCompleted()
        {
            
        }
        
        private void OnStageStepPrepared(Screen screen)
        {
            InitializeData.UIController.ActivateScreen(screen);
        }

        private void OnStageStepStarted()
        {
            
        }

        private void OnStageStepPerforming(float percent)
        {

        }

        private void OnStageStepCompleted(Screen screen)
        {
            InitializeData.UIController.DeactivateScreen(screen);
        }
        #endregion



        #region UIControllerEvents
        private void OnRecipeAccepted()
        {
            
        }
        
        private void OnRecipeDeclined()
        {
            
        }
        
        private void OnStageContinueClicked()
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
