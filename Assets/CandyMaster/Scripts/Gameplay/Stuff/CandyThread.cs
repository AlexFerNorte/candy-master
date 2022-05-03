using CandyMaster.Scripts.Gameplay.Interfaces;
using CandyMaster.Scripts.Interfaces;
using UnityEngine;

namespace CandyMaster.Scripts.Stuff
{
    public class CandyThread : MonoBehaviour, ICandyThread
    {
        [SerializeField] private Renderer strings;

        private MaterialPropertyBlock _materialPropertyBlock;
        
        private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");

        private void Awake()
        {
            _materialPropertyBlock = new MaterialPropertyBlock();
        }

        public void SetColors(Color[] colors)
        {
            for (var i = 0; i < colors.Length; i++)
            {
                _materialPropertyBlock.SetColor(BaseColor, colors[i]);
                strings.SetPropertyBlock(_materialPropertyBlock, i);
            }
        }
    }
}