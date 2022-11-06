﻿namespace Fitness.Common.Extensions;

public static class ParserTypes
{
    public static Guid ParseToGuidOrThrow(this string? input)
    {
        if (string.IsNullOrEmpty(input))
        {
            throw new InvalidOperationException("String is null or empty.");
        }
        return Guid.TryParse(input, out var result) ? result : throw new InvalidOperationException("Parse failed.");
    }
}