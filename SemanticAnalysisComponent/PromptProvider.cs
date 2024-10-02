namespace SemanticAnalysisComponent;

public class PromptProvider
{

    public string GetPromptForAnlysis()
    {
        return """
               --------------------
               You are provided with a transcription of a conversation and an expected emotional tone. Your task is:

               Identify People and Locations:
                Extract any person names and locations mentioned in the transcription.

               Check Emotional Tone:
                Compare the emotional tone of the conversation with the given expected tone.
                If the tone in the conversation likely differs from the provided tone, adjust it accordingly to match the actual emotional tone inferred from the transcription.
                If the emotional tone is unclear from the text, do not change it.
                The emotional tone must be one of the following values: Neutral, Positive, Negative, Angry.
                
               Categorization:
                Classify the conversation into one or more relevant categories based on the topic (e.g., "Visa and Passport Services," "Diplomatic Inquiries," "Travel Advisories," etc.).

               The output should be a JSON object without ANY addition text with the following structure:
               {
                 "people": ["John Doe", "Jane Doe"],
                 "locations": ["New York", "Los Angeles"],
                 "tone": "Neutral",
                 "categories": [
                 {"title":"Conversation topic 1", "points":["Point 1", "Point 2"]},
                 {"title":"Conversation topic 2", "points":["Point 1", "Point 2"]},
                 ]
               }
               -------------------- 
               """;
    }



}