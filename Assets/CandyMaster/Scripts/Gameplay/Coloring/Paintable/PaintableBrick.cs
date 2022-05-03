using System;
using CandyMaster.Scripts.Gameplay.Interfaces;
using CandyMaster.Scripts.Gameplay.Interfaces.Coloring;
using CandyMaster.Scripts.Gameplay.Utils;
using UnityEngine;
using UnityEngine.Serialization;

namespace CandyMaster.Scripts.Gameplay.Coloring.Paintable
{
    public class PaintableBrick : MonoBehaviour, IPaintableBrick
    {
        public event Action HasColored;

        [SerializeField] private GameObject sugarStringPrefab;


        [Header("Paint")] [SerializeField] [FormerlySerializedAs("prevColor")]
        private Color _previousColor = Color.white;

        [SerializeField] private int plainID;

        [SerializeField] private Vector3 currentOffset;
        [SerializeField] private Vector3 defaultOffset;


        private IPaintablePlain currentPlain;
        private IPaintablePlain[] paintablePlains;

        public Vector3 Position => transform.position;

        private void Start()
        {
            paintablePlains = this.GetMultipleInChildren<IPaintablePlain>();
            SetNextPlainAsCurrent();
        }

        public void ApplyPaintAtWorldPosition(Vector3 position, Color color)
        {
            if (_previousColor != color)
                SetNextPlainAsCurrent();
            currentPlain.ApplyPaintAtWorldPosition(position, color);
            _previousColor = color;
            HasColored?.Invoke();
        }

        public void ApplyPaint(Color color)
        {
            currentPlain.ApplyPaintAtWorldPosition(currentPlain.WorldPosition, color);
        }

        private void SetNextPlainAsCurrent()
        {
            plainID = (plainID + 1) % paintablePlains.Length;
            currentPlain = paintablePlains[plainID];

            foreach (var plain in paintablePlains) plain.SetPosition(defaultOffset);
            currentPlain.SetPosition(currentOffset);
        }

        public void ResetPaint()
        {
            foreach (var plain in paintablePlains) plain.ResetPaint();
        }

        public ISugarString GetNewPaintedSugarString()
        {
            var obj = Instantiate(sugarStringPrefab, transform).GetOrException<ISugarString>();
            obj.Color = _previousColor;
            return obj;
        }
    }
}