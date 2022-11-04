using System.Text.Json.Serialization;

namespace ApiIntegration.Cli.Tests.Acceptance.Contracts.Api {
    public record CuisineTypeResponse {
        [JsonPropertyName("name")]
        public string Name { get; init; } = default!;
    }
}