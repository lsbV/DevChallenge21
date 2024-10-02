using Core;

namespace CallComponent;

public interface ITranscriptionService
{
    Task<Transcription> TranscribeAsync(Audio audio);
}