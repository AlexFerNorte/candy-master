using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Tiq.Plugins.Tiq.Core;

namespace Tiq.Plugins.Tiq.Common
{
    public class Tiq
    {
        private readonly List<Instruction> _currentInstructions;

        public ReadOnlyCollection<Instruction> CurrentInstructions => _currentInstructions.AsReadOnly();

        
        public Tiq()
        {
            _currentInstructions = new List<Instruction>();
        }


        public void Start(Instruction instruction, Action onComplete = null)
        {
            _currentInstructions.Add(instruction);
            instruction.Start();
            instruction.OnComplete(onComplete);
            OnStart();
        }
        
        public void Stop(Instruction instruction)
        {
            _currentInstructions.Add(instruction);
            instruction.Stop();
            OnStop();
        }

        protected virtual void OnStart()
        {
            
        }

        protected virtual void OnStop()
        {
            
        }
    }
}