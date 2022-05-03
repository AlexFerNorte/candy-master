using CandyMasters.Project.Scripts.Data;
using UnityEngine;

namespace CandyMasters.Project.Scripts.Objects.Core
{
    public abstract class Entity<TInitializeData> : MonoBehaviour
        where TInitializeData : InitializeData
    {
        protected TInitializeData InitializeData;


        #region Initialization
        public virtual void Initialize(TInitializeData initializeData)
        {
            InitializeData = initializeData;
        }

        protected abstract void InitializeVariables(TInitializeData initializeData);

        public abstract void ResetVariables();

        protected abstract void InitializeInstructions(TInitializeData initializeData);

        public abstract void ResetInstructions();
        #endregion
    }
}