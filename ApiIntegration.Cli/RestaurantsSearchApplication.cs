using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using ApiIntegration.Cli.Models;
using ApiIntegration.Cli.Services;
using CommandLine;
using OneOf;

namespace ApiIntegration.Cli;

public sealed class RestaurantsSearchApplication
{
    private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };
    private readonly IConsoleWriter _consoleWriter;
    private readonly IRestaurantsSearchService _restaurantsSearchService;


    public RestaurantsSearchApplication
    (
        IConsoleWriter consoleWriter,
        IRestaurantsSearchService restaurantsSearchService
    )
    {
        _consoleWriter = consoleWriter;
        _restaurantsSearchService = restaurantsSearchService;
    }

    public async Task RunAsync(IEnumerable<string> args)
    {
        await Parser.Default
            .ParseArguments<RestaurantsSearchApplicationOptions>(args)
            .WithParsedAsync(async o =>
            {
                var result = await _restaurantsSearchService.SearchByOutcodeAsync(new RestaurantSearchRequest(o.Outcode));
                _PrintSearchResult(result);
            });

        // TODO: handle API Errors
    }

    private void _PrintSearchResult(OneOf<RestaurantSearchResult, RestaurantSearchErrors> result)
    {
        result.Switch(
            searchResult =>
            {

                var text = JsonSerializer.Serialize(searchResult, JsonOptions);

                _consoleWriter.WriteLine(text);
            },
            error =>
            {
                var text = string.Join(", ", error.Errors);
                _consoleWriter.WriteLine(text);
            }
        );
    }
}