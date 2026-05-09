using System.Reflection;

namespace HendiMapper.Core;

public static class SimpleMapper
{
    public static TDestination Map<TDestination>(object source)
        where TDestination : new()
    {
        if (source == null)
            return default!;

        var destination = new TDestination();

        Merge(destination, source);

        return destination;
    }

    public static TDestination Merge<TDestination, TSource>(
        TDestination destination,
        TSource source)
    {
        if (destination == null || source == null)
            return destination;

        // FIX BUG HERE
        var sourceProperties = source.GetType()
            .GetProperties(BindingFlags.Public | BindingFlags.Instance);

        var destinationProperties = typeof(TDestination)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var sourceProp in sourceProperties)
        {
            var destProp = destinationProperties
                .FirstOrDefault(x =>
                    x.Name == sourceProp.Name &&
                    x.PropertyType == sourceProp.PropertyType &&
                    x.CanWrite);

            if (destProp == null)
                continue;

            var value = sourceProp.GetValue(source);

            if (value == null)
                continue;

            destProp.SetValue(destination, value);
        }

        return destination;
    }
}