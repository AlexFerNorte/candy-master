using System;
using System.Collections;
using Tiq.Plugins.Tiq.Core;
using UnityEngine;

namespace Tiq.Plugins.Tiq.Implementations.Instructions
{
    public class AwaitBefore : Instruction
    {
        private readonly float _delay;
        private readonly Action _action;

        
        public AwaitBefore
        (
            float delay, 
            Action action, 
            Action onStartAction = null,
            Action onStopAction = null,
            Action onCompleteAction = null
        )
            : base(onStartAction, onStopAction, onCompleteAction)
        {
            _delay = delay;
            _action = action;
        }

        protected override IEnumerator Perform()
        {
            ExecutionTimer = 0f;
            
            while (ExecutionTimer < _delay)
            {
                ExecutionTimer += Time.deltaTime;
                yield return null;
            }
            
            _action.Invoke();
            
            OnComplete();
        }
    }
}