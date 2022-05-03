using System;
using CandyMaster.Scripts.Gameplay.Interfaces.Coloring;
using CandyMaster.Scripts.Interfaces;
using UnityEngine;

namespace CandyMaster.Scripts.Coloring.PaintTubeSystem
{
    public class PaintTear : MonoBehaviour
    {
        public static event Action<PaintTear> OnDone;
        
        [SerializeField] private new MeshRenderer renderer;
        private static readonly int ColorID = Shader.PropertyToID("_BaseColor");
        private Color _color;

        public void SetColor(in Color color)
        {
            _color = color;
            var property = new MaterialPropertyBlock();
            property.SetColor(ColorID, color);
            renderer.SetPropertyBlock(property);
        }

        public void Done() => OnDone?.Invoke(this);
        
        private void OnCollisionEnter(Collision collision)
        {
            print($"Tear collide with {collision.transform.name}");
            if (collision.collider.TryGetComponent(out IPaintableBrick paintableBrick))
            {
                print($"Has {paintableBrick.GetType().Name}");
                paintableBrick.ApplyPaintAtWorldPosition(transform.position, _color);
            }

            Done();
        }
    }
}