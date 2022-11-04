using System.Collections.Generic;

namespace ApiIntegration.Cli.Models {
    public record RestaurantSearchResult {
        public IReadOnlyList<RestaurantResult> Restaurants { get; init; }
    }
}
