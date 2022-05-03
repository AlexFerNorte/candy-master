using CandyMasters.Project.Scripts.Data;
using CandyMasters.Project.Scripts.Events;

namespace CandyMasters.Project.Scripts.Controllers.Implementations.StageController
{
    public class StageControllerInitializeData : InitializeData
    {
        public GameEvents GameEvents { get; }


        public StageControllerInitializeData(GameEvents gameEvents)
        {
            GameEvents = gameEvents;
        }
    }
}