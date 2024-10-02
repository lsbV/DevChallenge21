namespace Core;

public enum EmotionalTone
{
    Positive,
    Negative,
    Neutral,
    Angry
}

public static class EmotionalToneExtensions
{
    public static string ToFriendlyString(this EmotionalTone tone)
    {
        return tone switch
        {
            EmotionalTone.Positive => "Positive",
            EmotionalTone.Negative => "Negative",
            EmotionalTone.Neutral => "Neutral",
            EmotionalTone.Angry => "Angry",
            _ => throw new ArgumentOutOfRangeException(nameof(tone), tone, null)
        };
    }

    public static EmotionalTone FromFriendlyString(string tone)
    {
        return tone switch
        {
            "Positive" => EmotionalTone.Positive,
            "Negative" => EmotionalTone.Negative,
            "Neutral" => EmotionalTone.Neutral,
            "Angry" => EmotionalTone.Angry,
            _ => throw new ArgumentOutOfRangeException(nameof(tone), tone, null)
        };
    }
}