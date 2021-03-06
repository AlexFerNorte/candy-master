using FluidDynamics.Scripts;
using FluidDynamics.Scripts.Emitters;
using UnityEngine;
namespace FluidDynamics
{
    [AddComponentMenu("Fluid Dynamics/Obstacles/Fluid Edge Obstacle")]
    [RequireComponent(typeof(EdgeCollider2D))]
    public class Fluid_Dynamics_Edge_Obstacle : MonoBehaviour
    {
        States updateMode = States.LateUpdate;
        public MainFluidSimulation m_fluid;
        public float distance = 10;
        private bool m_bInitialised = false;
        public bool m_isStatic = false;
        private EdgeCollider2D m_collider;
        private Collider m_tempCol;
        private Ray ray1;
        private RaycastHit h1;
        private Ray ray2;
        private RaycastHit h2;
        private Ray ray3;
        private RaycastHit h3;
        private Vector2[] points;

        private void Start()
        {
            m_tempCol = m_fluid.GetComponent<Collider>();
            m_collider = GetComponent<EdgeCollider2D>();
        }
        private void Update()
        {
            if (updateMode == States.Update)
                StartBlock();
        }
        private void FixedUpdate()
        {
            if (updateMode == States.FixedUpdate)
                StartBlock();
        }
        private void LateUpdate()
        {
            if (updateMode == States.LateUpdate)
                StartBlock();
        }
        private void StartBlock()
        {
            if (!m_bInitialised)
            {
                if (m_isStatic)
                {
                    Block(true);
                    Debug.Log("sdf");
                }
                m_bInitialised = true;
            }
            if (!m_isStatic)
            {
                Block(false);
            }
        }
        private void Block(bool bStatic)
        {
            if (m_collider && m_fluid)
            {
                points = m_collider.points;
                int size = points.Length;
                if (size >= 3)
                {
                    ray1 = new Ray(transform.TransformPoint(points[0]), Vector3.forward);
                    if (m_tempCol.Raycast(ray1, out h1, distance))
                    {
                        ray2 = new Ray(transform.TransformPoint(points[1]), Vector3.forward);
                        if (m_tempCol.Raycast(ray2, out h2, distance))
                        {
                            for (int i = 2; i < size; ++i)
                            {
                                ray3 = new Ray(transform.TransformPoint(points[i]), Vector3.forward);
                                if (m_tempCol.Raycast(ray3, out h3, distance))
                                {
                                    m_fluid.AddObstacleTriangle(h1.textureCoord, h2.textureCoord, h3.textureCoord, bStatic);
                                }
                                h2 = h3;
                            }
                        }
                    }
                }
            }
        }
    }
}