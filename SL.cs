using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DS.Models;

namespace SL
{
    public static class SL
    {
        private static readonly Dictionary<Type, object> Services = new();
        private static readonly HashSet<Type> Initialized = new();

        public static void Register<T>(T service) where T : class, IService
        {
            Services[typeof(T)] = service;
        }

        public static async UniTask<Result> InitAsync<T>() where T : class, IService
        {
            var type = typeof(T);
            if (!Services.ContainsKey(type))
            {
                return Result.Failure($"Service {type.Name} not registered");
            }

            if (Initialized.Contains(type))
            {
                return Result.Success();
            }

            if (Services[type] is IService service)
            {
                var result = await service.InitAsync();
                if (result.IsSuccess)
                {
                    Initialized.Add(type);
                    return Result.Success();
                }

                return Result.Failure($"Failed to initialize service {type.Name}: {result.ErrorMessage}");
            }

            return Result.Success();
        }

        public static T Get<T>() where T : class, IService
        {
            var type = typeof(T);
            if (!Services.ContainsKey(type))
            {
                throw new InvalidOperationException($"Service {type.Name} not registered");
            }

            return (T)Services[type];
        }

        public static bool Contains<T>() => Services.ContainsKey(typeof(T));
        public static bool IsInitialized<T>() => Initialized.Contains(typeof(T));

        public static void Clear()
        {
            Services.Clear();
            Initialized.Clear();
        }
    }
}