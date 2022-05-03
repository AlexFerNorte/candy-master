using FluidDynamics.Scripts;
using UnityEngine;

namespace FluidDynamics
{
    [AddComponentMenu("Fluid Dynamics/Emitters/Fluid Velocity Emitter")]
    public class FluidDynamicsVelocityEmitter : MonoBehaviour
    {
        public MainFluidSimulation m_fluid;
        public bool m_velocityFromMovement = false;
        public float m_fluidVelocitySpeed = 1f;
        public float m_scaleVelocity = 1f;
        public float m_radius = 0.1f;
        public bool m_showGizmo = false;

        private Vector3 m_direction;
        private Vector3 m_speed;
        private Vector3 m_prevPosition;
        private Collider m_tempCol;
        private Renderer m_tempRend;
        private Ray ray;
        private RaycastHit hitInfo;
        private float fWidth;
        private float fRadius;

        public MainFluidSimulation MainFluidSimulation
        {
            set => m_fluid = value;
        }

        private void Start()
        {
            m_tempCol = m_fluid.GetComponent<Collider>();
            m_tempRend = m_fluid.GetComponent<Renderer>();
            m_prevPosition = transform.position;
            m_direction = GetDirection();
        }

        public float GetRadius() => m_radius;

        public Vector3 GetDirection()
        {
            if (m_velocityFromMovement)
            {
                return transform.position - m_prevPosition;
            }

            return transform.rotation * Vector3.down;
        }

        private void UpdateValues()
        {
            m_direction = GetDirection();
            if (m_direction != Vector3.zero)
            {
                m_direction.Normalize();
                m_speed = m_direction * m_fluidVelocitySpeed * Time.deltaTime;
            }
            else
                m_speed = Vector3.zero;

            m_prevPosition = transform.position;
        }

        private void Update() => UpdateValues();

        private void LateUpdate()
        {
            if (m_fluid && m_speed != Vector3.zero)
            {
                ray = new Ray(transform.position, Vector3.forward);
                if (m_tempCol.Raycast(ray, out hitInfo, 10))
                {
                    fWidth = m_tempRend.bounds.extents.x * 2f;
                    fRadius = (GetRadius() * m_fluid.GetWidth()) / fWidth;
                    m_fluid.AddVelocity(hitInfo.textureCoord, -m_speed, fRadius);
                }
            }
        }

        private void DrawGizmo()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, GetRadius());
            if (!m_velocityFromMovement || Application.isPlaying)
            {
                var end_pos = transform.position - m_direction * (2f + (m_fluidVelocitySpeed / 500f) * 5f);
                Gizmos.color = Color.cyan;
                Gizmos.DrawLine(transform.position, end_pos);
                var back_dir = (transform.position - end_pos);
                back_dir.Normalize();
                const float angle = 25 * Mathf.Deg2Rad;
                var cos = Mathf.Cos(angle);
                var sin = Mathf.Sin(angle);
                var arrow = new Vector3(back_dir.x * cos - back_dir.y * sin,
                    back_dir.x * sin + back_dir.y * cos, 0f);
                Gizmos.DrawLine(end_pos, end_pos + arrow * 0.5f);
                cos = Mathf.Cos(-angle);
                sin = Mathf.Sin(-angle);
                arrow = new Vector3(back_dir.x * cos - back_dir.y * sin,
                    back_dir.x * sin + back_dir.y * cos, 0f);
                Gizmos.DrawLine(end_pos, end_pos + arrow * 0.5f);
            }
        }

        private void OnDrawGizmosSelected() => DrawGizmo();

        private void OnDrawGizmos()
        {
            if (m_showGizmo) DrawGizmo();
        }
    }
}