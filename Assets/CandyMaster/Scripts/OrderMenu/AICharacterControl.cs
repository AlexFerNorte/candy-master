using System.Collections.Generic;
using UnityEngine;

namespace CandyMaster.Scripts.OrderMenu
{
    public class AICharacterControl : MonoBehaviour
    {
        [SerializeField] private float m_moveSpeed = 2;
        [SerializeField] private float m_turnSpeed = 200;
        [SerializeField] private float m_jumpForce = 4;

        [SerializeField] private Animator m_animator;
        [SerializeField] private Rigidbody m_rigidBody;
        [SerializeField] private Vector3 target;
        

        private float _currentV;
        private float _currentH;

        private readonly float m_interpolation = 10;
        private readonly float m_walkScale = 0.33f;
        private readonly float m_backwardsWalkScale = 0.16f;
        private readonly float m_backwardRunScale = 0.66f;

        private Vector3 _currentDirection = Vector3.zero;

        private float _jumpTimeStamp = 0;
        private float _minJumpInterval = 0.25f;
        private bool _jumpInput;

        private bool _isGrounded;

        private readonly List<Collider> _collisions = new List<Collider>();
        private static readonly int Grounded = Animator.StringToHash("Grounded");

        private Camera _camera;
        private static readonly int MoveSpeed = Animator.StringToHash("MoveSpeed");

        private void Awake()
        {
            m_animator = GetComponent<Animator>();
            m_rigidBody = GetComponent<Rigidbody>();
            _camera = Camera.main;
        }

        private void OnCollisionEnter(Collision collision)
        {
            var contactPoints = collision.contacts;
            for (int i = 0; i < contactPoints.Length; i++)
            {
                if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
                {
                    if (!_collisions.Contains(collision.collider))
                    {
                        _collisions.Add(collision.collider);
                    }

                    _isGrounded = true;
                }
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            var contactPoints = collision.contacts;
            var validSurfaceNormal = false;
            for (var i = 0; i < contactPoints.Length; i++)
                if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
                {
                    validSurfaceNormal = true;
                    break;
                }

            if (validSurfaceNormal)
            {
                _isGrounded = true;
                if (!_collisions.Contains(collision.collider))
                    _collisions.Add(collision.collider);
            }
            else
            {
                if (_collisions.Contains(collision.collider))
                    _collisions.Remove(collision.collider);

                if (_collisions.Count == 0)
                    _isGrounded = false;
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (_collisions.Contains(collision.collider))
                _collisions.Remove(collision.collider);

            if (_collisions.Count == 0)
                _isGrounded = false;
        }


        private void Update()
        {
            if (!_jumpInput && Input.GetKey(KeyCode.Space))
                _jumpInput = true;
        }

        private void FixedUpdate()
        {
            m_animator.SetBool(Grounded, _isGrounded);
            MoveToPosition(target);

            _jumpInput = false;
        }

        private void MoveToPosition(Vector3 target)
        {
            var direction = target - transform.position;
            direction.y = 0;
            direction.Normalize();

            if (direction != Vector3.zero)
            {
                _currentDirection = Vector3.Slerp(_currentDirection, direction, Time.deltaTime * m_interpolation);

                transform.rotation = Quaternion.LookRotation(_currentDirection);
                transform.position += _currentDirection * m_moveSpeed * Time.deltaTime;

                m_animator.SetFloat(MoveSpeed, direction.magnitude);
            }
        }
    }
}