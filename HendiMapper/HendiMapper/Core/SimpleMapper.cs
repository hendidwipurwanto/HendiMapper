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

        var destinationPropertyLookup = PropertyCache
            .GetPropertyLookup(typeof(TDestination));

        foreach (var sourceProp in sourceProperties)
        {
            var propertyFound = destinationPropertyLookup
                .TryGetValue(sourceProp.Name, out var destProp);

            if (!propertyFound)
                continue;

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
            var isComplexType =
                !sourceProp.PropertyType.IsPrimitive &&
                sourceProp.PropertyType != typeof(string) &&
                !sourceProp.PropertyType.IsEnum;

            if (destProp.PropertyType != sourceProp.PropertyType &&
                !isComplexType)
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
                if (value != null &&
                !sourceProp.PropertyType.IsPrimitive &&
                sourceProp.PropertyType != typeof(string) &&
                !sourceProp.PropertyType.IsEnum &&
                destProp.PropertyType != sourceProp.PropertyType)
                {
                    var nestedMethod = typeof(SimpleMapper)
                        .GetMethod(nameof(Map))!
                        .MakeGenericMethod(destProp.PropertyType);

                    var nestedMappedObject = nestedMethod
                        .Invoke(null, new[] { value });

                    destProp.SetValue(destination, nestedMappedObject);

                    continue;
                }
                //
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