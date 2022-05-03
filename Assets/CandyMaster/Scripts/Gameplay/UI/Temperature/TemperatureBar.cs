using System;
using CandyMaster.Scripts.Gameplay.Component;
using CandyMaster.Scripts.Interfaces;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CandyMaster.Scripts.UI.Temperature
{
    [RequireComponent(typeof(ShowableUIComponent))]
    public class TemperatureBar : MonoBehaviour, ITemperatureBar
    {
        [SerializeField, Range(0, 1)] private float value;

        [Header("Arrow")] [SerializeField] private Image arrowCore;
        [SerializeField] private RectTransform arrowParent;
        [SerializeField] private float arrowOffset = 0;
        [SerializeField] private float arrowAmplitude = 100;
        [SerializeField] private Gradient arrowGradientMap;

        [Header("Filling")] [SerializeField] private Image filling;
        [Header("Points")] [SerializeField] private TemperaturePoint[] points;

        private ShowableUIComponent _showableUIComponent;

        public float Value
        {
            get => value;
            set
            {
                this.value = Mathf.Clamp(value, 0, 1);
                UpdateState();
            }
        }

        public bool Show
        {
            get => _showableUIComponent.Show;
            set => _showableUIComponent.Show = value;
        }


        public void Dispose()
        {
            Destroy(gameObject);
        }

        private void Start()
        {
            _showableUIComponent = GetComponent<ShowableUIComponent>();
            value = 0;
            UpdateState();
        }

        private void UpdateState()
        {
            arrowParent.rotation = Quaternion.Euler(0, 0, arrowOffset + (arrowAmplitude * value));
            arrowCore.color = arrowGradientMap.Evaluate(value);
            filling.fillAmount = value;
            foreach (var point in points) point.UpdatePoint(value);
        }

#if UNITY_EDITOR
        private void Update()
        {
            if (Application.isPlaying)
                UpdateState();
        }
#endif
    }
}