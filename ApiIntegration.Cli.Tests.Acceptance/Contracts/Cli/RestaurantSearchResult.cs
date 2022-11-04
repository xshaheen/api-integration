using System.Collections.Generic;

namespace ApiIntegration.Cli.Tests.Acceptance.Contracts.Cli {
    public record RestaurantSearchResult {
        public IReadOnlyList<RestaurantResult> Restaurants { get; init; } = default!;
    }
}