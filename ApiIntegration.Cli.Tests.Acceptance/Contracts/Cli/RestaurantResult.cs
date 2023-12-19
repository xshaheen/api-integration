using System.Collections.Generic;

namespace ApiIntegration.Cli.Tests.Acceptance.Contracts.Cli;

public sealed record RestaurantResult
{
    public required string Name { get; init; }

    public required string Rating { get; init; }

    public required IReadOnlyList<string> CuisineTypes { get; init; }
}