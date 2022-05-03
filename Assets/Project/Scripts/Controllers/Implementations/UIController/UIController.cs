using System;
using CandyMasters.Project.Scripts.Controllers.Core;
using CandyMasters.Project.Scripts.Objects.Core.Entities;
using CandyMasters.Project.Scripts.Objects.Implementations.Entities.Stage;
using CandyMasters.Project.Scripts.UI.Implementations.Screens.StageComplete;
using CandyMasters.Project.Scripts.UI.Implementations.Screens.StageFailure;
using UnityEngine;

namespace CandyMasters.Project.Scripts.Controllers.Implementations.UIController
{
    public class UIController : Controller<UIControllerInitializeData>
    {
        #region Serialized
        [field: SerializeField] private StageCompleteScreen StageCompleteScreen { get; set; }
        [field: SerializeField] private StageFailureScreen StageFailureScreen { get; set; }
        #endregion

        #region Current
        [field: SerializeField] private Stage CurrentStage { get; set; }
        [field: SerializeField] private StageStep CurrentStageStep { get; set; }
        #endregion


        /*public override void Initialize(UIControllerInitializeData data)
        {
            base.Initialize(data);
            var player = CommonObjects.PlayerInstance;
            StageCompleteScreen.Initialize(new StageCompleteScreenInitializeData("", Data.OnContinueClicked));
            StageFailureScreen.Initialize(new StageFailureScreenInitializeData("", Data.OnReplayClicked));
            StaticTooltipManager.Initialize(Data.StageController);
        }
        
        public void OnPlay()
        {
            CommonObjects.PlayerInstance.Variables.NpcCrowd.Events.OnCrowdStateChanged = SetCurrentCrowdState;
            CrowdStateMonitor.Show();
            SetCurrentCrowdState(CommonObjects.PlayerInstance.Variables.NpcCrowd.Variables.StickStructure.CurrentState);
            StartRedrawMonitors();
        }

        public void OnStop()
        {
            CrowdStateMonitor.Hide();
            StopRedrawMonitors();
        }

        public void ShowStageCompleteScreen() => StageCompleteScreen.Appear();

        public void HideStageCompleteScreen() => StageCompleteScreen.Disappear();

        public void ShowStageFailureScreen() => StageFailureScreen.Appear();

        public void HideStageFailureScreen() => StageFailureScreen.Disappear();

        public void ShowViewSelectionScreen() => ViewSelectionScreen.Appear();

        public void ShowSettingsScreen() => SettingsScreen.Appear();

        public void HideSettingsScreen() => SettingsScreen.Disappear();
        
        public void ShowHoldToMoveTooltip() => StaticTooltipManager.ShowTooltip(StaticTooltipType.OnStagePlay);
        
        public void SetCurrentCrowdState(NpcStickPointsState state) => CrowdStateMonitor.SetState(state);

        public void StartRedrawMonitors()
        {

        }

        public void StopRedrawMonitors()
        {

        }

        public void Reset()
        {

        }*/
    }
}