using CandyMasters.Project.Scripts.Objects.Core;
using UnityEngine;

namespace CandyMasters.Project.Scripts.Objects.Implementations.Entities.Camera
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
        
        public void Rotate(Quaternion rotation) => transform.rotation = rotation;
        
        public void Transformize(Vector3 position, Quaternion rotation)
        {
            Locate(position);
            Rotate(rotation);
        }
        
        public Vector3 GetScreenPosition(Vector3 worldPosition) => Camera.WorldToScreenPoint(worldPosition);
        #endregion
    }
}