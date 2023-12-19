using System.Collections.Generic;

namespace ApiIntegration.Cli.Tests.Acceptance.Contracts.Api;

public sealed record RestaurantSearchResponse
{
    public required IReadOnlyList<RestaurantResponse> Restaurants { get; init; }
}