using System;
using System.Threading.Tasks;
using CandyMaster.Scripts.Gameplay.Interfaces;
using CandyMaster.Scripts.Gameplay.Utils;
using Cinemachine;
using UnityEngine;

namespace CandyMaster.Scripts.Gameplay
{
    public abstract class AbstractStep : MonoBehaviour, IDisposable
    {
        [SerializeField] private string title;
        [SerializeField] private float disposeDelay = 1f;
        [SerializeField] private bool disposed;

        private CinemachineVirtualCamera _virtualCamera;

        private ITutorialHand _tutorialHand;
        
        public CinemachineVirtualCamera Camera => _virtualCamera;

        public string StepTitle => title;

        public abstract void Init();

        public abstract Task ExecuteStep();

        protected abstract void DisposeSelf();

        protected ITutorialHand TutorialHand => _tutorialHand;

        public async void Dispose()
        {
            if (disposed) return;

            disposed = true;
            await Task.Delay(TimeSpan.FromSeconds(disposeDelay));
            DisposeSelf();
        }

        private void Awake()
        {
            _virtualCamera = GetComponentInChildren<CinemachineVirtualCamera>() ?? throw new NullReferenceException();
            _tutorialHand = this.FindOrException<ITutorialHand>();
            disposed = _virtualCamera.enabled = false;
        }
    }
}