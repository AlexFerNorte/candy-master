using FluidDynamics;
using FluidDynamics.Scripts;
using FluidDynamics.Scripts.Emitters;
using UnityEngine;

namespace CandyMaster.Scripts.Coloring
{
    public class PlainPaintEmitterAgent : MonoBehaviour
    {
        [SerializeField] private FluidDynamicsParticlesEmitter[] particlesEmitters;
        [SerializeField] private FluidDynamicsVelocityEmitter[] velocityEmitters;

        public void Init(MainFluidSimulation simulation)
        {
            foreach (var emitter in particlesEmitters) emitter.m_mainSimulation = simulation;
            foreach (var emitter in velocityEmitters) emitter.m_fluid = simulation;
        }
    }
}