using UnityEngine;

namespace CandyMasters.Project.Scripts.Objects.Core
{
    public abstract class Interactable : MonoBehaviour
    {
        protected bool IsExecuted { get; set; }
        protected bool IsStayIn { get; private set; }
        

        private void OnTriggerEnter(Collider c)
        {

        }

        private void OnTriggerStay(Collider c)
        {

        }

        private void OnTriggerExit(Collider c)
        {

        }

        /*protected virtual void OnCollisionInteraction(CollisionObject collisionObject) {}
        protected virtual void OnCollisionStayIn(CollisionObject collisionObject) {}
        protected virtual void OnCollisionCameOut(CollisionObject collisionObject) {}*/
        
        
        #region Visibility
        public virtual void Appear() => gameObject.SetActive(true);
        
        public virtual void Disappear() => gameObject.SetActive(false);
        #endregion
    }
}