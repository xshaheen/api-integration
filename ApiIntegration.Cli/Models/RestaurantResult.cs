using System.Collections.Generic;

namespace ApiIntegration.Cli.Models;

public sealed record RestaurantResult
{
    public string Name { get; init; }

    public string Rating { get; init; }

    public IReadOnlyList<string> CuisineTypes { get; init; }
}