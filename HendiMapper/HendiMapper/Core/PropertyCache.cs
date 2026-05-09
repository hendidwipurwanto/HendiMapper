using System.Reflection;
using System.Collections.Concurrent;

namespace HendiMapper.Core;

public static class PropertyCache
{
    private static readonly ConcurrentDictionary<Type, PropertyInfo[]>
        Cache = new();

    public static PropertyInfo[] GetProperties(Type type)
    {
        return Cache.GetOrAdd(type, t =>
        {
            return t.GetProperties(
                BindingFlags.Public |
                BindingFlags.Instance);
        });
    }
}