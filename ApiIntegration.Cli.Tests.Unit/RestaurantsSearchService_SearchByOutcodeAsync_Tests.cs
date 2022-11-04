using System.Threading.Tasks;
using ApiIntegration.Cli.Api;
using ApiIntegration.Cli.Api.Responses;
using ApiIntegration.Cli.Mapping;
using ApiIntegration.Cli.Models;
using ApiIntegration.Cli.Services;
using AutoFixture;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace ApiIntegration.Cli.Tests.Unit {
    public class RestaurantsSearchService_SearchByOutcodeAsync_Tests {
        private readonly RestaurantsSearchService _sut;

        private readonly IRestaurantApi _restaurantApi = Substitute.For<IRestaurantApi>();
        private readonly IValidator<RestaurantSearchRequest> _validator = new RestaurantSearchRequest.Validator();
        private readonly IFixture _fixture = new Fixture();

        public RestaurantsSearchService_SearchByOutcodeAsync_Tests() {
            _sut = new RestaurantsSearchService(_restaurantApi, _validator);
        }

        [Fact]
        public async Task should_return_result_when_outcode_is_valid() {
            // arrange
            const string outcode = "E2";
            var req = new RestaurantSearchRequest(outcode);
            var res = _fixture.Create<RestaurantSearchResponse>();
            _restaurantApi.SearchByPostcodeAsync(outcode).Returns(Task.FromResult(res));

            // act
            var result = await _sut.SearchByOutcodeAsync(req);

            // assert
            result.AsT0.Should().BeEquivalentTo(
                res.MapToRestaurantResult(),
                options => options.ComparingByMembers<RestaurantSearchResult>().ComparingByMembers<RestaurantResult>()
            );
        }

        [Fact]
        public async Task should_return_errors_when_outcode_is_invalid() {
            // arrange
            const string outcode = "34E2";
            var req = new RestaurantSearchRequest(outcode);

            // act
            var result = await _sut.SearchByOutcodeAsync(req);

            // assert
            result.AsT1.Errors.Should().BeEquivalentTo("Please provide a valid UK Outcode.");
        }
    }
}