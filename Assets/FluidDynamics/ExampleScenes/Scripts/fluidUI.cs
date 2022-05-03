using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FluidDynamics;
using FluidDynamics.Scripts;

public class fluidUI : MonoBehaviour
{
    public Slider vorticity;
    public Slider viscocity;
    public Slider partLife;
    public Slider resolution;
    public Slider simSpeed;
    public MainFluidSimulation fluid;

    private void Update()
    {
        fluid.speed = simSpeed.value;
        fluid.vorticity = vorticity.value;
        fluid.viscosity = viscocity.value;
        fluid.m_densityDissipation = partLife.value;
        fluid.ParticlesResolution = (int)resolution.value;
    }
}
