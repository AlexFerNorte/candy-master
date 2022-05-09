using System;
using UnityEngine;

namespace CandyMaster.Project.Scripts.Common.Classes
{
    [Serializable]
    public class ColorRange
    {
        [field: SerializeField] public Color Start { get; set; }
        [field: SerializeField] public bool UseCenter { get; set; }
        [field: SerializeField] public Color Center { get; set; }
        [field: SerializeField] public Color End { get; set; }


        public Color Lerp(float percent)
        {
            Color result = new Color();

            if (UseCenter)
            {
                if (percent <= 0.5f)
                {
                    result = Blend(Start, Center, percent);
                }
            
                if (percent > 0.5f)
                {
                    result = Blend(Center, End, (percent - 0.5f) * 2);
                }
            }
            else
            {
                result = Blend(Start, End, percent);
            }

            return result;
        }

        private Color Blend(Color a, Color b, float percent) => new Color
        {
            r = Mathf.Lerp(a.r, b.r, percent),
            g = Mathf.Lerp(a.g, b.g, percent),
            b = Mathf.Lerp(a.b, b.b, percent),
            a = Mathf.Lerp(a.a, b.a, percent)
        };
    }
}