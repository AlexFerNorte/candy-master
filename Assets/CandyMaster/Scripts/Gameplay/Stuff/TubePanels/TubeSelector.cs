using System;
using UnityEngine;

namespace CandyMaster.Scripts.Gameplay.Stuff.TubePanels
{
    [ExecuteAlways]
    public class TubeSelector : MonoBehaviour
    {
        [SerializeField] private Color color;
        [SerializeField] private MeshRenderer meshRenderer;

        private MaterialPropertyBlock _materialPropertyBlock;
        private static readonly int Color1 = Shader.PropertyToID("_Color");
        private static readonly int Color2 = Shader.PropertyToID("_BaseColor");

        public event Action<TubeSelector> Clicked;

        public Color Color
        {
            get => color;
            set
            {
                color = value;
                _materialPropertyBlock.SetColor(Color1, value);
                _materialPropertyBlock.SetColor(Color2, value);
                meshRenderer.SetPropertyBlock(_materialPropertyBlock, 0);
            }
        }

        public void OnMouseUp() => Clicked?.Invoke(this);
        

        private void Start()
        {
            _materialPropertyBlock = new MaterialPropertyBlock();
            Color = color;
        }

#if UNITY_EDITOR
        private void Update()
        {
            _materialPropertyBlock ??= new MaterialPropertyBlock();
            Color = color;
        }
#endif
    }
}