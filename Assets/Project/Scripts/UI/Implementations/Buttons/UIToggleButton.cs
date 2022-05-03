using System;

namespace CandyMaster.Project.Scripts.UI.Implementations.Buttons
{
    public class UIToggleButton : Core.Button
    {
        private Action _onToggleOn;
        private Action _onToggleOff;
        private bool _state;

        
        public void Initialize(Action onToggleOn, Action onToggleOff)
        {
            base.Initialize(Toggle);
            _onToggleOn = onToggleOn;
            _onToggleOff = onToggleOff;
        }

        private void Toggle()
        {
            if (_state)
            {
                _onToggleOff.Invoke();
            }
            else
            {
                _onToggleOn.Invoke();
            }
            
            _state = !_state;
        }
    }
}