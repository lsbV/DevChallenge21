using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Core;
using Core.Exceptions;

namespace CallComponent;

internal class ProxyTranscriptionService(HttpClient httpClient) : ITranscriptionService
{
    private readonly Uri _transcriptionServerUri = new("http://localhost:5000/transcribe");
    public async Task<Transcription> TranscribeAsync(Audio audio)
    {
        var content = new ByteArrayContent(audio.Data);
        var response = await httpClient.PostAsync(_transcriptionServerUri, content);
        if (response.StatusCode != HttpStatusCode.OK ||
            await response.Content.ReadFromJsonAsync<TranscriptionResponse>() is not { } transcriptionResponse)
        {
            throw new UnprocessableEntityException("Transcription failed");
        }

        return new Transcription(transcriptionResponse.Transcription);
    }
    private sealed record TranscriptionResponse(string Transcription);
}

