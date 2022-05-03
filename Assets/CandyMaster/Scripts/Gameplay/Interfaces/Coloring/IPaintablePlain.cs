using UnityEngine;

namespace CandyMaster.Scripts.Gameplay.Interfaces.Coloring
{
    public interface IPaintablePlain
    {
        Vector3 WorldPosition { get; }

        void ApplyPaintAtWorldPosition(Vector3 position, Color color);

        void ResetPaint();

        void SetPosition(Vector3 localPosition);
    }
}