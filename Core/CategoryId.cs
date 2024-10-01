using System.ComponentModel.DataAnnotations.Schema;

namespace Core;

public record CategoryId(int Value)
{
    public static CategoryId Default => new(0);
}