namespace ApiIntegration.Cli.Tests.Acceptance.Models;

public sealed class ProjectPaths
{
    public required string PathToBuild { get; init; } 

    public required string PathToExecute { get; init; }
}