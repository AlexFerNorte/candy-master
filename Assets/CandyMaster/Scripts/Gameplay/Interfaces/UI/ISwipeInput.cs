using System;

namespace CandyMaster.Scripts.Interfaces.UI
{
    public interface ISwipeInput
    {
        event Action<float> SwipeDelta;
    }
}