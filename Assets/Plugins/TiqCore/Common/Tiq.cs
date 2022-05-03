using System.Collections.Generic;
using System.Collections.ObjectModel;
using Plugins.TiqCore.Core;
using Plugins.TiqCore.Implementations.Instructions;

namespace Plugins.TiqCore.Common
{
    public class Tiq
    {
        private List<Instruction> _currentInstructions;

        public ReadOnlyCollection<Instruction> CurrentInstructions => _currentInstructions.AsReadOnly();

        
        public Tiq()
        {
            _currentInstructions = new List<Instruction>();
        }


        public void Start(Instruction instruction)
        {
            _currentInstructions.Add(instruction);
            instruction.Start();
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