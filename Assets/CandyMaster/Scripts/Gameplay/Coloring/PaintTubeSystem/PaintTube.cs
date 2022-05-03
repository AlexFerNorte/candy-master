using System;
using System.Threading.Tasks;
using CandyMaster.Scripts.Gameplay.Component;
using CandyMaster.Scripts.Gameplay.Interfaces.Tubes;
using CandyMaster.Scripts.Gameplay.Stuff.TubePanels;
using CandyMaster.Scripts.Interfaces;
using UnityEngine;

namespace CandyMaster.Scripts.Coloring.PaintTubeSystem
{
    [RequireComponent(typeof(MovableComponent))]
    public class PaintTube : MonoBehaviour, ITube
    {
        [SerializeField] private TubeSelector tubeSelector;
        [SerializeField] private PaintTearPool paintTearPool;

        [SerializeField] private float tearLifeTime = 0.4f;

        [SerializeField] private Transform paintNozzle;

        private MovableComponent _movableComponent;

        public Vector3 Position
        {
            get => _movableComponent.Position;
            set => _movableComponent.Position = value;
        }

        public Color PaintColor
        {
            get => tubeSelector.Color;
            set => tubeSelector.Color = value;
        }

        public async void Emit()
        {
            var tear = paintTearPool.GetTear();
            tear.SetColor(tubeSelector.Color);
            tear.transform.position = paintNozzle.position;

            await Task.Delay(TimeSpan.FromSeconds(tearLifeTime));

            if (tear.gameObject.activeSelf)
                paintTearPool.AddToPool(tear);
        }

        public Task MoveTo(Vector3 position) => _movableComponent.MoveTo(position);

        private void Awake()
        {
            _movableComponent = GetComponent<MovableComponent>();
        }
    }
}