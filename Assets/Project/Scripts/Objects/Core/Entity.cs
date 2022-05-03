using CandyMaster.Project.Scripts.Data;
using CandyMaster.Project.Scripts.Data.Immutable;
using UnityEngine;

namespace CandyMaster.Project.Scripts.Objects.Core
{
    public abstract class Entity<TInitializeData> : MonoBehaviour
        where TInitializeData : InitializeData
    {
        protected TInitializeData InitializeData;


        #region Initialization
        public virtual void Initialize(TInitializeData initializeData)
        {
            InitializeData = initializeData;
            
            InitializeVariables(initializeData);
            InitializeInstructions(initializeData);
        }

        protected abstract void InitializeVariables(TInitializeData initializeData);

        public abstract void ResetVariables();

        protected abstract void InitializeInstructions(TInitializeData initializeData);

        public abstract void ResetInstructions();
        #endregion
    }
}