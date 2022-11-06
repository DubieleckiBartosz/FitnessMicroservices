﻿namespace Fitness.Common.Extensions;

public static class ListExtensions
{
    public static List<T> Replace<T>(this List<T> list, T existingElement, T replacement)
    {
        var indexOfExistingItem = list.IndexOf(existingElement);

        if (indexOfExistingItem == -1)
        {
            throw new ArgumentOutOfRangeException(nameof(existingElement), "Element was not found");
        }

        list[indexOfExistingItem] = replacement;

        return list;
    }
}