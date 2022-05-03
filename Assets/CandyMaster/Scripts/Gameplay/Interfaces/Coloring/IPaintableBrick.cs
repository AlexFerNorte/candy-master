using System;
using UnityEngine;

namespace CandyMaster.Scripts.Gameplay.Interfaces.Coloring
{
    public interface IPaintableBrick
    {
        event Action HasColored;
        
        Vector3 Position { get; }
        
        void ApplyPaintAtWorldPosition(Vector3 position, Color color);
        
        void ResetPaint();

        ISugarString GetNewPaintedSugarString();
    }
}