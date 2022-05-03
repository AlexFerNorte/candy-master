using System.Threading.Tasks;
using CandyMaster.Scripts.Coloring;
using CandyMaster.Scripts.Gameplay.Interfaces.Coloring;
using FluidDynamics.Scripts;
using UnityEngine;

namespace CandyMaster.Scripts.Gameplay.Coloring.Paintable.Plains
{
    public class VortexPaintablePlain : MonoBehaviour, IPaintablePlain
    {
        [SerializeField] private bool simulateOnStart = false;
        
        [Header("Material Applying")] [SerializeField]
        private MeshRenderer simulationRenderer;

        [SerializeField] private MeshRenderer viewRenderer;

        [Header("Painting")] [SerializeField] private AnchorSystem anchorSystem;
        [SerializeField] private float emitterLifeTime = 1;
        [SerializeField] private GameObject emitterPrefab;
        [SerializeField] private MainFluidSimulation fluidSimulation;

        public Vector3 WorldPosition => transform.position;

        private async void Start()
        {
            await Task.Yield();
            viewRenderer.material = simulationRenderer.material;

            fluidSimulation.simulate = simulateOnStart;
        }

        public void ApplyPaintAtWorldPosition(Vector3 position, Color color)
        {
            fluidSimulation.simulate = true;
            
            fluidSimulation.m_colourGradient = new Gradient
            {
                colorKeys = new[] {new GradientColorKey(Color.white, 0), new GradientColorKey(color, 1)},
                alphaKeys = new[] {new GradientAlphaKey(0, 0), new GradientAlphaKey(1, 1)}
            };
            fluidSimulation.UpdateGradient();

            anchorSystem.Leader.transform.position = position;
            var emitter = Instantiate(emitterPrefab, anchorSystem.Follower);
            emitter.GetComponent<PlainPaintEmitterAgent>().Init(fluidSimulation);
            emitter.transform.position = anchorSystem.Follower.position;
            Destroy(emitter, emitterLifeTime);
        }

        public void ResetPaint()
        {
            //TODO
        }

        public void SetPosition(Vector3 localPosition) => transform.localPosition = localPosition;
    }
}