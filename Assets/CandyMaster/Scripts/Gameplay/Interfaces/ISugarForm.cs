using System;
using System.Threading.Tasks;
using CandyMaster.Scripts.Gameplay.Interfaces.Coloring;
using UnityEngine;

namespace CandyMaster.Scripts.Gameplay.Interfaces
{
    public interface ISugarForm
    {
        event Action<ISugarForm> Clicked;

        bool IsFull { get; }
        
        Vector3 Position { get; }
        
        void FillForFull(float duration, Color color);

        Task<IPaintableBrick> Chill(Color fluidColor);
    }
}