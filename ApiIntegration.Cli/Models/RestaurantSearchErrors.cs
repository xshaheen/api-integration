using System.Collections.Generic;

namespace ApiIntegration.Cli.Models;

public sealed record RestaurantSearchErrors
{
    public RestaurantSearchErrors(IReadOnlyList<string> errors)
    {
        Errors = errors;
    }

    public IReadOnlyList<string> Errors { get; }
}