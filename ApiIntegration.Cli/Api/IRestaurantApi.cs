using System.Threading.Tasks;
using ApiIntegration.Cli.Api.Responses;
using Refit;

namespace ApiIntegration.Cli.Api;

public interface IRestaurantApi
{
    [Get("/restaurants/by-postcode/{postcode}")]
    Task<RestaurantSearchResponse> SearchByPostcodeAsync(string postcode);
}