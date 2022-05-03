using System;
using System.Collections.Generic;
using System.Linq;
using CandyMaster.Scripts.Gameplay.Interfaces;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CandyMaster.Scripts.Gameplay.Utils
{
    public static class Extensions
    {
        [CanBeNull]
        public static T FindThe<T>(this MonoBehaviour monoBehaviour) where T : class =>
            Object.FindObjectsOfType<MonoBehaviour>().FirstOrDefault(b => b is T) as T;
        
        
        [CanBeNull]
        public static ISugarForm FirstUnfilled(this IEnumerable<ISugarForm> forms)
        {
            foreach (var sugarForm in forms)
                if (!sugarForm.IsFull)
                    return sugarForm;
            return null;
        }

        public static T FindOrException<T>(this MonoBehaviour monoBehaviour) where T : class =>
            Object.FindObjectsOfType<MonoBehaviour>().FirstOrDefault(b => b is T) as T
            ?? throw new NullReferenceException($"Object of type {typeof(T).Name} not found");

        public static T GetOrException<T>(this GameObject gameObject) where T : class =>
            Object.FindObjectsOfType<MonoBehaviour>().FirstOrDefault(b => b is T) as T
            ?? throw new NullReferenceException($"Object of type {typeof(T).Name} not found");

        public static T GetOrException<T>(this MonoBehaviour monoBehaviour) where T : class =>
            GetOrException<T>(monoBehaviour.gameObject);

        public static T[] FindMultiple<T>(this MonoBehaviour monoBehaviour) where T : class =>
            Object.FindObjectsOfType<MonoBehaviour>().OfType<T>().ToArray();

        public static T[] GetMultipleInChildren<T>(this MonoBehaviour monoBehaviour) where T : class =>
            monoBehaviour.GetComponentsInChildren<MonoBehaviour>().OfType<T>().ToArray();
    }
}