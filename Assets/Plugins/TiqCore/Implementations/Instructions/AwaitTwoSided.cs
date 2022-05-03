using System;
using System.Collections;
using Plugins.TiqCore.Common;
using Plugins.TiqCore.Core;
using UnityEngine;

namespace Plugins.TiqCore.Implementations.Instructions
{
    public class AwaitTwoSided : Instruction
    {
        private readonly float[] _delays;
        private readonly Action _action;

        
        public AwaitTwoSided
        (
            float delayBefore,
            float delayAfter, 
            Action action, 
            Action onStartAction = null,
            Action onStopAction = null,
            Action onCompleteAction = null
        )
            : base(onStartAction, onStopAction, onCompleteAction)
        {
            _delays = new[] { delayBefore, delayAfter };
            _action = action;
        }

        protected override IEnumerator Perform()
        {
            ExecutionTimer = 0f;

            for (int i = 0; i < 2; i++)
            {
                while (ExecutionTimer < _delays[i])
                {
                    ExecutionTimer += Time.deltaTime;
                    yield return null;
                }
                
                if(i <= 0)
                    _action.Invoke();
            }

            OnComplete();
        }
    }
}