using System;
using System.Collections.Generic;

namespace SL
{
    public static class SL
    {
        private static readonly Dictionary<Type, object> Services = new();

        public static void Register<T>(T service) where T : class
        {
            Services[typeof(T)] = service;
        }

        public static T Get<T>() where T : class
        {
            var type = typeof(T);
            if (!Services.ContainsKey(type))
            {
                throw new InvalidOperationException($"Service {type.Name} not registered");
            }

            return (T)Services[type];
        }

        public static bool Contains<T>() => Services.ContainsKey(typeof(T));

        public static void Clear()
        {
            Services.Clear();
        }
    }
}