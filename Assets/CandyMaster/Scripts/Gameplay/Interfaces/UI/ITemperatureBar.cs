using System;
using CandyMaster.Scripts.Interfaces.Utils;

namespace CandyMaster.Scripts.Interfaces
{
    public interface ITemperatureBar : IShowable, IDisposable
    {
        public float Value { get; set; }
    }
}