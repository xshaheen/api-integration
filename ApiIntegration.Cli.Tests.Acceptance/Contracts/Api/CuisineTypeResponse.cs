using System.Text.Json.Serialization;

namespace ApiIntegration.Cli.Tests.Acceptance.Contracts.Api;

public sealed record CuisineTypeResponse
{
    [JsonPropertyName("name")]
    public required string Name { get; init; }
}