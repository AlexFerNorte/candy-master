using System;
using System.Collections;
using Tiq.Plugins.Tiq.Core;

namespace Tiq.Plugins.Tiq.Implementations.Instructions
{
    public class TickConditional : Instruction
    {
        private readonly Func<float> _timerDelta;
        private readonly Func<float, bool> _tickWhile;
        private readonly Func<float, bool> _tickCondition;
        private readonly Action<float> _tickAction;
        private readonly bool _resetTimerOnTick;


        public TickConditional
        (
            Func<float> timerDelta,
            Func<float, bool> tickWhile,
            Func<float, bool> tickCondition,
            Action<float> tickAction,
            bool resetTimerOnTick = false,
            Action onStartAction = null,
            Action onStopAction = null,
            Action onCompleteAction = null
        )
            : base( onStartAction, onStopAction, onCompleteAction)
        {
            _timerDelta = timerDelta;
            _tickWhile = tickWhile;
            _tickCondition = tickCondition;
            _tickAction = tickAction;
            _resetTimerOnTick = resetTimerOnTick;
        }

        protected override IEnumerator Perform()
        {
            ExecutionTimer = 0f;

            while (_tickWhile.Invoke(ExecutionTimer))
            {
                ExecutionTimer += _timerDelta.Invoke();
                
                if (_tickCondition.Invoke(ExecutionTimer))
                {
                    OnTick();
                    
                    if(_resetTimerOnTick)
                        ExecutionTimer = 0f;
                }

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