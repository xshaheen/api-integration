using System.Linq;
using System.Threading.Tasks;
using ApiIntegration.Cli.Api;
using ApiIntegration.Cli.Mapping;
using ApiIntegration.Cli.Models;
using FluentValidation;
using OneOf;

namespace ApiIntegration.Cli.Services {
    public class RestaurantsSearchService : IRestaurantsSearchService {
        private readonly IRestaurantApi _restaurantApi;
        private readonly IValidator<RestaurantSearchRequest> _validator;

        public RestaurantsSearchService(
            IRestaurantApi restaurantApi,
            IValidator<RestaurantSearchRequest> validator
        ) {
            _restaurantApi = restaurantApi;
            _validator = validator;
        }

        public async Task<OneOf<RestaurantSearchResult, RestaurantSearchErrors>> SearchByOutcodeAsync(RestaurantSearchRequest request) {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid) {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return new RestaurantSearchErrors(errors);
            }

            var response = await _restaurantApi.SearchByPostcodeAsync(request.Outcode);

            return response.MapToRestaurantResult();
        }
    }
}
