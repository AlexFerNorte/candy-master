using System;
using System.Collections;
using Plugins.TiqCore.Common;
using Plugins.TiqCore.Core;
using UnityEngine;

namespace Plugins.TiqCore.Implementations.Instructions
{
    public class TickPeriodic : Instruction
    {
        private readonly int _iterations;
        private readonly Func<float> _period;
        private readonly Func<float> _timerDelta;
        private readonly Action<float> _tickAction;

        public float CurrentIteration { get; private set; }


        public TickPeriodic
        (
            int iterations,
            Func<float> period,
            Func<float> timerDelta,
            Action<float> tickAction, 
            Action onStartAction = null,
            Action onStopAction = null,
            Action onCompleteAction = null
        )
            : base( onStartAction, onStopAction, onCompleteAction)
        {
            _iterations = iterations;
            _period = period;
            _timerDelta = timerDelta;
            _tickAction = tickAction;
        }

        protected override IEnumerator Perform()
        {
            CurrentIteration = 0;
            ExecutionTimer = 0f;

            while (CurrentIteration < _iterations)
            {
                ExecutionTimer = _timerDelta.Invoke();

                if (ExecutionTimer >= _period.Invoke())
                {
                    OnTick();
                    
                    CurrentIteration += 1;
                    
                    ExecutionTimer = 0f;
                }

                yield return new WaitForEndOfFrame();
            }
            
            OnComplete();
        }
        
        protected virtual void OnTick()
        {
            _tickAction.Invoke(ExecutionTimer);
        }
    }
}