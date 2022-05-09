using System;
using CandyMaster.Project.Scripts.Common.Extensions;
using DG.Tweening;
using UnityEngine;

namespace CandyMaster.Project.Scripts.Objects.Common
{
    [RequireComponent(typeof(BoxCollider))]
    public class CookerHandle : MonoBehaviour
    {
        [SerializeField] private Vector3 startRotation;
        [SerializeField] private Vector3 endRotation;
        [SerializeField] private float transitionTime;

        private bool _currentState;
        private Action _onActivate;
        private Action _onDeactivate;


        public void Initialize(Action onActivate, Action onDeactivate)
        {
            _onActivate = onActivate;
            _onDeactivate = onDeactivate;
        }
        
        [ContextMenu("Click")]
        public void OnClick()
        {
            Turn(!_currentState);
        }

        public void Turn(bool state)
        {
            if (state == _currentState)
                return;

            _currentState = state;
            
            var targetRotation = _currentState ? endRotation : startRotation;
            transform.DOLocalRotate(targetRotation, transitionTime);
            
            state.Ternary(_onActivate, _onDeactivate);
        }
    }
}