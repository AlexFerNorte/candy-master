using System;
using CandyMasters.Project.Scripts.Data;
using CandyMasters.Project.Scripts.Objects.Implementations.Entities.Stage;

namespace CandyMasters.Project.Scripts.Controllers.Implementations.UIController
{
    public class UIControllerInitializeData : InitializeData
    {
        public Stage Stage { get; }
        public Action OnReplayClicked { get; }
        public Action OnContinueClicked { get; }


        public UIControllerInitializeData
        (
            Stage stage, Action onReplayClicked, Action onContinueClicked
        )
        {
            Stage = stage;
            OnReplayClicked = onReplayClicked;
            OnContinueClicked = onContinueClicked;
        }
    }
}