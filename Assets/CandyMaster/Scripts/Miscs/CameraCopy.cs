using UnityEngine;

namespace CandyMaster.Scripts.Miscs
{
    [RequireComponent(typeof(Camera))]
    public class CameraCopy : MonoBehaviour
    {
        [SerializeField] private Camera reference;
        private Camera _camera;

        private void Awake()
        {
            _camera = GetComponent<Camera>();
        }

        private void LateUpdate()
        {
            _camera.fieldOfView = reference.fieldOfView;
            _camera.aspect = reference.aspect;
            
            var refCamera = reference.transform;
            var thisCamera = transform;
            thisCamera.position = refCamera.position;
            thisCamera.rotation = refCamera.rotation;
        }
    }
}