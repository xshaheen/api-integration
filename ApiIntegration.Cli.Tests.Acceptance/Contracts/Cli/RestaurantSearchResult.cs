using System.Collections.Generic;

namespace ApiIntegration.Cli.Tests.Acceptance.Contracts.Cli;

public sealed record RestaurantSearchResult
{
    public required IReadOnlyList<RestaurantResult> Restaurants { get; init; }
}