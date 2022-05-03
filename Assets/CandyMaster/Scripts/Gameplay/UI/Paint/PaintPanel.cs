using System;
using CandyMaster.Scripts.Gameplay.Component;
using CandyMaster.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace CandyMaster.Scripts.UI.Paint
{
    [RequireComponent(typeof(ShowableUIComponent))]
    public class PaintPanel : MonoBehaviour, IPaintPanel
    {
        [SerializeField] private Button done, reset;
        private ShowableUIComponent _showableUIComponent;

        public bool Show
        {
            get => _showableUIComponent.Show;
            set => _showableUIComponent.Show = value;
        }

        public event Action Done;
        public event Action Reset;

        private void Start()
        {
            _showableUIComponent = GetComponent<ShowableUIComponent>();

            done.onClick.AddListener(() => Done?.Invoke());
            reset.onClick.AddListener(() => Reset?.Invoke());
        }
    }
}