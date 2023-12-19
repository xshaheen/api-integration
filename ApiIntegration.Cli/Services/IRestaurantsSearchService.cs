using System.Threading.Tasks;
using ApiIntegration.Cli.Models;
using OneOf;

namespace ApiIntegration.Cli.Services;

public interface IRestaurantsSearchService
{
    Task<OneOf<RestaurantSearchResult, RestaurantSearchErrors>> SearchByOutcodeAsync(RestaurantSearchRequest request);
}