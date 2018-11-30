namespace EverymanETL.Custom
{
    using EverymanETL.DataProviders;
    using System;
    using System.Collections.Generic;

    public static class ProviderResolver
    {
        private static Dictionary<Type, Func<object>> _DBProviders = new Dictionary<Type, Func<object>>();

        public static void RegisterDatabaseProvider<T>(Func<object> creationMethod)
        {
            _DBProviders.Add(typeof(T), creationMethod);
        }

        public static IDataProvider<T> ResolveDatabaseProvider<T>()
        {
            return (IDataProvider<T>)_DBProviders[typeof(T)]();
        }
    }

    public static class APIResolver<T>
    {
        private static Dictionary<Type, Func<HashSet<T>>> _APIProviders = new Dictionary<Type, Func<HashSet<T>>>();

        public static void RegisterDataProvider(Func<HashSet<T>> providerMethod)
        {
            _APIProviders.Add(typeof(T), providerMethod);
        }

        public static HashSet<T> ResolveData()
        {
            return _APIProviders[typeof(T)]();
        }
    }
}
