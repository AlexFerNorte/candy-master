using System;
using CandyMaster.Project.Scripts.Data;
using CandyMaster.Project.Scripts.Data.Immutable;
using CandyMaster.Project.Scripts.Events;

namespace CandyMaster.Project.Scripts.Objects.Implementations.Entities.Stage
{
    public class StageInitializeData : InitializeData
    {
        public StageEvents StageEvents { get; }

        public StageInitializeData(StageEvents stageEvents)
        {
            StageEvents = stageEvents;
        }
    }
}