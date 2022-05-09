using System;
using System.Collections;
using Tiq.Plugins.Tiq.Core;
using UnityEngine;

namespace Tiq.Plugins.Tiq.Implementations.Instructions
{
    public class Tick : Instruction
    {
        private readonly Func<float> _timerDelta;
        private readonly Func<float, bool> _tickWhile;
        private readonly Action<float> _tickAction;


        public Tick
        (
            Func<float> timerDelta,
            Func<float, bool> tickWhile,
            Action<float> tickAction,
            Action onStartAction = null,
            Action onStopAction = null,
            Action onCompleteAction = null
        )
            : base(onStartAction, onStopAction, onCompleteAction)
        {
            _timerDelta = timerDelta;
            _tickWhile = tickWhile;
            _tickAction = tickAction;
        }

        protected override IEnumerator Perform()
        {
            ExecutionTimer = 0f;

            while (_tickWhile.Invoke(ExecutionTimer))
            {
                ExecutionTimer += _timerDelta.Invoke();
                
                OnTick();
                
                yield return null;
            }
            
            OnComplete();
        }

        protected virtual void OnTick()
        {
            _tickAction.Invoke(ExecutionTimer);
        }
    }
}