using CandyMaster.Scripts.Interfaces.Utils;
using DG.Tweening;
using UnityEngine;

namespace CandyMaster.Scripts.Gameplay.Component
{
    public class ShowableUIComponent : MonoBehaviour, IShowable
    {
        [SerializeField] private float showDuration = 0.2f;
        [SerializeField] private Vector3 standardScale = Vector3.one;
        [SerializeField] private bool show = false;
        [SerializeField] private bool hideOnStart = true;

        public bool Show
        {
            get => show;
            set
            {
                show = value;
                transform.DOScale(value ? standardScale : Vector3.zero, showDuration);
            }
        }

        private void Start()
        {
            if (hideOnStart)
                transform.localScale = Vector3.zero;
        }
    }
}