using System;
using System.Collections;
using System.Collections.Generic;

namespace Source.Common
{
    public static class DI
    {
        private static readonly Dictionary<Type, object> Singletons = new ();
        private static readonly Dictionary<Type, IList> Multiples = new();

        public static bool TryGetSingleton<T>(out T instance)
            where T : class
        {
            if (Singletons.TryGetValue(typeof(T), out var objInstance))
            {
                instance = objInstance as T;
                return true;
            }
            instance = null;
            return false;
        }

        public static T GetSingleton<T>()
            where T : class
        {
            if(!TryGetSingleton<T>(out var instance))
                UnityEngine.Debug.LogException(new ArgumentException($"Can't access {typeof(T).Name} singleton"));
            return instance;
        }

        public static void DIRegisterSingleton<T>(this T instance)
            where T : class 
            => RegisterSingleton(typeof(T), instance);

        public static void RegisterSingleton(Type type, object instance)
        {
            if(!Singletons.TryAdd(type, instance))
                throw new ArgumentException($"Instance of type {type.Name} is already registered.");
        }

        public static void DIRemoveSingleton<T>(this T instance)
            where T : class 
            => RemoveSingleton(typeof(T));

        public static void RemoveSingleton(Type type) 
            => Singletons.Remove(type);

        public static void DIRegisterMultiple<T>(this T instance)
        {
            var type = typeof(T);
            if (!Multiples.TryGetValue(type, out var instList))
            {
                instList = new List<T>();
                Multiples.Add(type, instList);
            }
            instList.Add(instance);
        }

        public static IReadOnlyCollection<T> GetMultiple<T>()
            where T : class
        {
            if(!Multiples.TryGetValue(typeof(T), out var instList))
                UnityEngine.Debug.LogException(new ArgumentException($"Can't find any {typeof(T).Name}"));
            return instList as List<T>;
        }
    }
}