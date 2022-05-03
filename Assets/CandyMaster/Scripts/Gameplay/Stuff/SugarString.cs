using System.Threading.Tasks;
using CandyMaster.Scripts.Gameplay.Component;
using CandyMaster.Scripts.Gameplay.Interfaces;
using UnityEngine;

namespace CandyMaster.Scripts.Stuff
{
    [ExecuteAlways]
    [RequireComponent(typeof(MovableComponent))]
    public sealed class SugarString : MonoBehaviour, ISugarString
    {
        [SerializeField] private Vector3 twist;
        [SerializeField] private Transform parentBone;
        [SerializeField] private new Renderer renderer;

        [SerializeField] private Color color;


        private MovableComponent _movableComponent;
        private MaterialPropertyBlock _materialPropertyBlock;

        private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");


        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        public Vector3 Twist
        {
            get => twist;
            set => twist = value;
        }

        public Color Color
        {
            get => color;
            set
            {
                color = value;
                _materialPropertyBlock.SetColor(BaseColor, value);
                renderer.SetPropertyBlock(_materialPropertyBlock);
            }
        }

        private void Awake()
        {
            _movableComponent = GetComponent<MovableComponent>();
            _materialPropertyBlock = new MaterialPropertyBlock();
        }

#if UNITY_EDITOR
        private void Update()
        {
            TwistBody(parentBone);
        }
#endif


        private void TwistBody(Transform bone)
        {
            bone.localRotation = Quaternion.Euler(twist);
            for (var i = 0; i < bone.childCount; i++) TwistBody(bone.GetChild(i));
        }

        public Task MoveTo(Vector3 position) => _movableComponent.MoveTo(position);

        public void Dispose()
        {
            print($"{name} has been disposed");
            renderer.enabled = false;
            var gm = gameObject;
            gm.SetActive(false);
            Destroy(gm);
        }
    }
}