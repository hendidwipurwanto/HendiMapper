using HendiMapper.Core;
using HendiMapper.Exceptions;

namespace HendiMapper.Extensions;

public static class MergeExtensions
{
    public static TDestination Merge<TDestination>(this object? source) where TDestination : new()
    {
        if (source == null)
            throw new MapperException(
                "Source object cannot be null.");

        return SimpleMapper.Map<TDestination>(source);
    }

    public static TDestination Merge<TSource, TDestination>(
        this TDestination destination,
        TSource source)
    {
        return SimpleMapper.Merge(destination, source);
    }

    public static List<TDestination> Merge<TSource, TDestination>(
        this IEnumerable<TSource> source)
        where TDestination : new()
    {
        return source
            .Select(x => x.Merge<TDestination>())
            .ToList();
    }
}