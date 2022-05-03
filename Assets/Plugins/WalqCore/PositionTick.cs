using System;
using Plugins.TiqCore.Implementations.Instructions;
using UnityEngine;

namespace CandyMasters.Plugins.WalqCore
{
    public class PositionTick : Tick
    {
        private readonly Transform _transform;

        public PositionTick
        (
            Func<float, bool> tickWhile,
            Transform transform
        ) 
            : base(tickWhile, null)
        {
            _transform = transform;
        }

        protected override void OnTick()
        {
            
        }

        protected override void OnStart()
        {
            base.OnStart();
        }

        protected override void OnStop()
        {
            base.OnStop();
        }

        protected override void OnComplete()
        {
            base.OnComplete();
        }
    }
}