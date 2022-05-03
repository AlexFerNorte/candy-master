using System;
using UnityEngine;

namespace CandyMasters.Project.Scripts.UI.Core
{
    [RequireComponent(typeof(UnityEngine.UI.Button))]
    public abstract class Button : MonoBehaviour
    {
        [SerializeField] private UnityEngine.UI.Button button;

        private Action _onClick;
        private bool _isClicked;
        private float _timer;

        protected void Initialize(Action onClick)
        {
            _onClick = onClick;
            button.onClick.AddListener(Click);
        }

        private void Update()
        {
            _timer += Time.deltaTime;
            if (_timer >= 1f && _isClicked)
            {
                _timer = 0f;
                _isClicked = false;
            }
        }

        private void Click()
        {
            if(_isClicked)
                return;

            _onClick.Invoke();
            _isClicked = true;
        }

        protected void Reset()
        {
            _isClicked = false;
        }
    }
}