using System.Text.Json.Serialization;

namespace ApiIntegration.Cli.Api.Responses {
    public record CuisineTypeResponse {
        [JsonPropertyName("name")]
        public string Name { get; init; }
    }
}
