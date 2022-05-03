using System;

namespace CandyMaster.Scripts.Gameplay.Interfaces
{
    public interface ICookerPlate : IDisposable
    {
        /// <summary>
        /// Returns is plate on or off
        /// </summary>
        event Action<bool> StateUpdated;
        
        /// <summary>
        /// If blocked state can't change
        /// </summary>
        bool IsBlocked { get; set; }
    }
}