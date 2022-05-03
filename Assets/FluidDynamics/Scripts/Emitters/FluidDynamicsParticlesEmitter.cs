using UnityEngine;

namespace FluidDynamics.Scripts.Emitters
{
    public enum States
    {
        Update = 0,
        LateUpdate = 1,
        FixedUpdate = 2
    }

    [AddComponentMenu("Fluid Dynamics/Emitters/Fluid Particles Emitter")]
    public class FluidDynamicsParticlesEmitter : MonoBehaviour
    {
        public States updateMode;
        public MainFluidSimulation m_mainSimulation;
        public float m_radius = 0.1f;
        public float m_strength = 1f;
        public bool m_showGizmo = false;
        private Collider m_tempCol;
        private Renderer m_tempRend;
        private Ray ray;
        private RaycastHit hitInfo;
        private bool bdone = false;
        private float fWidth;
        private float fRadius;

        private void Start()
        {
            m_tempCol = m_mainSimulation.GetComponent<Collider>();
            m_tempRend = m_mainSimulation.GetComponent<Renderer>();
        }

        private void Update()
        {
            if (updateMode == States.Update)
                ManipulateParticles();
        }

        private void LateUpdate()
        {
            if (updateMode == States.LateUpdate)
                ManipulateParticles();
        }

        private void FixedUpdate()
        {
            if (updateMode == States.FixedUpdate)
                ManipulateParticles();
        }

        public float GetRadius() => m_radius;

        private void ManipulateParticles()
        {
            if (m_mainSimulation && !bdone)
            {
                ray = new Ray(transform.position, Vector3.forward);
                if (m_tempCol.Raycast(ray, out hitInfo, 10))
                {
                    fWidth = m_tempRend.bounds.extents.x * 2f;
                    fRadius = (GetRadius() * m_mainSimulation.GetParticlesWidth()) / fWidth;
                    m_mainSimulation.AddParticles(hitInfo.textureCoord, fRadius, m_strength * Time.deltaTime);
                }
            }
        }

        private void DrawGizmo()
        {
            var col = m_strength / 10000.0f;
            Gizmos.color = Color.Lerp(Color.yellow, Color.red, col);
            Gizmos.DrawWireSphere(transform.position, GetRadius());
        }

        private void OnDrawGizmosSelected() => DrawGizmo();

        private void OnDrawGizmos()
        {
            if (m_showGizmo) DrawGizmo();
        }
    }
}