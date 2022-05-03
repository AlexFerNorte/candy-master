using System;
using UnityEngine;

namespace CandyMaster.Project.Scripts.Common.Classes
{
    [Serializable]
    public class Range
    {
        [field: SerializeField] public float Min { get; set; }
        [field: SerializeField] public float Max { get; set; }

        
        public Range(float min = 0f, float max = 0f)
        {
            Min = min;
            Max = max;
        }
    }
}