using System.Collections.Generic;

namespace ApiIntegration.Cli.Models;

public sealed record RestaurantSearchResult
{
    public IReadOnlyList<RestaurantResult> Restaurants { get; init; }
}