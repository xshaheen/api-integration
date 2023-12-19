using System.Text.RegularExpressions;
using FluentValidation;

namespace ApiIntegration.Cli.Models;

public sealed partial record RestaurantSearchRequest(string Outcode)
{
    public sealed partial class Validator : AbstractValidator<RestaurantSearchRequest>
    {
        public Validator()
        {
            RuleFor(request => request.Outcode).NotEmpty()
                .Matches(OutCodePattern())
                .WithMessage("Please provide a valid UK Outcode.");
        }

        [GeneratedRegex("^([A-Za-z][0-9]{1,2})$", RegexOptions.Compiled)]
        private static partial Regex OutCodePattern();
    }
}