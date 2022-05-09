using CandyMaster.Project.Scripts.Events;
using CandyMaster.Project.Scripts.UI.Implementations.Bars;
using DG.Tweening;
using UnityEngine;
using Screen = CandyMaster.Project.Scripts.UI.Core.Screen;

namespace CandyMaster.Project.Scripts.UI.Implementations.Screens
{
    public class SyrupBoilingScreen : Screen
    {
        public TemperatureBar temperatureBar;


        public override void Initialize(StageEvents stageEvents)
        {
            base.Initialize(stageEvents);
            StageEvents.OnStageStepPerforming.AddListener(SetTemperatureBar);
        }

        public override void Appear()
        {
            base.Appear();
            temperatureBar.transform.localScale = Vector3.zero;
            temperatureBar.transform.DOScale(Vector3.one, transitionTime);
        }

        public override void Disappear()
        {
            base.Disappear();
            temperatureBar.transform.DOScale(Vector3.zero, transitionTime);
        }

        private void SetTemperatureBar(float percent)
        {
            temperatureBar.Fill(percent);
        }
    }
}