using CandyMaster.Scripts.Interfaces.Utils;
using UnityEngine;

namespace CandyMaster.Scripts.Gameplay.Interfaces.Tubes
{
    public interface ITube : IMovable
    {
        Color PaintColor { get; set; }
    }
}