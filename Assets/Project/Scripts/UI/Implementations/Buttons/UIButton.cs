using System;

namespace CandyMasters.Project.Scripts.UI.Implementations.Buttons
{
    public class UIButton : Core.Button
    {
        private Action _onClick;


        public new void Initialize(Action onClick)
        {
            _onClick = onClick;
            base.Initialize(_onClick.Invoke);
        }
    }
}