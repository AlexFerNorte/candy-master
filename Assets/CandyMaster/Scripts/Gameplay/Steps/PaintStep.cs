using System.Collections.Generic;
using System.Threading.Tasks;
using CandyMaster.Scripts.Gameplay.Coloring.PaintTubeSystem;
using CandyMaster.Scripts.Gameplay.Interfaces;
using CandyMaster.Scripts.Gameplay.Interfaces.Coloring;
using CandyMaster.Scripts.Gameplay.Interfaces.Tubes;
using CandyMaster.Scripts.Gameplay.Utils;
using CandyMaster.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.Assertions;

namespace CandyMaster.Scripts.Gameplay.Steps
{
    public class PaintStep : AbstractStep
    {
        [SerializeField] private Transform startTubePosition;

        private IPot _pot;
        private ITube _tube;
        private PaintTubeManager _paintTubeManager;
        private ITubePanel _tubePanel;
        private IPaintPanel _paintPanel;
        private IPaintableBrick[] _bricks;

        private ISugarForm[] _sugarForms;

        private bool _nextRequested;

        public override void Init()
        {
            _pot = this.FindOrException<IPot>();
            _tube = this.FindOrException<ITube>();
            _tubePanel = this.FindOrException<ITubePanel>();
            _paintPanel = this.FindOrException<IPaintPanel>();

            _sugarForms = this.FindMultiple<ISugarForm>();

            _paintTubeManager = GetComponentInChildren<PaintTubeManager>();

            _paintTubeManager.enabled = false;
        }

        public override async Task ExecuteStep()
        {
            _tubePanel.ColorSelected += TubePanelOnColorSelected;

            _paintPanel.Done += PaintPanelOnDone;
            _paintPanel.Reset += PaintPanelOnReset;

            var awaitList = new List<Task<IPaintableBrick>>(_sugarForms.Length);
            foreach (var sugarForm in _sugarForms) awaitList.Add(sugarForm.Chill(_pot.FluidColor));

            await Task.WhenAll(awaitList);

            _bricks = new IPaintableBrick[awaitList.Count];
            for (var i = 0; i < _bricks.Length; i++)
            {
                _bricks[i] = awaitList[i].Result;
                _bricks[i].HasColored+= OnHasColored;
            }

#if UNITY_EDITOR
            foreach (var t in _bricks) Assert.IsNotNull(t);
            print($"Count {_bricks.Length}");
#endif

            _nextRequested = false;
            _tubePanel.Show = true;
            _paintPanel.Show = true;

            _tube.PaintColor = _tubePanel.Selected;
            await _tube.MoveTo(startTubePosition.position);

            _paintTubeManager.enabled = true;

            TutorialHand.PointAt(_bricks[Random.Range(0, _bricks.Length)].Position, ITutorialHand.Mode.Simple);

            while (!_nextRequested) await Task.Yield();

            _tubePanel.ColorSelected -= TubePanelOnColorSelected;

            _paintPanel.Done -= PaintPanelOnDone;
            _paintPanel.Reset -= PaintPanelOnReset;

            _tubePanel.Show = false;
            _paintPanel.Show = false;

            _paintTubeManager.enabled = false;
        }

        private void OnHasColored() => TutorialHand.Hide();

        protected override void DisposeSelf()
        {
            //TODO
        }

        private void TubePanelOnColorSelected(Color obj) => _tube.PaintColor = obj;

        private void PaintPanelOnDone() => _nextRequested = true;

        private void PaintPanelOnReset()
        {
            foreach (var paintableBrick in _bricks) paintableBrick.ResetPaint();
        }
    }
}