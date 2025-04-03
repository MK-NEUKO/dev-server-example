namespace EnvironmentGateway.Domain.Abstractions;

public sealed record Error(string Code, string Name)
{
    public static readonly Error None =
        new("", "");

    public static readonly Error NullValue = 
        new("Error.NullValue", "Null Value was provided");

    public static readonly Error CurrentConfigExists =
        new("Error.CurrentConfigExists", "A current gateway configuration is already available");
}