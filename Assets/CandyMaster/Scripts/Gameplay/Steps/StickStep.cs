using System;
using System.Threading.Tasks;
using CandyMaster.Scripts.Gameplay;
using CandyMaster.Scripts.Gameplay.Interfaces;
using DG.Tweening;
using UnityEngine;

namespace CandyMaster.Scripts.Steps
{
    public class StickStep : AbstractStep
    {
        [SerializeField] private GameObject stick;
        [SerializeField] private Transform endStickPosition;
        [SerializeField] private float stickAppearsDuration = 0.5f;
        [SerializeField] private Ease stickAppearsEase = Ease.InBounce;

        public override void Init()
        {
            if (stick is null || endStickPosition is null)
                throw new NullReferenceException();
        }

        public override async Task ExecuteStep()
        {
            TutorialHand.PointAt(transform.position, ITutorialHand.Mode.Simple);
            
            Tween tween;
            while (true)
            {
                if (Input.anyKeyDown)
                {
                    TutorialHand.Hide();
                    stick.transform.SetParent(endStickPosition, false);
                    tween = stick.transform
                        .DOLocalMove(Vector3.zero, stickAppearsDuration)
                        .SetEase(stickAppearsEase);
                    break;
                }

                await Task.Yield();
            }

            while (tween.IsPlaying()) await Task.Yield();
        }

        protected override void DisposeSelf()
        {
            //ignore
        }
    }
}