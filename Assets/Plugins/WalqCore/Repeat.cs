using System;

namespace CandyMasters.Project.Scripts.Common.Packages.WalqCore
{
    public class Repeat
    {
        private readonly Action _action;
        private readonly int _iterations;
        
        
        public Repeat(Action action, int iterations)
        {
            _action = action;
            _iterations = iterations;
        }
        
        public void Start()
        {
            for (int i = 0; i < _iterations; i++)
            {
                _action.Invoke();
            }
        }
    }
}