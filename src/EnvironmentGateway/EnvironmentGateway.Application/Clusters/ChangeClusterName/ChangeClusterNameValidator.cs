using System.Text.RegularExpressions;
using FluentValidation;

namespace EnvironmentGateway.Application.Clusters.ChangeClusterName;

internal sealed class ChangeClusterNameValidator : AbstractValidator<ChangeClusterNameCommand>
{
    private const string UrlPattern = "^[A-Za-z0-9-]+$";
    
    public ChangeClusterNameValidator()
    {
        RuleFor(x => x.ClusterName)
            .NotEmpty()
            .MaximumLength(100)
            .Matches(UrlPattern, RegexOptions.IgnoreCase)
            .WithMessage("Please enter a valid name for the cluster.\n" +
                         "Please make sure to follow these rules:\n" +
                         "• The name may only contain letters, numbers, and the '-' character\n" +
                         "• Do not use spaces or special characters\n" +
                         "• Maximum length is 100 characters");
    }
}
