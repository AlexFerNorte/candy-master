using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace FluidDynamics.Scripts
{
    [RequireComponent(typeof(MeshCollider), typeof(MeshRenderer))]
    [AddComponentMenu("Fluid Dynamics/Fluid Simulator")]
    public class MainFluidSimulation : MonoBehaviour
    {
        #region Variables

        public ComputeShader simulationShader;

        public ComputeShader particleAreaShader;

        public bool simulate = true;

        [Tooltip(
            "If m_ExposeParticles is true the value of the particles will be cached in memory for use by other systems")]
        public bool m_CacheParticles = false;

        public Gradient m_colourGradient;
        public bool m_updateGradient = false;

        public Quality quality = Quality.Medium;

        [Range(0, 5000)] public float speed = 500f;

        [Tooltip("Iterations")] [Range(1, 100)]
        public int simulationQuality = 50; //Iterations

        [Range(0, 1)] public float velocityDissipation = 1f;

        [Range(0, 50)] public float vorticity = 5f;

        [Range(0, 1)] public float viscosity = 0f;

        private int resolution = 512;

        [Header("Advanced")] public bool cacheVelocity = false;

        private ComputeBuffer VelocityBuffer => m_velocityBuffer[VELOCITY_READ];

        private ComputeBuffer _obstaclesBuffer;

        private ComputeBuffer[] m_velocityBuffer;

        private ComputeBuffer m_divergenceBuffer;

        private ComputeBuffer[] m_pressure;

        private ComputeBuffer m_vorticityBuffer;

        private Vector2[] m_currentVelocity;

        private int m_nNumCells;

        private int m_nNumGroupsX;

        private int m_nNumGroupsY;

        private int m_nWidth = 512;

        private int m_nHeight = 512;

        private int m_addVelocityKernel = 0;

        private int m_initBoundariesKernel = 0;

        private int m_advectVelocityKernel = 0;

        private int m_divergenceKernel = 0;

        private int m_poissonKernel = 0;

        private int m_substractGradientKernel = 0;

        private int m_calcVorticityKernel = 0;

        private int m_applyVorticityKernel = 0;

        private int m_viscosityKernel = 0;

        private int m_addObstacleCircleKernel = 0;

        private int m_addObstacleTriangleKernel = 0;

        private int m_clearBufferKernel = 0;

        private int VELOCITY_READ = 0;

        private int VELOCITY_WRITE = 1;

        private int PRESSURE_READ = 0;

        private int PRESSURE_WRITE = 1;

        #endregion

        //


        #region Variables2

        [Tooltip("Particles life")] public float m_densityDissipation = 1f;


        [Range(128, 8192)] public int particlesResolution = 128;


        private ComputeBuffer m_colourRamp;
        private int m_addParticlesKernel = 0;
        private int m_advectKernel = 0;

        public int ParticlesResolution
        {
            get => particlesResolution;
            set
            {
                var saved = particlesResolution;
                particlesResolution = value;
                if (saved != value && Application.isPlaying && _mParticlesBuffer[0] != null &&
                    _mParticlesBuffer[1] != null)
                {
                    var nOldHeight = _mNParticlesHeight;
                    var nOldWidth = _mNParticlesWidth;
                    var oldParticleData = new float[nOldWidth * nOldHeight];
                    _mParticlesBuffer[READ].GetData(oldParticleData);
                    _mParticlesBuffer[0].Dispose();
                    _mParticlesBuffer[1].Dispose();

                    CalculateSize();

                    var newParticleData = new float[_mNParticlesWidth * _mNParticlesHeight];
                    for (var i = 0; i < _mNParticlesHeight; ++i)
                    {
                        for (var j = 0; j < _mNParticlesWidth; ++j)
                        {
                            var normX = (float) j / _mNParticlesWidth;
                            var normY = (float) i / _mNParticlesHeight;
                            var x = (int) (normX * nOldWidth);
                            var y = (int) (normY * nOldHeight);
                            newParticleData[i * _mNParticlesWidth + j] = oldParticleData[y * nOldWidth + x];
                        }
                    }

                    _mParticlesBuffer = new ComputeBuffer[2];
                    for (int i = 0; i < 2; ++i)
                    {
                        _mParticlesBuffer[i] = new ComputeBuffer(_mNParticlesWidth * _mNParticlesHeight, 4,
                            ComputeBufferType.Default);
                    }

                    _mParticlesBuffer[READ].SetData(newParticleData);
                }
            }
        }

        private int m_nColourRampSize = 256;
        private ComputeBuffer[] _mParticlesBuffer;
        private int _mNParticlesNumGroupsX;
        private int _mNParticlesNumGroupsY;
        private int _mNParticlesWidth = 512;
        private int _mNParticlesHeight = 512;
        private int READ = 0;
        private int WRITE = 1;
        private float[] m_currentParticles;
        private Renderer m_tempRend;

        #endregion

        private MaterialPropertyBlock _materialPropertyBlock;
        [SerializeField] private Material _material;

        #region Keys

        private static readonly int Size = Shader.PropertyToID("_Size");
        private static readonly int VelocityIn = Shader.PropertyToID("_VelocityIn");
        private static readonly int VelocityOut = Shader.PropertyToID("_VelocityOut");
        private static readonly int Obstacles = Shader.PropertyToID("_Obstacles");
        private static readonly int Divergence = Shader.PropertyToID("_Divergence");
        static readonly int PressureIn = Shader.PropertyToID("_PressureIn");
        static readonly int PressureOut = Shader.PropertyToID("_PressureOut");
        static readonly int _Vorticity = Shader.PropertyToID("_Vorticity");

        static readonly int _ElapsedTime = Shader.PropertyToID("_ElapsedTime");
        static readonly int _Speed = Shader.PropertyToID("_Speed");
        static readonly int Radius = Shader.PropertyToID("_Radius");
        static readonly int Position = Shader.PropertyToID("_Position");
        static readonly int _Value = Shader.PropertyToID("_Value");
        static readonly int _VorticityScale = Shader.PropertyToID("_VorticityScale");
        static readonly int _Static = Shader.PropertyToID("_Static");
        private static readonly int P1 = Shader.PropertyToID("_P1");
        private static readonly int P2 = Shader.PropertyToID("_P2");
        private static readonly int P3 = Shader.PropertyToID("_P3");
        static readonly int _ParticlesIn = Shader.PropertyToID("_ParticlesIn");
        static readonly int _ParticlesOut = Shader.PropertyToID("_ParticlesOut");

        static readonly int _ParticleSize = Shader.PropertyToID("_ParticleSize");
        static readonly int _Velocity = Shader.PropertyToID("_Velocity");

        static readonly int _VelocitySize = Shader.PropertyToID("_VelocitySize");
        static readonly int _Dissipation = Shader.PropertyToID("_Dissipation");

        static readonly int _Buffer = Shader.PropertyToID("_Buffer");
        static readonly int _Particles = Shader.PropertyToID("_Particles");
        static readonly int _ColourRamp = Shader.PropertyToID("_ColourRamp");
        static readonly int _Alpha = Shader.PropertyToID("_Alpha");
        static readonly int _rBeta = Shader.PropertyToID("_rBeta");

        #endregion

        private void Start()
        {
            _materialPropertyBlock = new MaterialPropertyBlock();

            ParticlesResolution = particlesResolution;
            resolution = quality switch
            {
                Quality.Low => 128,
                Quality.Medium => 256,
                Quality.High => 512,
                _ => throw new ArgumentOutOfRangeException()
            };

            simulationShader = (ComputeShader) Instantiate(Resources.Load("FLUID_DYNAMICS_SIMULATION"));
            particleAreaShader = (ComputeShader) Instantiate(Resources.Load("FLUID_DYNAMICS_PARTICLES"));
            m_tempRend = GetComponent<Renderer>();
            _material = m_tempRend.material = new Material(Shader.Find("FluidDynamics/MatRenderer"));
            if (SystemInfo.supportsComputeShaders)
            {
                m_nNumCells = m_nWidth * m_nHeight;
                m_addVelocityKernel = simulationShader.FindKernel("AddVelocity");
                m_initBoundariesKernel = simulationShader.FindKernel("InitBoundaries");
                m_advectVelocityKernel = simulationShader.FindKernel("AdvectVelocity");
                m_divergenceKernel = simulationShader.FindKernel("Divergence");
                m_clearBufferKernel = simulationShader.FindKernel("ClearBuffer");
                m_poissonKernel = simulationShader.FindKernel("Poisson");
                m_substractGradientKernel = simulationShader.FindKernel("SubstractGradient");
                m_calcVorticityKernel = simulationShader.FindKernel("CalcVorticity");
                m_applyVorticityKernel = simulationShader.FindKernel("ApplyVorticity");
                m_viscosityKernel = simulationShader.FindKernel("Viscosity");
                m_addObstacleCircleKernel = simulationShader.FindKernel("AddCircleObstacle");
                m_addObstacleTriangleKernel = simulationShader.FindKernel("AddTriangleObstacle");
                CalculateSize();
                LinkToFluidSimulation();
                m_advectKernel = particleAreaShader.FindKernel("Advect");
                m_addParticlesKernel = particleAreaShader.FindKernel("AddParticles");
            }
            else
            {
                simulate = false;
                Debug.LogError(
                    "Seems like your target Hardware does not support Compute Shaders. 'Fluid Dynamics' needs support for Compute Shaders to work.");
            }
        }

        private void Update()
        {
            if (simulate)
            {
                UpdateParameters();
                CreateBuffersIfNeeded();
                simulationShader.SetBuffer(m_initBoundariesKernel, VelocityIn, m_velocityBuffer[VELOCITY_READ]);
                simulationShader.Dispatch(m_initBoundariesKernel, m_nNumGroupsX, m_nNumGroupsY, 1);
                simulationShader.SetBuffer(m_advectVelocityKernel, Obstacles, _obstaclesBuffer);
                simulationShader.SetBuffer(m_advectVelocityKernel, VelocityIn, m_velocityBuffer[VELOCITY_READ]);
                simulationShader.SetBuffer(m_advectVelocityKernel, VelocityOut, m_velocityBuffer[VELOCITY_WRITE]);
                simulationShader.Dispatch(m_advectVelocityKernel, m_nNumGroupsX, m_nNumGroupsY, 1);
                FlipVelocityBuffers();
                simulationShader.SetBuffer(m_calcVorticityKernel, VelocityIn, m_velocityBuffer[VELOCITY_READ]);
                simulationShader.SetBuffer(m_calcVorticityKernel, _Vorticity, m_vorticityBuffer);
                simulationShader.Dispatch(m_calcVorticityKernel, m_nNumGroupsX, m_nNumGroupsY, 1);
                simulationShader.SetBuffer(m_applyVorticityKernel, VelocityIn, m_velocityBuffer[VELOCITY_READ]);
                simulationShader.SetBuffer(m_applyVorticityKernel, _Vorticity, m_vorticityBuffer);
                simulationShader.SetBuffer(m_applyVorticityKernel, VelocityOut, m_velocityBuffer[VELOCITY_WRITE]);
                simulationShader.Dispatch(m_applyVorticityKernel, m_nNumGroupsX, m_nNumGroupsY, 1);
                FlipVelocityBuffers();
                if (viscosity > 0)
                    for (var i = 0; i < simulationQuality; ++i)
                    {
                        simulationShader.SetBuffer(m_viscosityKernel, VelocityIn, m_velocityBuffer[VELOCITY_READ]);
                        simulationShader.SetBuffer(m_viscosityKernel, VelocityOut, m_velocityBuffer[VELOCITY_WRITE]);
                        simulationShader.Dispatch(m_viscosityKernel, m_nNumGroupsX, m_nNumGroupsY, 1);
                        FlipVelocityBuffers();
                    }

                simulationShader.SetBuffer(m_divergenceKernel, VelocityIn, m_velocityBuffer[VELOCITY_READ]);
                simulationShader.SetBuffer(m_divergenceKernel, Obstacles, _obstaclesBuffer);
                simulationShader.SetBuffer(m_divergenceKernel, Divergence, m_divergenceBuffer);
                simulationShader.Dispatch(m_divergenceKernel, m_nNumGroupsX, m_nNumGroupsY, 1);
                simulationShader.SetBuffer(m_clearBufferKernel, _Buffer, m_pressure[PRESSURE_READ]);
                simulationShader.Dispatch(m_clearBufferKernel, m_nNumGroupsX, m_nNumGroupsY, 1);
                simulationShader.SetBuffer(m_poissonKernel, Divergence, m_divergenceBuffer);
                simulationShader.SetBuffer(m_poissonKernel, Obstacles, _obstaclesBuffer);

                for (var i = 0; i < simulationQuality; ++i)
                {
                    simulationShader.SetBuffer(m_poissonKernel, PressureIn, m_pressure[PRESSURE_READ]);
                    simulationShader.SetBuffer(m_poissonKernel, PressureOut, m_pressure[PRESSURE_WRITE]);
                    simulationShader.Dispatch(m_poissonKernel, m_nNumGroupsX, m_nNumGroupsY, 1);
                    FlipPressureBuffers();
                }

                simulationShader.SetBuffer(m_substractGradientKernel, PressureIn, m_pressure[PRESSURE_READ]);
                simulationShader.SetBuffer(m_substractGradientKernel, VelocityIn, m_velocityBuffer[VELOCITY_READ]);
                simulationShader.SetBuffer(m_substractGradientKernel, VelocityOut, m_velocityBuffer[VELOCITY_WRITE]);
                simulationShader.SetBuffer(m_substractGradientKernel, Obstacles, _obstaclesBuffer);
                simulationShader.Dispatch(m_substractGradientKernel, m_nNumGroupsX, m_nNumGroupsY, 1);
                FlipVelocityBuffers();
                simulationShader.SetBuffer(m_clearBufferKernel, _Buffer, _obstaclesBuffer);
                simulationShader.Dispatch(m_clearBufferKernel, m_nNumGroupsX, m_nNumGroupsY, 1);
                if (cacheVelocity)
                {
                    m_velocityBuffer[VELOCITY_READ].GetData(m_currentVelocity);
                }
            }

            if (_mParticlesBuffer == null)
            {
                _mParticlesBuffer = new ComputeBuffer[2];
                for (var i = 0; i < 2; ++i)
                    _mParticlesBuffer[i] = new ComputeBuffer(_mNParticlesWidth * _mNParticlesHeight, 4,
                        ComputeBufferType.Default);
            }

            particleAreaShader.SetFloat(_Dissipation, m_densityDissipation);
            particleAreaShader.SetFloat(_ElapsedTime, Time.deltaTime);
            particleAreaShader.SetFloat(_Speed, speed);
            particleAreaShader.SetBuffer(m_advectKernel, Obstacles, _obstaclesBuffer);
            particleAreaShader.SetBuffer(m_advectKernel, _Velocity, VelocityBuffer);
            particleAreaShader.SetBuffer(m_advectKernel, _ParticlesIn, _mParticlesBuffer[READ]);
            particleAreaShader.SetBuffer(m_advectKernel, _ParticlesOut, _mParticlesBuffer[WRITE]);
            particleAreaShader.Dispatch(m_advectKernel, _mNParticlesNumGroupsX, _mNParticlesNumGroupsY, 1);
            FlipBuffers();
            if (m_colourRamp == null)
            {
                m_colourRamp = new ComputeBuffer(m_nColourRampSize, 16, ComputeBufferType.Default);
                UpdateGradient();
            }

            if (m_updateGradient)
                UpdateGradient();

            //ApplyViaMaterial();
            //ApplyViaMaterial();
            ApplyDirectly();


            if (m_CacheParticles)
                _mParticlesBuffer[READ].GetData(m_currentParticles);
        }

        private void ApplyViaMaterial()
        {
            _material.SetBuffer(_Particles, _mParticlesBuffer[READ]);
            _material.SetBuffer(_ColourRamp, m_colourRamp);
            _material.SetVector(Size, new Vector2(_mNParticlesWidth, _mNParticlesHeight));
            m_tempRend.material = _material;
        }
        
        private void ApplyDirectly()
        {
            m_tempRend.material.SetBuffer(_Particles, _mParticlesBuffer[READ]);
            m_tempRend.material.SetBuffer(_ColourRamp, m_colourRamp);
            m_tempRend.material.SetVector(Size, new Vector2(_mNParticlesWidth, _mNParticlesHeight));
        }

        private void ApplyViaPropertyBlock()
        {
            _materialPropertyBlock.SetBuffer(_Particles, _mParticlesBuffer[READ]);
            _materialPropertyBlock.SetBuffer(_ColourRamp, m_colourRamp);
            _materialPropertyBlock.SetVector(Size, new Vector2(_mNParticlesWidth, _mNParticlesHeight));
            m_tempRend.SetPropertyBlock(new MaterialPropertyBlock(), 0);
        }

        private void OnDestroy() => OnDisable();

        private void OnDisable()
        {
            if (m_velocityBuffer is {Length: 2})
            {
                m_velocityBuffer[0]?.Dispose();
                m_velocityBuffer[1]?.Dispose();
            }

            m_divergenceBuffer?.Dispose();

            if (m_pressure is {Length: 2})
            {
                m_pressure[0]?.Dispose();
                m_pressure[1]?.Dispose();
            }

            _obstaclesBuffer?.Dispose();
            m_vorticityBuffer?.Dispose();

            m_colourRamp?.Dispose();

            if (_mParticlesBuffer is {Length: 2})
            {
                _mParticlesBuffer[0]?.Dispose();
                _mParticlesBuffer[1]?.Dispose();
            }
        }

        public void SetSize(int nWidth, int nHeight)
        {
            simulationShader.GetKernelThreadGroupSizes(0, out var groupSizeX, out _, out _);
            m_nWidth = nWidth;
            m_nHeight = nHeight;
            m_nNumCells = m_nWidth * m_nHeight;
            m_nNumGroupsX = Mathf.CeilToInt((float) m_nWidth / groupSizeX);
            m_nNumGroupsY = Mathf.CeilToInt((float) m_nHeight / groupSizeX);
        }

        public int GetWidth() => m_nWidth;

        public int GetHeight() => m_nHeight;

        public void AddVelocity(Vector2 position, Vector2 velocity, float fRadius)
        {
            if (simulationShader != null && m_velocityBuffer != null && m_velocityBuffer.Length >= 2)
            {
                float[] pos = {position.x, position.y};
                simulationShader.SetFloats(Position, pos);
                float[] val = {velocity.x, velocity.y};
                simulationShader.SetFloats(_Value, val);
                simulationShader.SetFloat(Radius, fRadius);
                simulationShader.SetInts(Size, new int[] {m_nWidth, m_nHeight});
                simulationShader.SetBuffer(m_addVelocityKernel, VelocityIn, m_velocityBuffer[VELOCITY_READ]);
                simulationShader.SetBuffer(m_addVelocityKernel, VelocityOut, m_velocityBuffer[VELOCITY_WRITE]);
                simulationShader.Dispatch(m_addVelocityKernel, m_nNumGroupsX, m_nNumGroupsY, 1);
                FlipVelocityBuffers();
            }
        }

        public void AddObstacleCircle(Vector2 position, float fRadius, bool bStatic)
        {
            float[] pos = {position.x, position.y};
            simulationShader.SetFloats(Position, pos);
            simulationShader.SetFloat(Radius, fRadius);
            simulationShader.SetInt(_Static, bStatic ? 1 : 0);
            simulationShader.SetBuffer(m_addObstacleCircleKernel, Obstacles, _obstaclesBuffer);
            simulationShader.Dispatch(m_addObstacleCircleKernel, m_nNumGroupsX, m_nNumGroupsY, 1);
        }

        public void AddObstacleTriangle(Vector2 p1, Vector2 p2, Vector2 p3, bool bStatic = false)
        {
            float[] pos1 = {p1.x, p1.y};
            float[] pos2 = {p2.x, p2.y};
            float[] pos3 = {p3.x, p3.y};
            simulationShader.SetFloats(P1, pos1);
            simulationShader.SetFloats(P2, pos2);
            simulationShader.SetFloats(P3, pos3);
            simulationShader.SetInt(_Static, bStatic ? 1 : 0);
            simulationShader.SetBuffer(m_addObstacleTriangleKernel, Obstacles, _obstaclesBuffer);
            simulationShader.Dispatch(m_addObstacleTriangleKernel, m_nNumGroupsX, m_nNumGroupsY, 1);
        }

        public Vector2 GetVelocity(int x, int y) => m_currentVelocity[y * m_nWidth + x] * speed;

        public void InitShaders()
        {
            CreateBuffersIfNeeded();
            UpdateParameters();
            int[] size = {m_nWidth, m_nHeight};
            simulationShader.SetInts(Size, size);
            m_currentVelocity = new Vector2[m_nNumCells];
        }

        private void CreateBuffersIfNeeded()
        {
            if (m_velocityBuffer == null)
            {
                m_velocityBuffer = new ComputeBuffer[2];
                for (var i = 0; i < 2; ++i)
                    m_velocityBuffer[i] = new ComputeBuffer(m_nNumCells, 8, ComputeBufferType.Default);
            }

            m_divergenceBuffer ??= new ComputeBuffer(m_nNumCells, 4, ComputeBufferType.Default);

            if (m_pressure == null)
            {
                m_pressure = new ComputeBuffer[2];
                for (int i = 0; i < 2; ++i)
                {
                    m_pressure[i] = new ComputeBuffer(m_nNumCells, 4, ComputeBufferType.Default);
                }
            }

            _obstaclesBuffer ??= new ComputeBuffer(m_nNumCells, 8, ComputeBufferType.Default);

            m_vorticityBuffer ??= new ComputeBuffer(m_nNumCells, 4, ComputeBufferType.Default);
        }

        private void UpdateParameters()
        {
            simulationShader.SetFloat(_Dissipation, velocityDissipation);
            simulationShader.SetFloat(_ElapsedTime, Time.deltaTime);
            simulationShader.SetFloat(_Speed, speed);
            simulationShader.SetFloat(_VorticityScale, vorticity);

            var centreFactor = 1.0f / (viscosity);
            var stencilFactor = 1.0f / (4.0f + centreFactor);
            simulationShader.SetFloat(_Alpha, centreFactor);
            simulationShader.SetFloat(_rBeta, stencilFactor);
        }

        private void FlipVelocityBuffers() => (VELOCITY_READ, VELOCITY_WRITE) = (VELOCITY_WRITE, VELOCITY_READ);

        private void FlipPressureBuffers() => (PRESSURE_READ, PRESSURE_WRITE) = (PRESSURE_WRITE, PRESSURE_READ);

        private void CalculateSize()
        {
            var x = gameObject.transform.lossyScale.x;
            var y = gameObject.transform.lossyScale.z;
            if (x > y)
            {
                float fHeight = (y / x) * particlesResolution;
                _mNParticlesWidth = particlesResolution;
                _mNParticlesHeight = (int) fHeight;
            }
            else
            {
                float fWidth = (x / y) * particlesResolution;
                _mNParticlesWidth = (int) fWidth;
                _mNParticlesHeight = particlesResolution;
            }

            SetSizeInShaders();
            particleAreaShader.GetKernelThreadGroupSizes(0, out var groupSizeX, out _, out _);
            _mNParticlesNumGroupsX = Mathf.CeilToInt((float) _mNParticlesWidth / groupSizeX);
            _mNParticlesNumGroupsY = Mathf.CeilToInt((float) _mNParticlesHeight / groupSizeX);
            m_currentParticles = new float[_mNParticlesWidth * _mNParticlesHeight];
        }

        private void LinkToFluidSimulation()
        {
            var fResolutionRatio = resolution / (float) particlesResolution;
            SetSize((int) (_mNParticlesWidth * fResolutionRatio), (int) (_mNParticlesHeight * fResolutionRatio));

            InitShaders();
            particleAreaShader.SetInts(_VelocitySize, GetWidth(), GetHeight());
        }

        private void FlipBuffers()
        {
            (READ, WRITE) = (WRITE, READ);
        }

        public Vector2 GetRenderSize() => m_tempRend.bounds.size;

        public int GetParticlesWidth() => _mNParticlesWidth;

        public int GetParticlesHeight() => _mNParticlesHeight;

        public void UpdateGradient()
        {
            var colourData = new Vector4[m_nColourRampSize];
            for (var i = 0; i < m_nColourRampSize; ++i)
                colourData[i] = m_colourGradient.Evaluate(i / 255.0f);

            m_colourRamp.SetData(colourData);
        }

        private void SetSizeInShaders()
        {
            particleAreaShader.SetInts(_ParticleSize, _mNParticlesWidth, _mNParticlesHeight);
            m_tempRend.material.SetVector(Size, new Vector2(_mNParticlesWidth, _mNParticlesHeight));
        }

        public void AddParticles(Vector2 position, float fRadius, float fStrength)
        {
            if (particleAreaShader != null && _mParticlesBuffer != null && _mParticlesBuffer.Length >= 2)
            {
                float[] pos = {position.x, position.y};
                particleAreaShader.SetFloats(Position, pos);
                particleAreaShader.SetFloat(_Value, fStrength);
                particleAreaShader.SetFloat(Radius, fRadius);
                particleAreaShader.SetInts(Size, new int[] {_mNParticlesWidth, _mNParticlesHeight});
                particleAreaShader.SetBuffer(m_addParticlesKernel, _ParticlesIn, _mParticlesBuffer[READ]);
                particleAreaShader.SetBuffer(m_addParticlesKernel, _ParticlesOut, _mParticlesBuffer[WRITE]);
                particleAreaShader.Dispatch(m_addParticlesKernel, _mNParticlesNumGroupsX, _mNParticlesNumGroupsY, 1);
                FlipBuffers();
                m_tempRend.material.SetBuffer(_Particles, _mParticlesBuffer[READ]);
            }
        }
    }

    public enum Quality
    {
        Low,
        Medium,
        High
    }
}