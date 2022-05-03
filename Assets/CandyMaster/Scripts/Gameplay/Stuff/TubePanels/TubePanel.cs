using System;
using CandyMaster.Scripts.Gameplay.Interfaces.Tubes;
using CandyMaster.Scripts.Interfaces;
using DG.Tweening;
using UnityEngine;

namespace CandyMaster.Scripts.Gameplay.Stuff.TubePanels
{
    public class TubePanel : MonoBehaviour, ITubePanel
    {
        [SerializeField] private new Camera camera;
        [SerializeField] private float cameraDistance = 100;


        [SerializeField] private float showDuration;
        [SerializeField] private Vector3 hideOffset;
        [SerializeField] private bool hideOnStart;
        [SerializeField] private bool show;

        [Header("Selectors")] [SerializeField] private float selectorShowDuration = .3f;
        [SerializeField] private float selectorOffset = 1;
        [SerializeField] private TubeSelector currentSelector;

        [Header("Grid")] [SerializeField] private Vector3 gridStep = Vector3.right;
        [SerializeField] private Vector3 gridOffset = Vector3.left;

        private TubeSelector[] _selectors;


        public bool Show
        {
            get => show;
            set
            {
                show = value;
                camera.enabled = value;
                transform.DOLocalMove(value ? Vector3.zero : hideOffset, showDuration);
            }
        }

        public event Action<Color> ColorSelected;

        public Color Selected => currentSelector.Color;

        private void Start()
        {
            if (hideOnStart) transform.localPosition = hideOffset;
            show = !hideOnStart;

            _selectors = GetComponentsInChildren<TubeSelector>();
            foreach (var tubeSelector in _selectors) tubeSelector.Clicked += TubeSelectorOnClicked;

            TubeSelectorOnClicked(currentSelector);
        }

        private void Update()
        {
            if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out var info) &&
                info.collider.TryGetComponent(out TubeSelector selector))
                selector.OnMouseUp();
        }


        [ContextMenu(nameof(UpdateGrid))]
        private void UpdateGrid()
        {
            var start = gridOffset;
            for (var i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).localPosition = start;
                start += gridStep;
            }
        }

        private void OnDestroy()
        {
            foreach (var tubeSelector in _selectors) tubeSelector.Clicked -= TubeSelectorOnClicked;
        }

        private void SetSelectorHeight(TubeSelector selector, float height)
        {
            var position = selector.transform.localPosition;
            position.y = height;
            selector.transform.DOLocalMove(position, selectorShowDuration);
        }

        private void TubeSelectorOnClicked(TubeSelector obj)
        {
            currentSelector = obj;

            foreach (var tubeSelector in _selectors) SetSelectorHeight(tubeSelector, 0);
            SetSelectorHeight(obj, selectorOffset);

            ColorSelected?.Invoke(Selected);
        }
    }
}