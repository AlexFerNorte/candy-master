using UnityEngine;

namespace CandyMasters.Project.Scripts.Common.Interfaces
{
    public interface IActor
    {
        public void Locate(Vector3 position);
        public void Rotate(Quaternion rotation);
        public void Transformize(Vector3 position, Quaternion rotation);
        
        /*public void OnCollectableContact(CollisionType receiver, Collectable collectable);
        public void OnDamageableContact(CollisionType receiver, Damageable damageable);
        public void OnTriggerContact(CollisionType receiver, Trigger trigger);
        public void OnTriggerStayIn(CollisionType receiver, Trigger trigger);
        public void OnTriggerExitFrom(CollisionType receiver, Trigger trigger);*/
    }
}