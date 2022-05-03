using CandyMasters.Project.Scripts.Data;

namespace CandyMasters.Project.Scripts.Controllers.Implementations.GameController
{
    public class GameControllerInitializeData : InitializeData
    {
        public InputController.InputController InputController { get; }
        public InstancesController.InstancesController InstancesController { get; }
        public StageController.StageController StageController { get; }
        public StageStepController.StageStepController StageStepController { get; }
        public UIController.UIController UIController { get; }


        public GameControllerInitializeData
        (
            InputController.InputController inputController,
            InstancesController.InstancesController instancesController,
            StageController.StageController stageController,
            StageStepController.StageStepController stageStepController,
            UIController.UIController uiController
        )
        {
            InputController = inputController;
            InstancesController = instancesController;
            StageController = stageController;
            StageStepController = stageStepController;
            UIController = uiController;
        }
    }
}