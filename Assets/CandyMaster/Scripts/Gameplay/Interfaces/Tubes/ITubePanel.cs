using System;
using CandyMaster.Scripts.Interfaces.Utils;
using UnityEngine;

namespace CandyMaster.Scripts.Gameplay.Interfaces.Tubes
{
    public interface ITubePanel : IShowable
    {
        event Action<Color> ColorSelected;
        
        Color Selected { get; }
    }
}