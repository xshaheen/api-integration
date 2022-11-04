using System.Collections.Generic;

namespace ApiIntegration.Cli.Tests.Acceptance.Contracts.Api {
    public record RestaurantSearchResponse {
        public IReadOnlyList<RestaurantResponse> Restaurants { get; init; } = default!;
    }
}