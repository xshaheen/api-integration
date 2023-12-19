using System.Text.Json;
using System.Threading.Tasks;
using ApiIntegration.Cli.Models;
using ApiIntegration.Cli.Services;
using AutoFixture;
using NSubstitute;
using Xunit;

namespace ApiIntegration.Cli.Tests.Unit;

public sealed class RestaurantsSearchApplicationTests
{
    private readonly RestaurantsSearchApplication _sut;
    private readonly IConsoleWriter _consoleWriter = Substitute.For<IConsoleWriter>();
    private readonly IRestaurantsSearchService _restaurantsSearchService = Substitute.For<IRestaurantsSearchService>();
    private readonly IFixture _fixture = new Fixture();
    private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };

    public RestaurantsSearchApplicationTests()
    {
        _sut = new RestaurantsSearchApplication(_consoleWriter, _restaurantsSearchService);
    }

    [Fact]
    public async Task should_return_write_restaurants_when_outcode_is_valid()
    {
        // arrange
        const string outcode = "E2";
        var args = new[] { "--o", outcode };

        var restaurantResult = _fixture.Create<RestaurantSearchResult>();

        _restaurantsSearchService
            .SearchByOutcodeAsync(new RestaurantSearchRequest(outcode))
            .Returns(restaurantResult);

        var expectedText = JsonSerializer.Serialize(restaurantResult, JsonOptions);

        // act
        await _sut.RunAsync(args);

        // assert
        _consoleWriter.Received(1).WriteLine(Arg.Is(expectedText));
    }

    [Fact]
    public async Task should_return_write_errors_when_outcode_is_invalid()
    {
        // arrange
        const string outcode = "E2 ARR3";
        var args = new[] { "--o", outcode };
        var errors = new[] { "Please provide a valid UK Outcode." };

        _restaurantsSearchService
            .SearchByOutcodeAsync(new RestaurantSearchRequest(outcode))
            .Returns(new RestaurantSearchErrors(errors));

        // act
        await _sut.RunAsync(args);

        // assert
        _consoleWriter.Received(1).WriteLine(Arg.Is(string.Join(", ", errors)));
    }
}