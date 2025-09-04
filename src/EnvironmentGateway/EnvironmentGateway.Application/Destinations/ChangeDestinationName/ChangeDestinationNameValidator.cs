using System.Text.RegularExpressions;
using FluentValidation;

namespace EnvironmentGateway.Application.Destinations.ChangeDestinationName;

public class ChangeDestinationNameValidator : AbstractValidator<ChangeDestinationNameCommand>
{
    private const string UrlPattern = "^[A-Za-z0-9-]+$";
    
    public ChangeDestinationNameValidator()
    {
        RuleFor(x => x.DestinationName)
            .NotEmpty()
            .MaximumLength(100)
            .Matches(UrlPattern, RegexOptions.IgnoreCase)
            .WithMessage("Please enter a valid name for the destination.\n" +
                        "Please make sure to follow these rules:\n" +
                        "• The name may only contain letters, numbers, and the '-' character\n" +
                        "• Do not use spaces or special characters\n" +
                        "• Maximum length is 100 characters");
    }
    
}
