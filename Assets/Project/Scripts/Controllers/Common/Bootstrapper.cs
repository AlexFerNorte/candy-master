using CandyMasters.Project.Scripts.Common.Classes;
using CandyMasters.Project.Scripts.Controllers.Implementations.GameController;
using CandyMasters.Project.Scripts.Controllers.Implementations.InputController;
using CandyMasters.Project.Scripts.Controllers.Implementations.InstancesController;
using CandyMasters.Project.Scripts.Controllers.Implementations.StageController;
using CandyMasters.Project.Scripts.Controllers.Implementations.StageStepController;
using CandyMasters.Project.Scripts.Controllers.Implementations.UIController;
using UnityEngine;

namespace CandyMasters.Project.Scripts.Controllers.Common
{
    public class Bootstrapper : MonoBehaviour
    {
        #region Serialized
        [field: Header("Common")]
        [field: SerializeField] public Transform GameObjectsFolder { get; private set; }
        [field: SerializeField] public Transform UIFolder { get; private set; }

        [field: Header("Controllers")]
        [field: SerializeField] public GameController GameController { get; private set; }
        [field: SerializeField] public InputController InputController { get; private set; }
        [field: SerializeField] public InstancesController InstancesController { get; private set; }
        [field: SerializeField] public StageController StageController { get; private set; }
        [field: SerializeField] public StageStepController StageStepController { get; private set; }
        [field: SerializeField] public UIController UIController { get; private set; }

        [field: Header("Values")]
        [field: SerializeField] public float NoValues { get; private set; }
        [field: Header("Flags")]
        [field: SerializeField] public bool NoFlags { get; private set; }
        #endregion

        #region Current
        
        #endregion

        #region Special

        #endregion


        #region Start
        private void Awake()
        {
            InitializeGameData();

            InitializeControllers();

            Perform();
        }
        #endregion


        #region Common
        private void InitializeGameData()
        {
            /*JsonFileIOUtility.Initialize();
            JsonFileIOUtility.TryLoad(out var data).Ternary
            (
                () => Variables.GameData = data,
                () => Variables.GameData = new GameJsonData()
            );*/
        }

        private void InitializeControllers()
        {
            /*GameController.Initialize(new GameControllerInitializeData());
            InputController.Initialize(new InputControllerInitializeData());
            InstancesController.Initialize(new InstancesControllerInitializeData());
            StageController.Initialize(new StageControllerInitializeData());
            StageStepController.Initialize(new StageStepControllerInitializeData());
            UIController.Initialize(new UIControllerInitializeData());*/
        }

        private void Perform()
        {
            //GameController.PerformGame();
        }
        #endregion
    }
}