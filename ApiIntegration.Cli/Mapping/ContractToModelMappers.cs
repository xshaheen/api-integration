using System.Linq;
using ApiIntegration.Cli.Api.Responses;
using ApiIntegration.Cli.Models;

namespace ApiIntegration.Cli.Mapping;

public static class ContractToModelMappers
{
    public static RestaurantSearchResult MapToRestaurantResult(this RestaurantSearchResponse response)
    {
        return new RestaurantSearchResult
        {
            Restaurants = response.Restaurants.Select(res => new RestaurantResult
            {
                Name = res.Name,
                Rating = res.Rating,
                CuisineTypes = res.CuisineTypes.Select(c => c.Name).ToList(),
            }).ToList(),
        };
    }
}