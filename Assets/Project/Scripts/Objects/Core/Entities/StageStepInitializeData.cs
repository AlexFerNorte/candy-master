using System;
using CandyMaster.Project.Scripts.Data.Immutable;
using CandyMaster.Project.Scripts.Objects.Implementations.Entities.Camera;
using CandyMaster.Project.Scripts.Objects.Implementations.Entities.Stage;

namespace CandyMaster.Project.Scripts.Objects.Core.Entities
{
    public class StageStepInitializeData : InitializeData
    {
        public ActiveCamera ActiveCamera { get; }
        public StageEvents StageEvents { get; }

        
        public StageStepInitializeData
        (
            ActiveCamera activeCamera, StageEvents stageEvents
        )
        {
            ActiveCamera = activeCamera;
            StageEvents = stageEvents;
        }
    }
}