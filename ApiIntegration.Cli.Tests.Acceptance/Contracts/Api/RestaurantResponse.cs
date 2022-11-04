using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ApiIntegration.Cli.Tests.Acceptance.Contracts.Api {
    public record RestaurantResponse {
        [JsonPropertyName("name")]
        public string Name { get; init; } = default!;

        [JsonPropertyName("ratingStars")]
        public string Rating { get; init; } = default!;

        [JsonPropertyName("cuisineTypes")]
        public IReadOnlyList<CuisineTypeResponse> CuisineTypes { get; init; } = default!;
    }
}