using CandyMaster.Project.Scripts.Objects.Common;
using CandyMaster.Project.Scripts.Objects.Core.Entities;
using Tiq.Plugins.Tiq.Common;
using Tiq.Plugins.Tiq.Implementations.Instructions;
using UnityEngine;

namespace CandyMaster.Project.Scripts.Objects.Implementations.Entities.StageSteps
{
    public class SyrupBoilingStageStep : StageStep
    {
        [SerializeField] private MeltingMesh sugar;
        [SerializeField] private CookerHandle cookerHandle;
        [SerializeField] private float maxTemperature;
        [SerializeField] private float temperatureIncreaseSpeed;

        private float _currentTemperaturePercent;
        private Tick _boilInstruction;


        protected override void InitializeVariables(StageStepInitializeData initializeData)
        {
            base.InitializeVariables(initializeData);
            cookerHandle.Initialize(Promote, Complete);
        }

        protected override void InitializeInstructions(StageStepInitializeData initializeData)
        {
            base.InitializeInstructions(initializeData);
            
            _boilInstruction = new Tick
            (
                () => Time.deltaTime * temperatureIncreaseSpeed,
                timer => _currentTemperaturePercent * maxTemperature < maxTemperature,
                timer =>
                {
                    _currentTemperaturePercent = timer;
                    sugar.Melt(_currentTemperaturePercent);
                    OnPerforming(_currentTemperaturePercent);
                }
            );
        }

        protected override void OnPromoted()
        {
            base.OnPromoted();
            TiqStarter.Instance.Tiq.Start(_boilInstruction);
        }

        /*
         * Ждать нажатия рычага
         * По нажатию:
         * Поднимать температуру
         * Плавить сахар
         * Менять окраску сиропа
         */
    }
}