using System;
using System.Collections;
using Plugins.TiqCore.Common;
using UnityEngine;

namespace Plugins.TiqCore.Core
{
    public abstract class Instruction
    {
        protected Coroutine Coroutine { get; set; }

        public float ExecutionTimer { get; protected set; }
        public bool IsExecuting { get; protected set; }
        
        protected Action OnStartAction { get; set; }
        protected Action OnStopAction { get; set; }
        protected Action OnCompleteAction { get; set; }


        public Instruction
        (
            Action onStartAction = null,
            Action onStopAction = null,
            Action onCompleteAction = null
        )
        {
            OnStartAction = onStartAction;
            OnStopAction = onStopAction;
            OnCompleteAction = onCompleteAction;
        }

        internal virtual void Start()
        {
            if (IsExecuting)
                throw new Exception("You are trying to start currently executing instruction");
            
            Coroutine = TiqStarter.Instance.StartCoroutine(Perform());
            IsExecuting = true;
            OnStart();
        }

        internal virtual void Stop()
        {
            if (Coroutine != null)
            {
                TiqStarter.Instance.StopCoroutine(Coroutine);
            }
            IsExecuting = false;
            OnStop();
        }

        protected abstract IEnumerator Perform();

        protected virtual void OnStart()
        {
            OnStartAction?.Invoke();
        }

        protected virtual void OnStop()
        {
            OnStopAction?.Invoke();
        }
        
        protected virtual void OnComplete()
        {
            OnCompleteAction?.Invoke();
        }
    }
}