using System;
using CandyMaster.Scripts.Gameplay.Component;
using CandyMaster.Scripts.Interfaces.UI;
using CandyMaster.Scripts.Interfaces.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CandyMaster.Scripts.UI.Input
{
    [RequireComponent(typeof(ShowableUIComponent))]
    public class SwipeUpDownInput : MonoBehaviour, ISwipeInput, IDragHandler, IShowable
    {
        private ShowableUIComponent _showableUIComponent;

        public event Action<float> SwipeDelta;

        public bool Show
        {
            get => _showableUIComponent.Show;
            set => _showableUIComponent.Show = value;
        }

        private void Start()
        {
            _showableUIComponent = GetComponent<ShowableUIComponent>();
        }

        public void OnDrag(PointerEventData eventData)
        {
            SwipeDelta?.Invoke(eventData.delta.y);
        }
    }
}