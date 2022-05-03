using System;
using CandyMaster.Scripts.Interfaces.Utils;

namespace CandyMaster.Scripts.Interfaces
{
    public interface IPaintPanel : IShowable
    {
        event Action Done, Reset;
    }
}