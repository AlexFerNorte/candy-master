using System.Collections.Generic;
using System.Threading.Tasks;
using CandyMaster.Scripts.Gameplay.Component;
using CandyMaster.Scripts.Gameplay.Interfaces;
using DG.Tweening;
using Obi;
using UnityEngine;
using UnityEngine.Serialization;

namespace CandyMaster.Scripts.Gameplay.Stuff
{
    [RequireComponent(typeof(MovableComponent))]
    public class Pot : MonoBehaviour, IPot
    {
        [SerializeField, Range(0, 1)] private float fill;

        [FormerlySerializedAs("_emitter")] [Header("Fluid")] [SerializeField]
        private ObiEmitter emitter;

        [SerializeField] private float killParticlesBelow = 0.1f;

        [SerializeField] private ObiSolver obiSolver;


        [SerializeField] private ObiParticleRenderer particleRenderer;
        [SerializeField] private float emitPower = 1;

        [Header("Burn")] [SerializeField] private Gradient burnGradient;
        [SerializeField, Range(0, 1)] private float burn;


        [SerializeField] private GameObject invisibleCap;

        [Header("Out")] [SerializeField] private List<Vector3> angles;
        [SerializeField] private Transform rotateObject;
        [SerializeField] private float rotateDuration = 0.3f;
        [SerializeField] private float holdDuration = 0.2f;
        [SerializeField] private float backDuration = 0.2f;

        [SerializeField] private float portionAmount;


        private MovableComponent _movableComponent;

        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        public float Burn
        {
            get => burn;
            set => burn = Mathf.Clamp(value, 0, 1);
        }

        public float Fill
        {
            get => fill;
            set => fill = Mathf.Clamp(value, 0, 1);
        }

        public Color FluidColor => particleRenderer.particleColor;

        [ContextMenu(nameof(FillOut))]
        public async Task FillOut()
        {
            fill -= portionAmount;

            var nextAngle = angles[0];
            angles.RemoveAt(0);

            var sequence = DOTween.Sequence()
                .Append(rotateObject.DOLocalRotate(nextAngle, rotateDuration))
                .AppendInterval(holdDuration)
                .Append(rotateObject.DOLocalRotate(Vector3.zero, backDuration));

            while (sequence.IsPlaying()) await Task.Yield();
        }

        private void Awake()
        {
            _movableComponent = GetComponent<MovableComponent>();
            invisibleCap.SetActive(false);

            portionAmount = 1f / angles.Count;

            obiSolver.OnCollision += ObiSolverOnOnCollision;
        }

        private void OnDestroy()
        {
            obiSolver.OnCollision -= ObiSolverOnOnCollision;
        }

        private void ObiSolverOnOnCollision(ObiSolver solver, ObiSolver.ObiCollisionEventArgs contacts)
        {
            return;
            for (var i = 0; i < contacts.contacts.Count; i++)
            {
                var contact = contacts.contacts[i];
                //contact.
                //print($"Collided {contact.bodyA} with {contact.bodyB}");
            }
        }

        private void Update()
        {
            var current = (float) emitter.activeParticleCount / emitter.particleCount;
            emitter.speed = current < fill ? emitPower : 0;
            particleRenderer.particleColor = burnGradient.Evaluate(burn);

            for (var i = emitter.activeParticleCount - 1; i >= 0; i--)
            {
                var index = emitter.GetParticleRuntimeIndex(i);
                if (emitter.GetParticlePosition(index).y < killParticlesBelow)
                    emitter.KillParticle(index);
            }
        }

        private void LateUpdate()
        {
            var emitterPose = emitter.transform.position;
            obiSolver.transform.position = Vector3.zero;
            emitter.transform.position = emitterPose;
        }

        public async Task MoveTo(Vector3 position)
        {
            invisibleCap.SetActive(true);
            await _movableComponent.MoveTo(position);
            invisibleCap.SetActive(false);
        }

        public void Dispose() => Destroy(gameObject);
    }
}