using CandyMaster.Project.Scripts.Objects.Core;
using UnityEngine;

namespace CandyMaster.Project.Scripts.Objects.Implementations.Entities.Player
{
    public class Player : Entity<PlayerInitializeData>
    {
        #region Initialization
        protected override void InitializeVariables(PlayerInitializeData initializeData)
        {

        }

        public override void ResetVariables()
        {

        }

        protected override void InitializeInstructions(PlayerInitializeData initializeData)
        {

        }

        public override void ResetInstructions()
        {

        }
        #endregion
        
        
        #region Common
        public void Locate(Vector3 position) => transform.position = position;
        
        public void Rotate(Quaternion rotation) => transform.rotation = rotation;
        
        public void Transformize(Vector3 position, Quaternion rotation)
        {
            Locate(position);
            Rotate(rotation);
        }
        #endregion
    }
}