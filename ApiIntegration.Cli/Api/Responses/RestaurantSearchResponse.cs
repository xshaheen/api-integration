using System.Collections.Generic;

namespace ApiIntegration.Cli.Api.Responses {
    public record RestaurantSearchResponse {
        public IReadOnlyList<RestaurantResponse> Restaurants { get; init; }
    }
}
