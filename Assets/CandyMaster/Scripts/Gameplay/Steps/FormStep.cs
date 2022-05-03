using System;
using System.Threading.Tasks;
using CandyMaster.Scripts.Gameplay.Interfaces;
using CandyMaster.Scripts.Gameplay.Stuff;
using CandyMaster.Scripts.Gameplay.Utils;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CandyMaster.Scripts.Gameplay.Steps
{
    public class FormStep : AbstractStep
    {
        [SerializeField] private Vector3 potMoveOffset;
        [SerializeField] private float fillDuration = 1;
        [SerializeField] private Transform startPotPosition, endPotPosition;

        private IPot _pot;
        private ISugarForm[] _forms;

        private bool _executing;
        private bool _isAnyFilling;

        private ISugarForm _currentPointing;

        public override void Init()
        {
            _pot = this.FindOrException<IPot>();
            _forms = this.GetMultipleInChildren<ISugarForm>();
        }

        public override async Task ExecuteStep()
        {
            await _pot.MoveTo(startPotPosition.position);

            foreach (var sugarForm in _forms) sugarForm.Clicked += SugarFormOnClicked;

            _executing = true;

            SetCurrentPointing(_forms[Random.Range(0, _forms.Length)]);

            while (_executing)
            {
                var everyIsFull = true;
                for (var i = 0; i < _forms.Length; i++)
                    everyIsFull = everyIsFull && _forms[i].IsFull;
                if (!_isAnyFilling && everyIsFull)
                    break;

                await Task.Yield();
            }

            foreach (var sugarForm in _forms) sugarForm.Clicked -= SugarFormOnClicked;

            await _pot.MoveTo(endPotPosition.position);
        }

        protected override void DisposeSelf()
        {
            _pot.Dispose();
            //ignore
        }

        private void SetCurrentPointing(ISugarForm sugarForm)
        {
            _currentPointing = sugarForm;
            TutorialHand.PointAt(_currentPointing.Position, ITutorialHand.Mode.Simple);
        }

        private async void SugarFormOnClicked(ISugarForm obj)
        {
            if (obj.IsFull || _isAnyFilling) return;

            _isAnyFilling = true;
            await _pot.MoveTo(obj.Position + potMoveOffset);
            obj.FillForFull(fillDuration, _pot.FluidColor);
            //await Task.Delay(TimeSpan.FromSeconds(fillDuration));
            await _pot.FillOut();
            _isAnyFilling = false;

            if (obj == _currentPointing)
            {
                var next = _forms.FirstUnfilled();
                if (next == null)
                    TutorialHand.Hide();
                else
                    SetCurrentPointing(next);
            }
        }
    }
}