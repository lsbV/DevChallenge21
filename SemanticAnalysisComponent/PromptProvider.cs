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
                Classify the conversation into one or more relevant topics (e.g., "Visa and Passport Services," "Diplomatic Inquiries," "Travel Advisories," etc.).

               Transcription:
                Provide the transcription of the conversation. Correct any errors in the transcription if necessary. Convert text into scripted dialogue if it is possible.
                For example, convert "Hello. Hi. How are you?" to "A: Hello. B: Hi. A: How are you?" with each speaker's name before their dialogue and a colon after their name and punctuation at the end of each sentence.
               
               The output should be a JSON object without ANY addition text with the following structure:
               {
                 "people": ["John Doe", "Jane Doe"],
                 "locations": ["New York", "Los Angeles"],
                 "tone": "Neutral",
                 "topics": [
                 {"title":"Conversation topic 1", "points":["Point 1", "Point 2"]},
                 {"title":"Conversation topic 2", "points":["Point 1", "Point 2"]},
                 ],
                 "transcription": "The transcription of the conversation."
               }
               -------------------- 
               """;
    }



}