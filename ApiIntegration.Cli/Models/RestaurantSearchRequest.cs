using System.Text.RegularExpressions;
using FluentValidation;

namespace ApiIntegration.Cli.Models {
    public record RestaurantSearchRequest(string Outcode) {
        public class Validator : AbstractValidator<RestaurantSearchRequest> {
            private static readonly Regex OutCodePattern = new("^([A-Za-z][0-9]{1,2})$", RegexOptions.Compiled);

            public Validator() {
                RuleFor(request => request.Outcode).NotEmpty()
                    .Matches(OutCodePattern)
                    .WithMessage("Please provide a valid UK Outcode.");
            }
        }
    }
}