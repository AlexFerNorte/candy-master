using CandyMaster.Project.Scripts.Controllers.Implementations.GameController;
using CandyMaster.Project.Scripts.Controllers.Implementations.InputController;
using CandyMaster.Project.Scripts.Controllers.Implementations.StageController;
using CandyMaster.Project.Scripts.Controllers.Implementations.UIController;
using CandyMaster.Project.Scripts.Objects.Implementations.Entities.Stage;
using CandyMaster.Project.Scripts.UI.Common;
using UnityEngine;

namespace CandyMaster.Project.Scripts.Controllers.Common
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
        [field: SerializeField] public StageController StageController { get; private set; }
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
            var stageEvents = new StageEvents();
            var uiEvents = new UIEvents();
            
            InputController.Initialize(new InputControllerInitializeData());
            
            StageController.Initialize(new StageControllerInitializeData
            (
                stageEvents,
                uiEvents
            ));
            
            UIController.Initialize(new UIControllerInitializeData
            (
                uiEvents,
                stageEvents
            ));
            
            GameController.Initialize(new GameControllerInitializeData
            (
                InputController,
                StageController,
                UIController,
                stageEvents
            ));
        }

        private void Perform()
        {
            GameController.PrepareGame();
            GameController.PerformGame();
        }
        #endregion
    }
}