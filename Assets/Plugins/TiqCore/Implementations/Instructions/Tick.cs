using System;
using System.Collections;
using Plugins.TiqCore.Common;
using Plugins.TiqCore.Core;
using UnityEngine;

namespace Plugins.TiqCore.Implementations.Instructions
{
    public class Tick : Instruction
    {
        private readonly Func<float, bool> _tickWhile;
        private readonly Action<float> _tickAction;


        public Tick
        (
            Func<float, bool> tickWhile, 
            Action<float> tickAction,
            Action onStartAction = null,
            Action onStopAction = null,
            Action onCompleteAction = null
        )
            : base(onStartAction, onStopAction, onCompleteAction)
        {
            _tickWhile = tickWhile;
            _tickAction = tickAction;
        }

        protected override IEnumerator Perform()
        {
            ExecutionTimer = 0f;

            while (_tickWhile.Invoke(ExecutionTimer))
            {
                ExecutionTimer += Time.deltaTime;
                
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