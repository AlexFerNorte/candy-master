using System;
using System.Threading.Tasks;
using CandyMaster.Scripts.Coloring.PaintTubeSystem;
using UnityEngine;

namespace CandyMaster.Scripts.Gameplay.Coloring.PaintTubeSystem
{
    public class PaintTubeManager : MonoBehaviour
    {
        [Header("Paint Point")] [SerializeField]
        private SpriteRenderer paintPoint;

        [SerializeField] private Vector3 paintPointOffset;
        [SerializeField] private Color onColor;
        [SerializeField] private Color offColor;


        [Header("Tube")] [SerializeField] private PaintTube paintTube;
        [SerializeField] private Vector3 offset;

        [SerializeField] private float rayDistance = 50f;

        [SerializeField] private float delay = 0.5f;
        [SerializeField] private LayerMask paintPlainMask;
        [SerializeField] private bool emitOnStart;

        private bool _emitting;

        private Camera _camera;

        private async void Start()
        {
            _camera = Camera.main;

            _emitting = emitOnStart;
            paintPoint.color = offColor;
            while (this)
            {
                if (_emitting)
                    paintTube.Emit();
                await Task.Delay(TimeSpan.FromSeconds(delay));
            }
        }

        private void OnDestroy()
        {
            _emitting = false;
        }

        private void Update()
        {
            if (Physics.Raycast(
                    _camera.ScreenPointToRay(Input.mousePosition),
                    out var info,
                    rayDistance,
                    paintPlainMask.value))
            {
                paintTube.transform.position = info.point + offset;
                paintPoint.transform.position = info.point + paintPointOffset;

                var allow = info.collider.CompareTag("PaintAllow");

                paintPoint.color = allow ? onColor : offColor;
                _emitting = allow;
            }
        }
    }
}