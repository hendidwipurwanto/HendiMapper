using System.Reflection;
using HendiMapper.Exceptions;

namespace HendiMapper.Core;

public static class SimpleMapper
{
    public static TDestination Map<TDestination>(object source)
        where TDestination : new()
    {
        if (source == null)
            throw new MapperException(
                "Source object cannot be null.");

        var destination = new TDestination();

        Merge(destination, source);

        return destination;
    }

    public static TDestination Merge<TDestination, TSource>(
        TDestination destination,
        TSource source)
    {
        if (destination == null)
            throw new MapperException(
                "Destination object cannot be null.");

        if (source == null)
            throw new MapperException(
                "Source object cannot be null.");

        var sourceProperties = PropertyCache
    .GetProperties(source.GetType());

        var destinationProperties = PropertyCache
            .GetProperties(typeof(TDestination));

        foreach (var sourceProp in sourceProperties)
        {
            var destProp = destinationProperties
                .FirstOrDefault(x => x.Name == sourceProp.Name);

            // property not found
            if (destProp == null)
                continue;

            // readonly property
            if (!destProp.CanWrite)
            {
                throw new MapperException(
                    $"Property '{destProp.Name}' on type '{typeof(TDestination).Name}' is readonly.");
            }

            // strict type validation
            if (destProp.PropertyType != sourceProp.PropertyType)
            {
                throw new MapperException(
                    $"Property type mismatch for '{sourceProp.Name}'. " +
                    $"Source type: '{sourceProp.PropertyType.Name}', " +
                    $"Destination type: '{destProp.PropertyType.Name}'.");
            }

            var value = sourceProp.GetValue(source);

            // skip null
            if (value == null)
                continue;

            try
            {
                destProp.SetValue(destination, value);
            }
            catch (Exception ex)
            {
                throw new MapperException(
                    $"Failed to map property '{sourceProp.Name}'. " +
                    $"Error: {ex.Message}");
            }
        }

        return destination;
    }
}