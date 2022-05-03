using System;
using System.Threading.Tasks;
using CandyMaster.Scripts.Interfaces.Utils;
using UnityEngine;

namespace CandyMaster.Scripts.Gameplay.Interfaces
{
    public interface IPot : IMovable, IDisposable
    {
        /// <summary>
        /// Range 0 to 1
        /// </summary>
        float Burn { get; set; }
        
        Color FluidColor { get; }

        /// <summary>
        /// Range 0 to 1
        /// </summary>
        float Fill { get; set; }

        Task FillOut();
    }
}