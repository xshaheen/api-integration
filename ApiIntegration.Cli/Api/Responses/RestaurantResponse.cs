using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ApiIntegration.Cli.Api.Responses;

public record RestaurantResponse
{
    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("ratingStars")]
    public string Rating { get; init; }

    [JsonPropertyName("cuisineTypes")]
    public IReadOnlyList<CuisineTypeResponse> CuisineTypes { get; init; }
}