using System;
using CandyMaster.Project.Scripts.Common.Classes;
using UnityEngine;

namespace CandyMaster.Project.Scripts.Objects.Common
{
    public class MeltingMesh : MonoBehaviour
    {
        [SerializeField] private SkinnedMeshRenderer mesh;
        [SerializeField] private Material material;
        [SerializeField] private ColorRange backgroundColor;
        [SerializeField] private ColorRange distortionColor;
        [SerializeField] private ColorRange distortionBackgroundColor;
        [SerializeField] private ColorRange bubblesColor;

        private Material _material;


        private void Awake()
        {
            _material = new Material(material);
            mesh.material = _material;
        }

        public void Melt(float percent)
        {
            if (percent < 0.5f)
            {
                var localPercent = percent * 2;
                
                var blendShapePercent = Mathf.Clamp(100 * localPercent, 0f, 100f);
                var materialMetallicPercent = Mathf.Clamp(0.5f + percent, 0f, 1f);
                
                SetBlendShapeState(blendShapePercent);
                SetMaterialColor("_background_color", backgroundColor, localPercent);
                SetMaterialColor("_distortion_color", distortionColor, localPercent);
                SetMaterialColor("_bubbles_color", bubblesColor, localPercent);
            }

            SetMaterialColor("_distortion_background_color", distortionBackgroundColor, percent);
        }

        private void SetBlendShapeState(float value)
        {
            mesh.SetBlendShapeWeight(0, value);
            mesh.SetBlendShapeWeight(1, value);
            mesh.SetBlendShapeWeight(2, value);
        }

        private void SetMaterialColor(string property, ColorRange range, float percent)
        {
            _material.SetColor(property, range.Lerp(percent));
        }
    }
}