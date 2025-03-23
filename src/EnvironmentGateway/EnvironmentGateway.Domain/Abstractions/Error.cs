namespace EnvironmentGateway.Domain.Abstractions;

public record Error(string Code, string Name)
{
    public static Error None = new(String.Empty, String.Empty);

    public static Error NullValue = new("Error.NullValue", "Null Value was provided");
}