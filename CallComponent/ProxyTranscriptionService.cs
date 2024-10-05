using System.Net;
using System.Net.Http.Headers;
using Core;
using Core.Exceptions;

namespace CallComponent;

internal class ProxyTranscriptionService(HttpClient httpClient) : ITranscriptionService
{
    private readonly Uri _transcriptionServerUri = new("http://transcription-server:5000/transcribe");
    public async Task<Transcription> TranscribeAsync(Audio audio)
    {
        var content = new MultipartFormDataContent();
        var audioContent = new ByteArrayContent(audio.Data.ToArray());
        audioContent.Headers.ContentType = new MediaTypeHeaderValue("audio/wav");
        content.Add(audioContent, "audio", "temp_audio.wav");
        var response = await httpClient.PostAsync(_transcriptionServerUri, content);
        if (response.StatusCode != HttpStatusCode.OK ||
            await response.Content.ReadAsStringAsync() is not { } transcriptionResponse)
        {
            throw new UnprocessableEntityException("Transcription failed");
        }

        return new Transcription(transcriptionResponse);
    }
}