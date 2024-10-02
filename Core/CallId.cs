namespace Core;

public record CallId(int Value)
{
    public static CallId Default => new(0);
}