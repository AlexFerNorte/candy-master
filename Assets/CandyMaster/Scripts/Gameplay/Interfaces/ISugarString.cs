using System;
using CandyMaster.Scripts.Interfaces.Utils;
using UnityEngine;

namespace CandyMaster.Scripts.Gameplay.Interfaces
{
    public interface ISugarString : IMovable, IDisposable
    {
        Vector3 Twist { get; set; }

        Color Color { get; set; }
    }
}