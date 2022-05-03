using System;

namespace CandyMasters.Project.Scripts.Common.Extensions
{
    public static class BoolExtension
    {
        public static void Ternary(this bool predicate, Action onTrue = null, Action onFalse = null)
        {
            if (predicate)
            {
                onTrue?.Invoke();
            }
            else
            {
                onFalse?.Invoke();
            }
        }
    }
}