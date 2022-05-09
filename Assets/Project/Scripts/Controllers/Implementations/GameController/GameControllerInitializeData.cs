using CandyMaster.Project.Scripts.Data;
using CandyMaster.Project.Scripts.Data.Immutable;
using CandyMaster.Project.Scripts.Events;
using CandyMaster.Project.Scripts.Objects.Implementations.Entities.Stage;

namespace CandyMaster.Project.Scripts.Controllers.Implementations.GameController
{
    public class GameControllerInitializeData : InitializeData
    {
        public InputController.InputController InputController { get; }
        public StageController.StageController StageController { get; }
        public UIController.UIController UIController { get; }
        public StageEvents StageEvents { get; }
        public UIEvents UIEvents { get; }


        public GameControllerInitializeData
        (
            InputController.InputController inputController,
            StageController.StageController stageController,
            UIController.UIController uiController,
            StageEvents stageEvents,
            UIEvents uiEvents
        )
        {
            InputController = inputController;
            StageController = stageController;
            UIController = uiController;
            StageEvents = stageEvents;
            UIEvents = uiEvents;
        }
    }
}