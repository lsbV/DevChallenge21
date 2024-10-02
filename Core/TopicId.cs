using System.ComponentModel.DataAnnotations.Schema;

namespace Core;

public record TopicId(int Value)
{
    public static TopicId Default => new(0);
}