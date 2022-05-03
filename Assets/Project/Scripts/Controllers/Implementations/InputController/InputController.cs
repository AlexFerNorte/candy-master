using System;
using CandyMasters.Project.Scripts.Controllers.Core;
using UnityEngine;
using UnityEngine.Events;

namespace CandyMasters.Project.Scripts.Controllers.Implementations.InputController
{
    [Serializable]
    public class InputController : Controller<InputControllerInitializeData>
    {
        [field: SerializeField] private float SwipeMinYScreenPercent { get; set; }
        [field: SerializeField] private float SwipeDetectHoldTime { get; set; }
        [field: SerializeField] private float SwipeCancelHoldTime { get; set; }
        
        private bool _isDown;
        private int _tapCounter;
        private float _swipeHoldTimer;
        private float _swipeYDelta;
        private bool _isSwipe;

        public UnityEvent OnFirstFingerDown { get; private set; }
        public UnityEvent OnFingerDown { get; private set; }
        public UnityEvent<float> OnFingerHold { get; private set; }
        public UnityEvent OnFingerUp { get; private set; }
        public UnityEvent OnSwipe { get; private set; }

        /*public override void Initialize(InputControllerInitializeData data)
        {
            base.Initialize(data);
            OnFirstFingerDown = new UnityEvent();
            OnFingerDown = new UnityEvent();
            OnFingerHold = new UnityEvent<float>();
            OnFingerUp = new UnityEvent();
            OnSwipe = new UnityEvent();

            LeanTouch.OnFingerDown += FingerDown;
            LeanTouch.OnFingerUpdate += FingerUpdate;
            LeanTouch.OnFingerUp += FingerUp;
        }

        private void FingerDown(LeanFinger finger)
        {
            if (finger.IsOverGui || Variables.IsGameplayInputFrozen)
                return;
                    
            _tapCounter += 1;
                    
            if (_tapCounter == 1)
                OnFirstFingerDown?.Invoke();

            _isDown = true;
            OnFingerDown.Invoke();
        }

        private void FingerUpdate(LeanFinger finger)
        {
            if (finger.IsOverGui || Variables.IsGameplayInputFrozen)
                return;

            if (_isDown)
            {
                OnFingerHold.Invoke(finger.ScreenDelta.x / Screen.width);

                _swipeHoldTimer += Time.deltaTime;
                _swipeYDelta = Mathf.Abs(finger.ScreenDelta.y);

                if (_swipeHoldTimer >= SwipeDetectHoldTime && 
                    _swipeHoldTimer <= SwipeCancelHoldTime && 
                    _swipeYDelta >= Variables.ActiveCameraInstance.Variables.Camera.rect.height * SwipeMinYScreenPercent)
                {
                    _isSwipe = true;
                }
                else
                {
                    _isSwipe = false;
                }
            }
        }

        private void FingerUp(LeanFinger finger)
        {
            if (finger.IsOverGui || Variables.IsGameplayInputFrozen)
                return;
            
            _isDown = false;
            if(_isSwipe)
                OnSwipe.Invoke();
            _swipeHoldTimer = 0f;
            OnFingerUp.Invoke();
        }

        public void Reset()
        {
            LeanTouch.OnFingerDown -= FingerDown;
            LeanTouch.OnFingerUpdate -= FingerUpdate;
            LeanTouch.OnFingerUp -= FingerUp;
            _tapCounter = 0;
            _isDown = false;
        }*/
    }
}
