using System;
using System.Collections;
using UnityEngine;

namespace CandyMasters.Project.Scripts.Common.Classes
{
    [Serializable]
    public class TimeScale
    {
        [field: SerializeField, Range(0f, 1f)] public float Min { get; private set; }
        [field: SerializeField, Range(0f, 1f)] public float Max { get; private set; }
        [field: SerializeField] public float SwitchDuration { get; private set; }

        public float Current { get; private set; }
        private MonoBehaviour _coroutineHandler;
        private Coroutine _coroutine;


        public void Initialize(MonoBehaviour coroutineHandler)
        {
            Current = Max;
            _coroutineHandler = coroutineHandler;
        }

        public void IncreaseTimeScale(Action onComplete = null)
        {
            _coroutine = _coroutineHandler.StartCoroutine(LerpToTimeScale(Max, onComplete));
        }

        public void DecreaseTimeScale(Action onComplete = null, float directValue = -1f)
        {
            directValue = directValue == -1f ? Min : directValue;
            _coroutine = _coroutineHandler.StartCoroutine(LerpToTimeScale(directValue, onComplete));
        }

        public void Stop()
        {
            if (_coroutine == null)
                return;
            
            _coroutineHandler.StopCoroutine(_coroutine);
        }

        public void Reset()
        {
            if (_coroutine != null)
            {
                _coroutineHandler.StopCoroutine(_coroutine);
            }
            
            Current = Max;
        }

        private IEnumerator LerpToTimeScale(float targetValue, Action onComplete = null)
        {
            var evaluationSpeed = (Current - targetValue) / SwitchDuration;

            if (evaluationSpeed < 0) //Add
            {
                while (Current < targetValue)
                {
                    Current -= Time.deltaTime * evaluationSpeed;
                    yield return new WaitForEndOfFrame();
                }
            }
            else //Subtract
            {
                while (Current > targetValue)
                {
                    Current -= Time.deltaTime * evaluationSpeed;
                    yield return new WaitForEndOfFrame();
                }
            }
            
            onComplete?.Invoke();
        }
    }
}