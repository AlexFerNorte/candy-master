using System.Threading.Tasks;
using CandyMaster.Scripts.Gameplay;
using CandyMaster.Scripts.Gameplay.Interfaces;
using CandyMaster.Scripts.Gameplay.Steps;
using CandyMaster.Scripts.Gameplay.Utils;
using CandyMaster.Scripts.Interfaces;
using CandyMaster.Scripts.UI.Input;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Playables;

namespace CandyMaster.Scripts.Steps
{
    public class RollStep : AbstractStep
    {
        [Header("Candy")] [SerializeField] private GameObject rollingCandy;

        [SerializeField] private float appearDuration = .3f;
        [SerializeField] private Ease appearEase = Ease.InBounce;
        
        [Space] [SerializeField] private ParticleSystem hideEffect;


        [Space] [SerializeField] private PlayableDirector director;
        [SerializeField, Range(0, 1)] private float progress;
        [SerializeField] private float speed;
        private SwipeUpDownInput _swipeUpDownInput;
        private ICandyThread _candyThread;
        private TwistStep _twistStep;


        public override void Init()
        {
            _swipeUpDownInput = this.GetOrException<SwipeUpDownInput>();
            _candyThread = this.GetOrException<ICandyThread>();
            _twistStep = this.GetOrException<TwistStep>();

            rollingCandy.transform.localScale = Vector3.zero;
            director.enabled = false;
        }

        public override async Task ExecuteStep()
        {
            var tween = rollingCandy.transform.DOScale(1, appearDuration).SetEase(appearEase);
            _candyThread.SetColors(_twistStep.ColorsSet);
            hideEffect.Play();
            foreach (var sugarString in _twistStep.SugarStrings) sugarString.Dispose();

            while (tween.IsPlaying()) await Task.Yield();

            director.enabled = true;
            director.Play();

            progress = 0;
            
            _swipeUpDownInput.Show = true;
            _swipeUpDownInput.SwipeDelta += SwipeUpDownInputOnSwipeDelta;
            
            TutorialHand.PointAt(transform.position, ITutorialHand.Mode.Clock);

            while (progress <= 1) await Task.Yield();
            
            TutorialHand.Hide();
            
            _swipeUpDownInput.Show = false;
            _swipeUpDownInput.SwipeDelta -= SwipeUpDownInputOnSwipeDelta;
        }

        private void SwipeUpDownInputOnSwipeDelta(float obj)
        {
            var power = Mathf.Abs(obj * Time.deltaTime);
            
            progress += power * speed;
            director.time = director.duration * progress;
            director.Evaluate();
        }

        protected override void DisposeSelf()
        {
            //ignore
        }
    }
}