using System;
using System.Threading.Tasks;
using CandyMaster.Scripts.Gameplay.Interfaces;
using CandyMaster.Scripts.Gameplay.Utils;
using CandyMaster.Scripts.Interfaces;
using UnityEngine;

namespace CandyMaster.Scripts.Gameplay.Steps
{
    public class BoilStep : AbstractStep
    {
        [SerializeField] private float processSpeed;
        [SerializeField] private float temperatureSpeed = 0.1f;
        [SerializeField] private bool burning;
        [SerializeField] private float minimalTemperatureToUnlockButtonBlock = .5f;
        [SerializeField] private float delayOnStepEnd = 0.6f;

        [Space] [SerializeField] private Transform buttonPoint;

        [Space] [SerializeField] [Header("Steam")]
        private ParticleSystem steamParticles;

        [SerializeField] private AnimationCurve eminCurve;

        [Space] [SerializeField] [Header("Bubbles")]
        private ParticleSystem bubblesParticles;

        [SerializeField] private AnimationCurve bubblesCurve;


        private bool _execute;

        private ITemperatureBar _temperatureBar;
        private ISugarMelt _sugarMelt;
        private IPot _pot;
        private ICookerPlate _plate;

        public override void Init()
        {
            _temperatureBar = this.FindOrException<ITemperatureBar>();
            _sugarMelt = this.FindOrException<ISugarMelt>();
            _pot = this.FindOrException<IPot>();
            _plate = this.FindOrException<ICookerPlate>();

            SetParticlesEmission(0);
        }

        public override async Task ExecuteStep()
        {
            burning = false;
            _execute = true;

            _temperatureBar.Show = true;
            _plate.IsBlocked = false;

            _plate.StateUpdated += PlateOnStateUpdated;

            TutorialHand.PointAt(buttonPoint.position, ITutorialHand.Mode.Simple);

            while (_execute)
            {
                if (burning)
                {
                    _temperatureBar.Value += temperatureSpeed * Time.deltaTime;
                    _pot.Burn = _temperatureBar.Value;
                    _pot.Fill += processSpeed * Time.deltaTime;
                    _sugarMelt.Progress = _pot.Fill;

                    SetParticlesEmission(_pot.Fill);

                    _plate.IsBlocked = _temperatureBar.Value < minimalTemperatureToUnlockButtonBlock;
                    if (!_plate.IsBlocked)
                        TutorialHand.PointAt(buttonPoint.position, ITutorialHand.Mode.Simple);
                }

                await Task.Yield();
            }

            _temperatureBar.Show = false;
            _plate.IsBlocked = true;

            _plate.StateUpdated -= PlateOnStateUpdated;

            await Task.Delay(TimeSpan.FromSeconds(delayOnStepEnd));
        }

        protected override void DisposeSelf()
        {
            _plate.Dispose();
            _temperatureBar.Dispose();
            Destroy(steamParticles);
        }

        /// <param name="value">Range 0 - 1</param>
        private void SetParticlesEmission(float value)
        {
            void Apply(ParticleSystem particleSystem, AnimationCurve curve)
            {
                var emission = particleSystem.emission;
                emission.rateOverTime = curve.Evaluate(value);
            }
            
            Apply(steamParticles, eminCurve);
            Apply(bubblesParticles, bubblesCurve);
        }

        private void PlateOnStateUpdated(bool obj)
        {
            burning = obj;
            if (obj)
                _plate.IsBlocked = true;
            else
                _execute = false;

            TutorialHand.Hide();
        }
    }
}