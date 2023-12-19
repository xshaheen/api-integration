using CommandLine;

namespace ApiIntegration.Cli;

public class RestaurantsSearchApplicationOptions {
    [Option(
        'o',
        "outcode",
        Required = true,
        HelpText = "Providers the outcode to perform the search on.")]
    public string Outcode { get; init; }
}