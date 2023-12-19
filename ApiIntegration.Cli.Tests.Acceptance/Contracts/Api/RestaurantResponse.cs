using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ApiIntegration.Cli.Tests.Acceptance.Contracts.Api;

public sealed record RestaurantResponse
{
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("ratingStars")]
    public required string Rating { get; init; }

    [JsonPropertyName("cuisineTypes")]
    public required IReadOnlyList<CuisineTypeResponse> CuisineTypes { get; init; }
}