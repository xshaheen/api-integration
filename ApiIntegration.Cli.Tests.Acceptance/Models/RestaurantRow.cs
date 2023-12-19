namespace ApiIntegration.Cli.Tests.Acceptance.Models;

public sealed class RestaurantRow
{
    public required string Name { get; init; }

    public required string Rating { get; init; }

    public required string CuisineType { get; init; }
}