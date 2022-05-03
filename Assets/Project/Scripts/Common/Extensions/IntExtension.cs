using System;

namespace CandyMasters.Project.Scripts.Common.Extensions
{
    public static class IntExtension
    {
        public static int InRange(this int value, int min, int max, Action onEntry = null)
        {
            if (value < min)
            {
                value = min;
            }
            
            if (value > max)
            {
                value = max;
            }

            onEntry?.Invoke();
            return value;
        }
    }
}