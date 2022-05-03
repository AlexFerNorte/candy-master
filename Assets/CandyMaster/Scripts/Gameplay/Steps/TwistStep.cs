using System;
using System.Threading.Tasks;
using CandyMaster.Scripts.Gameplay.Interfaces;
using CandyMaster.Scripts.Gameplay.Interfaces.Coloring;
using CandyMaster.Scripts.Gameplay.Utils;
using CandyMaster.Scripts.Stuff;
using CandyMaster.Scripts.UI.Input;
using UnityEngine;

namespace CandyMaster.Scripts.Gameplay.Steps
{
    public class TwistStep : AbstractStep
    {
        [SerializeField] private Transform tutorialPoint;
        
        [SerializeField] private SwipeUpDownInput swipeUpDownInput;

        [SerializeField] private Posing[] stringStartPositions;

        [Header("Strings")] [SerializeField] private float moveToPositionsPower = 5;
        [SerializeField] private float maxFinishDelta = 0.2f;

        [Header("Thread")] [SerializeField] private SugarString thread;
        [SerializeField] private float twistPower = 0.001f;
        [SerializeField] private float twistFinishDelta = 0.001f;
        [SerializeField] private Vector3 endTwistValue;
        
        [Space]
        [SerializeField] private Step currentStep = Step.Move;

        [SerializeField] private Color[] colorSet;
        

        private ISugarString[] _sugarStrings;

        public ISugarString[] SugarStrings => _sugarStrings;

        public Color[] ColorsSet => colorSet;

        public override void Init()
        {
            swipeUpDownInput = FindObjectOfType<SwipeUpDownInput>();
            thread.gameObject.SetActive(false);
        }

        public override async Task ExecuteStep()
        {
            {
                var bricks = this.FindMultiple<IPaintableBrick>();
                _sugarStrings = new ISugarString[bricks.Length];
                colorSet = new Color[bricks.Length];
                for (var i = 0; i < bricks.Length; i++)
                {
                    _sugarStrings[i] = bricks[i].GetNewPaintedSugarString();
                    colorSet[i] = _sugarStrings[i].Color;
                }
            }

            for (var i = 0; i < _sugarStrings.Length; i++)
                await _sugarStrings[i].MoveTo(stringStartPositions[i].Start.position);

            swipeUpDownInput.Show = true;
            swipeUpDownInput.SwipeDelta += SwipeUpDownInputOnSwipeDelta;
            
            TutorialHand.PointAt(tutorialPoint.position, ITutorialHand.Mode.UpDown);

            while (currentStep == Step.Move) await Task.Yield();

            TutorialHand.Hide();

            swipeUpDownInput.Show = false;
            swipeUpDownInput.SwipeDelta -= SwipeUpDownInputOnSwipeDelta;
        }

        protected override void DisposeSelf()
        {
            print($"Disposed {GetType().Name}");
            foreach (var sugarString in _sugarStrings) sugarString.Dispose();
        }

        private void SwipeUpDownInputOnSwipeDelta(float obj)
        {
            var power = Mathf.Abs(obj * Time.deltaTime);
            var allDone = true;

            switch (currentStep)
            {
                case Step.Move:
                    power *= moveToPositionsPower;

                    for (var i = 0; i < _sugarStrings.Length; i++)
                    {
                        var sugarString = _sugarStrings[i];
                        var endPosition = stringStartPositions[i].End.position;
                        sugarString.Position =
                            Vector3.MoveTowards(sugarString.Position, endPosition, power);

                        allDone = allDone && Vector3.Distance(sugarString.Position, endPosition) <= maxFinishDelta;
                    }

                    if (allDone)
                        currentStep = Step.Thread;
                    break;
                case Step.Thread:
                    power *= twistPower;
                    thread.Twist = Vector3.MoveTowards(thread.Twist, endTwistValue, power);
                    allDone = Vector3.Distance(thread.Twist, endTwistValue) <= twistFinishDelta;

                    if (allDone)
                        currentStep = Step.Done;
                    break;
                case Step.Done:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        [Serializable]
        public struct Posing
        {
            public Transform Start, End;
        }

        public enum Step
        {
            Move,
            Thread,
            Done
        }
    }
}