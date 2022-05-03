using System;

namespace CandyMaster.Project.Scripts.Common.Extensions
{
    public static class ArrayExtension
    {
        public static void For<T>(this T[] array, Action<T, int> action)
        {
            for (int i = 0; i < array.Length; i++)
            {
                action.Invoke(array[i], i);
            }
        }

        public static void ForEach<T>(this T[] array, Action<T> action)
        {
            for (int i = 0; i < array.Length; i++)
            {
                action.Invoke(array[i]);
            }
        }
    }
}