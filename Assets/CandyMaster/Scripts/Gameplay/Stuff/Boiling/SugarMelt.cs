using CandyMaster.Scripts.Gameplay.Interfaces;
using UnityEngine;

namespace CandyMaster.Scripts.Gameplay.Stuff.Boiling
{
    [ExecuteAlways]
    public class SugarMelt : MonoBehaviour, ISugarMelt
    {
        [SerializeField] private Transform[] steps;
        [SerializeField, Range(0, 1)] private float progress;
        [SerializeField] private Vector3[] defaultScales;

        [SerializeField] private bool executeEditMode = false;


        public float Progress
        {
            set
            {
                progress = 1 - value;
                UpdateProgress();
            }
        }

        private void Start()
        {
            defaultScales = new Vector3[steps.Length];
            for (var i = 0; i < steps.Length; i++)
                defaultScales[i] = steps[i].localScale;
        }

        private void UpdateProgress()
        {
            var progressPart = 1f / steps.Length;
            for (var i = 0; i < steps.Length; i++)
            {
                var part = steps[i];

                var progress = Mathf.Clamp((this.progress - progressPart * i) / progressPart, 0, 1);

                part.localScale = defaultScales[i] * progress;
            }
        }

        private void Update()
        {
            if (executeEditMode && Application.isEditor) UpdateProgress();
        }
    }
}