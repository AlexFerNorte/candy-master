using System;
using System.Threading.Tasks;
using CandyMaster.Scripts.Gameplay.Coloring.Paintable;
using CandyMaster.Scripts.Gameplay.Interfaces;
using CandyMaster.Scripts.Gameplay.Interfaces.Coloring;
using DG.Tweening;
using Obi;
using UnityEngine;

namespace CandyMaster.Scripts.Gameplay.Stuff
{
    [DisallowMultipleComponent]
    public class SugarForm : MonoBehaviour, ISugarForm
    {
        [SerializeField] private bool meltedOnStart = false;
        [SerializeField] private bool isFull;
        [SerializeField] private float chillDuration = 1f;
        [SerializeField] private PaintableBrick paintableBrick;

        [Header("Obi")]
        [SerializeField] private ObiParticleRenderer particleRenderer;
        [SerializeField] private ObiEmitter obiEmitter;
        [SerializeField] private float emitterSpeed = 0.25f;
        [SerializeField] private float fillDelay = 0.2f;
        [SerializeField] private float delayToStop = 0.7f;
        
        

        private Collider _collider;

        [SerializeField] private Vector3 hideBrickPosition = Vector3.down;
        [SerializeField] private Vector3 defaultBrickPosition;
        


        public event Action<ISugarForm> Clicked;

        public bool IsFull
        {
            get => isFull;
            private set => isFull = value;
        }

        public Vector3 Position => transform.position;

        private void Start()
        {
            obiEmitter.speed = 0;
            
            _collider = GetComponent<Collider>();
            _collider.enabled = !meltedOnStart;

            defaultBrickPosition = paintableBrick.transform.localPosition;

            if (!meltedOnStart)
                paintableBrick.transform.localPosition = hideBrickPosition;
        }

        public async void FillForFull(float duration, Color color)
        {
            IsFull = true;
            _collider.enabled = false;

            await Task.Delay(TimeSpan.FromSeconds(fillDelay));
            await Chill(color);
            
            return;
            particleRenderer.particleColor = color;
            await Task.Delay(TimeSpan.FromSeconds(fillDelay));
            obiEmitter.speed = emitterSpeed;
            await Task.Delay(TimeSpan.FromSeconds(delayToStop));
            for (var i = 0; i < obiEmitter.activeParticleCount; i++)
                obiEmitter.DeactivateParticle(obiEmitter.GetParticleRuntimeIndex(i));
        }
        
        public async Task<IPaintableBrick> Chill(Color fluidColor)
        {
            paintableBrick.ApplyPaint(fluidColor);
            paintableBrick.transform.DOLocalMove(defaultBrickPosition, chillDuration);
            //particleRenderer.particleColor;
            await Task.Delay(TimeSpan.FromSeconds(chillDuration));
            obiEmitter.KillAll();
            obiEmitter.speed = 0;
            return paintableBrick;
        }

        private void OnMouseUp() => Clicked?.Invoke(this);
    }
}