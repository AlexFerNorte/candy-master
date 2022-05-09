using System;
using CandyMaster.Project.Scripts.Data;
using CandyMaster.Project.Scripts.Data.Immutable;
using CandyMaster.Project.Scripts.Events;
using CandyMaster.Project.Scripts.Objects.Implementations.Entities.Stage;
using CandyMaster.Project.Scripts.UI.Common;

namespace CandyMaster.Project.Scripts.Controllers.Implementations.StageController
{
    public class StageControllerInitializeData : InitializeData
    {
        public StageEvents StageEvents { get; }
        
        public UIEvents UIEvents { get; }

        public StageControllerInitializeData
        (
            StageEvents stageEvents, UIEvents uiEvents
        )
        {
            StageEvents = stageEvents;
            UIEvents = uiEvents;
        }
    }
}