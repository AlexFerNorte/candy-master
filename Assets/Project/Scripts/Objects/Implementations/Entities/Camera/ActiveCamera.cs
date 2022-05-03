using System;
using CandyMaster.Project.Scripts.Objects.Core;
using DG.Tweening;
using UnityEngine;

namespace CandyMaster.Project.Scripts.Objects.Implementations.Entities.Camera
{
    public class ActiveCamera : Entity<ActiveCameraInitializeData>
    {
        #region Serialized
        [field: SerializeField] public UnityEngine.Camera Camera { get; private set; }
        [field: SerializeField] public float DragOffset { get; private set; }
        
        [field: SerializeField] public float OffsetDuration { get; private set; }
        #endregion
        
        #region Current
        public Transform CurrentStickPoint { get; set; }
        public bool CurrentIsSticking { get; set; }
        public bool CurrentYStickLocked { get; set; }
        #endregion

        #region Special
        public int ScreenCenter => Camera.pixelWidth / 2;
        #endregion


        #region Initialization
        public override void Initialize(ActiveCameraInitializeData initializeData)
        {
            base.Initialize(initializeData);
            
            InitializeVariables(initializeData);
            InitializeInstructions(initializeData);
        }

        protected override void InitializeVariables(ActiveCameraInitializeData initializeData) { }

        public override void ResetVariables() { }
        protected override void InitializeInstructions(ActiveCameraInitializeData initializeData) { }

        public override void ResetInstructions() { }
        #endregion


        #region Common
        public void Locate(Vector3 position) => transform.position = position;
        
        public void SmoothLocate(Vector3 position, float time, Action onComplete = null)
        {
            transform.DOMove(position, time).OnComplete(() => onComplete?.Invoke());
        }

        public void SmoothRotate(Vector3 rotation, float time, Action onComplete = null)
        {
            transform.DORotate(rotation, time).OnComplete(() => onComplete?.Invoke());
        }

        public void Rotate(Quaternion rotation) => transform.rotation = rotation;

        public Vector3 GetScreenPosition(Vector3 worldPosition) => Camera.WorldToScreenPoint(worldPosition);
        #endregion
    }
}