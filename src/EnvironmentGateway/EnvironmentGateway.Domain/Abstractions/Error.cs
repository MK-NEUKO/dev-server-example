namespace EnvironmentGateway.Domain.Abstractions;

public sealed record Error(string Code, string Name)
{
    public static readonly Error None =
        new("", "");

    public static readonly Error NullValue = 
        new("Error.NullValue", "Null Value was provided");
    
    public static Error Problem(string code, string name) =>
        new(code, name);
}