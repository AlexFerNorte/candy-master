using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CandyMaster.Project.Scripts.Common.Classes
{
    [Serializable]
    public struct Curve3
    {
        #region SerializedProperties
        [field: SerializeField] public AnimationCurve X { get; private set; }
        [field: SerializeField] public AnimationCurve Y { get; private set; }
        [field: SerializeField] public AnimationCurve Z { get; private set; }
        [field: SerializeField] public Range XSpeedRange { get; private set; }
        [field: SerializeField] public Range YSpeedRange { get; private set; }
        [field: SerializeField] public Range ZSpeedRange { get; private set; }
        #endregion

        #region CustomProperties
        public Vector3 ActiveSpeeds { get; set; }

        public Vector3 RandomActiveSpeeds => new Vector3
        {
            x = Random.Range(XSpeedRange.Min, XSpeedRange.Max),
            y = Random.Range(YSpeedRange.Min, YSpeedRange.Max),
            z = Random.Range(ZSpeedRange.Min, ZSpeedRange.Max)
        };
        #endregion
    }
}