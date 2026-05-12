using System.Collections.Concurrent;
using System.Reflection;

namespace HendiMapper.Core;

public static class PropertyCache
{
    private static readonly ConcurrentDictionary<Type, PropertyInfo[]>
        PropertyCacheDictionary = new();

    private static readonly ConcurrentDictionary<Type, Dictionary<string, PropertyInfo>>
        PropertyLookupCache = new();

    public static PropertyInfo[] GetProperties(Type type)
    {
        return PropertyCacheDictionary.GetOrAdd(type, t =>
        {
            return t.GetProperties(
                BindingFlags.Public |
                BindingFlags.Instance);
        });
    }

    public static Dictionary<string, PropertyInfo>
        GetPropertyLookup(Type type)
    {
        return PropertyLookupCache.GetOrAdd(type, t =>
        {
            return t.GetProperties(
                    BindingFlags.Public |
                    BindingFlags.Instance)
                .ToDictionary(x => x.Name);
        });
    }
}