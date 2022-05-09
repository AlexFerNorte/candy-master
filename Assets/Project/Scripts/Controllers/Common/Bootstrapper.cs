using CandyMaster.Project.Scripts.Controllers.Implementations.GameController;
using CandyMaster.Project.Scripts.Controllers.Implementations.InputController;
using CandyMaster.Project.Scripts.Controllers.Implementations.LobbyController;
using CandyMaster.Project.Scripts.Controllers.Implementations.StageController;
using CandyMaster.Project.Scripts.Controllers.Implementations.UIController;
using CandyMaster.Project.Scripts.Events;
using CandyMaster.Project.Scripts.Objects.Implementations.Entities.Stage;
using CandyMaster.Project.Scripts.UI.Common;
using UnityEngine;

namespace CandyMaster.Project.Scripts.Controllers.Common
{
    public class Bootstrapper : MonoBehaviour
    {
        #region Serialized
        [field: Header("Controllers")]
        [field: SerializeField] public InputController InputController { get; private set; }
        [field: SerializeField] public LobbyController LobbyController { get; private set; }
        [field: SerializeField] public StageController StageController { get; private set; }
        [field: SerializeField] public UIController UIController { get; private set; }
        [field: SerializeField] public GameController GameController { get; private set; }
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
            
            LobbyController.Initialize(new LobbyControllerInitializeData());
            
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
                stageEvents,
                uiEvents
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