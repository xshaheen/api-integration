using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ApiIntegration.Cli.Tests.Acceptance.Contracts.Api;
using ApiIntegration.Cli.Tests.Acceptance.Contracts.Cli;
using ApiIntegration.Cli.Tests.Acceptance.Models;
using BoDi;
using FluentAssertions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace ApiIntegration.Cli.Tests.Acceptance.Features;

[Binding]
public sealed class SearchForRestaurantByOutcodeSteps
{
    private Process _cli = default!;
    private string _outcode = default!;
    private List<RestaurantRow> _setupRestaurants = default!;

    private readonly IObjectContainer _objectContainer;

    public SearchForRestaurantByOutcodeSteps(IObjectContainer objectContainer)
    {
        _objectContainer = objectContainer;
    }

    [Given("the outcode (.*)")]
    public void GivenTheOutcode(string outcode)
    {
        _outcode = outcode;
    }

    [Given("the following restaurants that are delivering there")]
    public void GivenTheFollowingRestaurantsThatAreDeliveringThere(Table table)
    {
        _setupRestaurants = table.CreateSet<RestaurantRow>().ToList();

        var searchApiResponse = new RestaurantSearchResponse
        {
            Restaurants = _setupRestaurants.Select(r => new RestaurantResponse
            {
                Name = r.Name,
                Rating = r.Rating,
                CuisineTypes = new CuisineTypeResponse[] { new() { Name = r.CuisineType } },
            }).ToList(),
        };

        var server = _objectContainer.Resolve<WireMockServer>();

        var request = Request.Create()
            .WithPath($"/restaurants/by-postcode/{_outcode}")
            .UsingGet();

        var response = Response.Create()
            .WithStatusCode(200)
            .WithBody(JsonSerializer.Serialize(searchApiResponse));

        server.Given(request).RespondWith(response);
    }

    [When("a user searches for restaurants at that outcode")]
    public void WhenAUserSearchesForRestaurantsAtThatOutcode()
    {
        var server = _objectContainer.Resolve<WireMockServer>();
        var paths = _objectContainer.Resolve<ProjectPaths>();

        var args = $"-o {_outcode}";

        var envVariables = new Dictionary<string, string>
        {
            ["CLI_RestaurantApi__BaseAddress"] = server.Urls.First(),
        };

        _cli = ProcessUtils.ExecuteDotnetCommand($"{paths.PathToExecute} {args}", envVariables);
    }

    [Then("the restaurants are returned")]
    public async Task ThenTheRestaurantsAreReturned()
    {
        var resultAsText = await _cli.StandardOutput.ReadToEndAsync();
        await _cli.WaitForExitAsync();

        var result = JsonSerializer.Deserialize<RestaurantSearchResult>(resultAsText);

        var expectedResult = new RestaurantSearchResult
        {
            Restaurants = _setupRestaurants.Select(x => new RestaurantResult
            {
                Name = x.Name,
                Rating = x.Rating,
                CuisineTypes = new[] { x.CuisineType },
            }).ToList(),
        };

        result!.Should().BeEquivalentTo(
            expectedResult,
            o => o.ComparingByMembers<RestaurantSearchResult>().ComparingByMembers<RestaurantResult>()
        );
    }

    [Then("""the error "(.*)" is returned""")]
    public async Task ThenTheErrorIsReturned(string errorMessage)
    {
        var result = await _cli.StandardOutput.ReadToEndAsync();
        await _cli.WaitForExitAsync();
        result.TrimEnd().Should().Be(errorMessage);
    }
}