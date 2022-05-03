using System;
using CandyMaster.Scripts.Gameplay.Interfaces;
using CandyMaster.Scripts.Gameplay.Utils;
using CandyMaster.Scripts.Interfaces;
using CandyMaster.Scripts.Interfaces.UI;
using UnityEngine;

namespace CandyMaster.Scripts.Gameplay
{
    public class Pipeline : MonoBehaviour
    {
        [SerializeField] private AbstractStep[] steps;


        private ICameraManager _cameraManager;
        private IStepBanner _stepBanner;
        private IStepBar _stepBar;

        private void Awake()
        {
            _cameraManager = this.FindThe<ICameraManager>() ?? throw new Exception();
            _stepBanner = this.FindThe<IStepBanner>() ?? throw new Exception();
            _stepBar = this.FindOrException<IStepBar>();
        }

        private async void Start()
        {
            _stepBar.Init(steps.Length);
            foreach (var step in steps) step.Init();
            foreach (var step in steps)
            {
                await _cameraManager.GoToStep(step);

                _stepBar.NextStetStarted();
                _stepBanner.ShowStep(step);
                await step.ExecuteStep();
                _stepBar.NextStepFinished();
                step.Dispose();
            }
        }
    }
}