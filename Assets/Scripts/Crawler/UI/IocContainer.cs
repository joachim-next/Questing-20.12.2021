using System;
using System.Collections.Generic;

namespace DefaultNamespace
{
    public static class IocContainer
    {
        private static Dictionary<Type, object> _instances = new Dictionary<Type, object>();

        public static void RegisterSingleton<T>(T obj)
        {
            var type = typeof(T);

            _instances[type] = obj;
        }
        
        public static T GetSingleton<T>()
        {
            var type = typeof(T);

            if (!_instances.ContainsKey(type))
            {
                throw new InvalidOperationException($"Instance of {type} hasn't been registered.");
            }
            
            return (T) _instances[type];
        }
    }
}