using System;
using CandyMaster.Scripts.Gameplay.Interfaces;
using DG.Tweening;
using UnityEngine;

namespace CandyMaster.Scripts.Gameplay.UI
{
    public class Tutorial : MonoBehaviour, ITutorialHand
    {
        [SerializeField] private float showDuration;
        [SerializeField] private float moveDuration = 0.4f;

        [SerializeField] private GameObject handPoint, handUpDown, handClock;
        private ITutorialHand.Mode? _currentMode;

        private Camera _camera;
        private GameObject[] _hands;

        private void Awake()
        {
            _camera = Camera.main;
            _hands = new[] {handPoint, handUpDown, handClock};
            Hide();
        }

        private GameObject Current(ITutorialHand.Mode mode) => mode switch
        {
            ITutorialHand.Mode.Simple => handPoint,
            ITutorialHand.Mode.UpDown => handUpDown,
            ITutorialHand.Mode.Clock => handClock,
            _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
        };

        public void PointAt(Vector3 position, ITutorialHand.Mode mode)
        {
            if (_currentMode == mode)
                transform.DOMove(_camera.WorldToScreenPoint(position), moveDuration);
            else
            {
                _currentMode = mode;
                var current = Current(mode);
                transform.position = _camera.WorldToScreenPoint(position);
                foreach (var hand in _hands) hand.SetActive(hand == current);

                transform.DOScale(1, showDuration);
            }
        }

        public void Hide()
        {
            if (_currentMode == null) return;
            transform.DOScale(0, showDuration).OnComplete(() =>
            {
                _currentMode = null;
                foreach (var hand in _hands) hand.SetActive(false);
            });
        }
    }
}