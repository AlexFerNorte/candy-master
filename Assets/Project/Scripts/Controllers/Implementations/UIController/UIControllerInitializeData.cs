using System;
using CandyMaster.Project.Scripts.Data;
using CandyMaster.Project.Scripts.Data.Immutable;
using CandyMaster.Project.Scripts.Objects.Implementations.Entities.Stage;
using CandyMaster.Project.Scripts.UI.Common;

namespace CandyMaster.Project.Scripts.Controllers.Implementations.UIController
{
    public class UIControllerInitializeData : InitializeData
    {
        public UIEvents UIEvents { get; }
        public StageEvents StageEvents { get; }


        public UIControllerInitializeData
        (
            UIEvents uiEvents,
            StageEvents stageEvents
        )
        {
            UIEvents = uiEvents;
            StageEvents = stageEvents;
        }
    }
}