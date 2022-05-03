using System;
using System.Threading.Tasks;
using CandyMaster.Scripts.Gameplay.Interfaces;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

namespace CandyMaster.Scripts.Gameplay
{
    public class CameraManager : MonoBehaviour, ICameraManager
    {
        [SerializeField] private float moveDuration;
        [SerializeField] private Ease moveEase;
        [SerializeField] private CinemachineVirtualCamera currentCamera;

        private void Start() => currentCamera.enabled = true;

        public async Task GoToStep(AbstractStep step)
        {
            currentCamera.enabled = false;
            currentCamera = step.Camera;
            
            // ReSharper disable once Unity.InefficientPropertyAccess
            currentCamera.enabled = true;
            await Task.Delay(TimeSpan.FromSeconds(moveDuration));
        }
    }
}