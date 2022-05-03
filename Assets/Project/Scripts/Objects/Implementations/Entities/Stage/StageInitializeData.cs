using CandyMasters.Project.Scripts.Data;
using CandyMasters.Project.Scripts.Events;

namespace CandyMasters.Project.Scripts.Objects.Implementations.Entities.Stage
{
    public class StageInitializeData : InitializeData
    {
        public GameEvents GameEvents { get; }


        public StageInitializeData(GameEvents gameEvents)
        {
            GameEvents = gameEvents;
        }
    }
}