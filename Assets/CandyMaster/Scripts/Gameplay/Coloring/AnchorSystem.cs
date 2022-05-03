using System;
using UnityEngine;

namespace CandyMaster.Scripts.Coloring
{
    public class AnchorSystem : MonoBehaviour
    {
        [SerializeField] private Anchors headerAnchors, followerAnchors;
        [SerializeField] private Transform leader, follower;
        [SerializeField] private Vector3 value;

        public Transform Leader => leader;
        
        public Transform Follower => follower;
        
        private void Update()
        {
            var position = leader.localPosition;

            var delta = headerAnchors.GetDelta();
            var rawValue = GetDelta(headerAnchors.startAnchor, position);
            value.x = DivideOrZero(rawValue.x, delta.x);
            value.y = DivideOrZero(rawValue.y, delta.y);
            value.z = DivideOrZero(rawValue.z, delta.z);

            follower.localPosition = followerAnchors.startAnchor + Vector3.Scale(followerAnchors.GetDelta(), value);
        }

        [Serializable]
        public class Anchors
        {
            public Vector3 startAnchor;
            public Vector3 endAnchor;

            public Vector3 GetDelta() => AnchorSystem.GetDelta(startAnchor, endAnchor);
        }

        private static float DivideOrZero(in float value, in float divider) =>
            Mathf.Approximately(divider, 0) ? 0 : value / divider;

        private static Vector3 GetDelta(in Vector3 v1, in Vector3 v2)
        {
            var deltaX = Mathf.Abs(v1.x - v2.x);
            var deltaY = Mathf.Abs(v1.y - v2.y);
            var deltaZ = Mathf.Abs(v1.z - v2.z);

            return new Vector3(deltaX, deltaY, deltaZ);
        }
    }
}