using System;
using System.Collections;
using Plugins.TiqCore.Common;
using Plugins.TiqCore.Core;
using UnityEngine;

namespace Plugins.TiqCore.Implementations.Instructions
{
    public class Await : Instruction
    {
        private readonly Func<float, bool> _waitWhile;

        public Await
        (
            Func<float, bool> waitWhile, 
            Action onStartAction = null,
            Action onStopAction = null,
            Action onCompleteAction = null
        ) 
            : base( onStartAction, onStopAction, onCompleteAction)
        {
            _waitWhile = waitWhile;
        }

        protected override IEnumerator Perform()
        {
            ExecutionTimer = 0f;
            
            while (_waitWhile.Invoke(ExecutionTimer))
            {
                ExecutionTimer += Time.deltaTime;
                yield return null;
            }
            
            OnComplete();
        }
    }
}