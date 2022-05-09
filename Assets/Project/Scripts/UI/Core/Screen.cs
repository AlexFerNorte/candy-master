using System;
using CandyMaster.Project.Scripts.Events;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace CandyMaster.Project.Scripts.UI.Core
{
    public class Screen : MonoBehaviour
    {
        [SerializeField] private TMP_Text title;
        [SerializeField] protected float transitionTime;

        protected StageEvents StageEvents { get; private set; }


        public virtual void Initialize(StageEvents stageEvents) => StageEvents = stageEvents;

        public virtual void Appear()
        {
            gameObject.SetActive(true);
            AppearTitle();
        }

        public virtual void Disappear()
        {
            DisappearTitle(() => gameObject.SetActive(false));
        }

        private void AppearTitle()
        {
            title.transform.localScale = Vector3.zero;
            title.transform.DOScale(Vector3.one, transitionTime);
        }

        private void DisappearTitle(Action onComplete = null)
        {
            title.transform.DOScale(Vector3.zero, transitionTime).OnComplete(() => onComplete?.Invoke());
        }
    }
}