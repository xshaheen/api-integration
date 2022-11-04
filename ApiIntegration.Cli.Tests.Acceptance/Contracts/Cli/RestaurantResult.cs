using System.Collections.Generic;

namespace ApiIntegration.Cli.Tests.Acceptance.Contracts.Cli {
    public record RestaurantResult {
        public string Name { get; init; } = default!;

        public string Rating { get; init; } = default!;

        public IReadOnlyList<string> CuisineTypes { get; init; } = default!;
    }
}